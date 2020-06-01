using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Axon.Business.Abstractions.Adapters;
using Axon.Business.Abstractions.Models;
using Axon.Business.Abstractions.Services;
using Axon.Business.Rules;
using Axon.Core.Constants;
using Axon.Core.Exceptions;
using Axon.Core.Guards;
using Axon.Data.Abstractions.Entities.Base;
using Axon.Data.Abstractions.Repositories;
using EasyCaching.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Axon.Business.Services
{
    public abstract class Service : IService
    {
        protected List<string> _errors = new List<string>();
        protected ClaimsPrincipal _currentUser { get; private set; }
        protected IEasyCachingProvider _cachingProvider { get; private set; }
        protected ILogger<Service> _logger => _loggerFactory.CreateLogger<Service>();
        protected ILoggerFactory _loggerFactory;

        protected IConfiguration _configuration;
        protected IServiceProvider _serviceProvider;

        public ICollection<string> Errors => _errors;
        public bool HasErrors => Errors.Any();

        public Service(
            ILoggerFactory loggerFactory,
            IConfiguration configuration,
            IEasyCachingProvider cachingProvider,
            ClaimsPrincipal currentUser,
            IServiceProvider serviceProvider)
        {
            _loggerFactory = loggerFactory;
            _currentUser = currentUser;
            _cachingProvider = cachingProvider;
            _configuration = configuration;
            _serviceProvider = serviceProvider;
        }

        public void Dispose()
        {
            Dispose(true);

            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
        }

        ~Service()
        {
            Dispose(false);
        }
    }

    public class Service<ENTITY, DTO, ADAPTER, IREPO> : Service, IService<ENTITY, DTO>
        where ENTITY : IdentifiedEntity, new()
        where DTO : IdentifiedEntityDTO<ENTITY>, new()
        where ADAPTER : IdentifiedEntityAdapter<ENTITY, DTO>, new()
        where IREPO : IRepositoryWithIdentifier<ENTITY>
    {
        private readonly ADAPTER adapter;
        private Lazy<IREPO> _repository;
        protected IREPO Repository => _repository.Value;

        public Service(
            ILoggerFactory loggerFactory,
            IConfiguration configuration,
            IEasyCachingProvider cachingProvider,
            ClaimsPrincipal currentUser,
            IServiceProvider serviceProvider,
            ADAPTER adapter,
            Lazy<IREPO> repository)
            : base(loggerFactory, configuration, cachingProvider, currentUser, serviceProvider)
        {
            this.adapter = adapter;
            _repository = repository;
        }

        public virtual async Task<DTO> CreateOrUpdateAsync(DTO dto)
        {
            if (!Ensure.Arguments.IsValidGuid(dto.Id))
            {
                return await CreateAsync(dto);
            }
            return await UpdateAsync(dto);
        }

        public virtual async Task<bool> CreateOrUpdateAsync(IEnumerable<DTO> dtos)
        {
            await CreateAsync(dtos.Where(d => !Ensure.Arguments.IsValidGuid(d.Id)));
            await UpdateAsync(dtos.Where(d => Ensure.Arguments.IsValidGuid(d.Id)));

            return true;
        }

        public virtual async Task<DTO> CreateAsync(DTO dto)
        {
            var entity = _ApplyChanges(dto, Repository.GetNew());

            return await _createAsync(entity);
        }

        public virtual async Task<List<DTO>> CreateAsync(IEnumerable<DTO> dtos)
        {
            try
            {
                if (dtos == null || !dtos.Any())
                    return dtos?.ToList();

                var entities = new List<ENTITY>();
                List<DTO> results = new List<DTO>();
                foreach (var dtoNew in dtos)
                {
                    entities.Add(_ApplyChanges(dtoNew, new ENTITY()));
                }

                foreach (var entity in entities)
                {
                    if (!_onBeforeCreateOrUpdate(entity, null))
                        throw new AxonException("CREATE_NOK", Errors);
                }

                Repository.Create(entities);
                if (!await Repository.SaveChangesAsync())
                    throw new AxonException("CREATE_NOK", Errors);

                results = await FindAsync(entities.Select(e => e.Id).ToList());
                _cachingProvider.Remove(BusinessRules.CacheListKey(typeof(ENTITY)));
                foreach (var entity in entities)
                {
                    if (!_onAfterCreateOrUpdate(entity, ActionType.Creation))
                        throw new AxonException("CREATE_NOK", Errors);
                }
                return results;
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        public virtual async Task<bool> DeleteAsync(DTO dto)
        {
            var entity = await Repository.FindAsync(dto.Id);
            if (!_onBeforeDelete(entity))
                return false;
            _cachingProvider.Remove(BusinessRules.CacheListKey(typeof(ENTITY)));
            _cachingProvider.Remove(BusinessRules.CacheObjectKey(entity));

            Repository.Delete(entity);
            return await Repository.SaveChangesAsync();
        }

        internal virtual async Task<ENTITY> _updateAsync(ENTITY entity)
        {
            var savedEntity = await Repository.FindAsync(entity.Id);
            if (!_onBeforeCreateOrUpdate(entity, savedEntity))
                throw new AxonException("SAVE_NOK", Errors);
            Repository.Update(entity);
            if (!await Repository.SaveChangesAsync())
                throw new AxonException("SAVE_NOK", Errors);
            if (!_onAfterCreateOrUpdate(entity, ActionType.Update))
                throw new AxonException("SAVE_NOK", Errors);

            // Invalide cache
            _cachingProvider.Remove(BusinessRules.CacheListKey(typeof(ENTITY)));
            _cachingProvider.Remove(BusinessRules.CacheObjectKey(entity));

            return entity;
        }

        internal virtual async Task<DTO> _createAsync(ENTITY entity)
        {
            if (!_onBeforeCreateOrUpdate(entity, null))
                throw new AxonException("DELETE_NOK", Errors);
            Repository.Create(entity);
            var saved = await Repository.SaveChangesAsync();
            if (!saved || !Ensure.Arguments.IsValidGuid(entity.Id))
                throw new AxonException("DELETE_NOK", Errors);
            if (!_onAfterCreateOrUpdate(entity, ActionType.Creation))
                throw new AxonException("DELETE_NOK", Errors);

            // Invalidate cache
            _cachingProvider.Remove(BusinessRules.CacheListKey(typeof(ENTITY)));
            return await FindAsync(entity.Id);
        }

        internal virtual async Task<bool> _deleteAsync(ENTITY entity)
        {
            if (!_onBeforeDelete(entity))
                return false;
            _cachingProvider.Remove(BusinessRules.CacheListKey(typeof(ENTITY)));
            _cachingProvider.Remove(BusinessRules.CacheObjectKey(entity));
            Repository.Delete(entity);
            return await Repository.SaveChangesAsync(); ;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var entity = await Repository.FindAsync(id);
            return await _deleteAsync(entity);
        }

        public async Task<bool> DeleteAsync(IEnumerable<DTO> elementsToRemove)
        {
            return await DeleteAsync(elementsToRemove.Select(e => e.Id));
        }

        public async Task<bool> DeleteAsync(IEnumerable<string> ids)
        {
            var entities = await Repository.FindAsync(ids);
            return await _deleteListAsync(entities);
        }

        private async Task<List<DTO>> _findAll(int? maximumRows = null, int skipRows = 0)
        {
            var results = await Repository.FindAllAsync(maximumRows, skipRows);
            return results.Select(e => _ToDTO(e)).ToList();

        }

        public async Task<List<DTO>> FindAllAsync(bool useCache = true, int? maximumRows = null, int skipRows = 0)
        {
            if (!useCache)
                return await _findAll(maximumRows, skipRows);

            var cacheKey = BusinessRules.CacheListKey(typeof(ENTITY));
            var cached = await _cachingProvider.GetAsync<List<DTO>>(cacheKey);
            if (cached.HasValue && !cached.IsNull)
            {
                return cached.Value;
            }
            var results = await _findAll(maximumRows, skipRows);
            await _cachingProvider.SetAsync(cacheKey, results, TimeSpan.FromSeconds(_configuration.GetValue<int>(ConfigurationConstants.CacheInSeconds)));
            return results;
        }

        public async Task<List<DTO>> FindAsync(IEnumerable<string> ids)
        {
            var results = await Repository.FindAsync(ids);
            return results.Select(e => _ToDTO(e)).ToList();
        }

        public async Task<DTO> FindAsync(string id, bool forceReloadCache = false)
        {
            var cacheKey = BusinessRules.CacheObjectKey(typeof(ENTITY), id);
            var cached = await _cachingProvider.GetAsync<DTO>(cacheKey);
            if (!forceReloadCache && cached.HasValue && !cached.IsNull)
            {
                return cached.Value;
            }
            var result = _ToDTO(await Repository.FindAsync(id));
            await _cachingProvider.SetAsync(cacheKey, result, TimeSpan.FromSeconds(_configuration.GetValue<int>(ConfigurationConstants.CacheInSeconds)));
            return result;
        }

        public virtual async Task<List<DTO>> UpdateAsync(IEnumerable<DTO> dtos)
        {
            Dictionary<string, ENTITY> dicoSavedEntities = (await Repository.FindAsync(dtos.Select(e => e.Id), true)).ToDictionary(savedEntity => savedEntity.Id);
            var entities = new List<ENTITY>();
            foreach (var dto in dtos)
            {
                if (dicoSavedEntities.ContainsKey(dto.Id))
                {
                    var entity = _ApplyChanges(dto, dicoSavedEntities[dto.Id]);
                    entities.Add(entity);
                    if (!_onBeforeCreateOrUpdate(entity, dicoSavedEntities[entity.Id]))
                        throw new AxonException("SAVE_NOK", Errors);
                }
            }
            Repository.Update(entities);
            if (!await Repository.SaveChangesAsync())
                throw new AxonException("SAVE_NOK", Errors);

            // invalidate cache
            _cachingProvider.Remove(BusinessRules.CacheListKey(typeof(ENTITY)));

            foreach (var entity in entities)
            {
                _cachingProvider.Remove(BusinessRules.CacheObjectKey(entity));
                if (!_onAfterCreateOrUpdate(entity, ActionType.Update))
                    throw new AxonException("SAVE_NOK", Errors);
            }
            return entities.Select(e => _ToDTO(e)).ToList();
        }

        public virtual async Task<DTO> UpdateAsync(DTO dto)
        {
            var entity = _ApplyChanges(dto, await Repository.FindAsync(dto.Id));

            return _ToDTO(await _updateAsync(entity));
        }

        internal async Task<bool> _deleteListAsync(IEnumerable<ENTITY> elementsToRemove)
        {
            foreach (ENTITY entity in elementsToRemove)
            {
                if (!_onBeforeDelete(entity))
                    return false;
            }
            _cachingProvider.Remove(BusinessRules.CacheListKey(typeof(ENTITY)));
            foreach (var entity in elementsToRemove)
            {
                _cachingProvider.Remove(BusinessRules.CacheObjectKey(entity));
            }
            // Virtual deletion only
            try
            {
                Repository.Delete(elementsToRemove);

                await Repository.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return false;
            }
            return true;
        }

        internal async Task<IEnumerable<ENTITY>> _updateListAsync(IEnumerable<ENTITY> entities)
        {
            var savedEntities = (await Repository.FindAsync(entities.Select(e => e.Id), true)).ToDictionary(e => e.Id);
            foreach (ENTITY entity in entities)
            {
                if (!_onBeforeCreateOrUpdate(entity, savedEntities[entity.Id]))
                    return null;
            }

            Repository.Update(entities);
            await Repository.SaveChangesAsync();
            // Invalide cache
            _cachingProvider.Remove(BusinessRules.CacheListKey(typeof(ENTITY)));
            foreach (ENTITY entity in entities)
            {
                _cachingProvider.Remove(BusinessRules.CacheObjectKey(entity));
                if (!_onAfterCreateOrUpdate(entity, ActionType.Update))
                    return null;
            }
            return entities;
        }

        /// <summary>
        /// Method called before deletion. (ie. Can be used to manage cascade delete)
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        protected virtual bool _onBeforeDelete(ENTITY entity)
        {
            return true;
        }

        /// <summary>
        /// Method called before creation or update (ie. Can be used to initialize dependancies / historic objects or write logs)
        /// </summary>
        /// <param name="entity">New state of the entity</param>
        /// <param name="savedEntity">Saved state of the entity (null for creation)</param>
        /// <returns></returns>
        protected virtual bool _onBeforeCreateOrUpdate(ENTITY entity, ENTITY savedEntity)
        {
            if(savedEntity == null && !Ensure.Arguments.IsValidGuid(entity.Id))
            {
                entity.Id = BusinessRules.GenerateIdentifier();
            }
            return true;
        }

        protected virtual bool _onAfterCreateOrUpdate(ENTITY entity, ActionType action)
        {
            return true;
        }

        protected DTO _ToDTO(ENTITY entity)
        {
            if (entity == null) return null;
            return adapter.Convert(entity);
        }

        protected ENTITY _ApplyChanges(DTO dto, ENTITY entity)
        {
            if (dto == null || entity == null) return null;
            return adapter.Bind(entity, dto);
        }

        protected List<DTO> _ToDTO(IEnumerable<ENTITY> entities)
        {
            var results = new List<DTO>();
            foreach (var entity in entities)
            {
                if (entity == null) return null;
                results.Add(adapter.Convert(entity));
            }
            return results;
        }

        protected List<ENTITY> _ApplyChanges(IEnumerable<(DTO dto, ENTITY entity)> datas)
        {
            var results = new List<ENTITY>();
            foreach (var (dto, entity) in datas)
            {
                if (dto == null || entity == null) continue;
                results.Add(
                    adapter.Bind(entity, dto)
                );
            }
            return results;
        }

        protected enum ActionType
        {
            Creation,
            Update
        }
    }
}
