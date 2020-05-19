using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Axon.Core.Guards;
using Axon.Data.Abstractions;
using Axon.Data.Abstractions.Entities.Base;
using Axon.Data.Abstractions.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Axon.Data.Repositories
{
    public abstract class Repository<ENTITY> : IRepository<ENTITY>
        where ENTITY : IdentifiedEntity, new()
    {
        protected IServiceProvider _serviceProvider;
        protected DbSet<ENTITY> _dbSet => _context.Set<ENTITY>();
        protected AxonDbContext _context;

        public Repository(AxonDbContext context, IServiceProvider serviceProvider)
        {
            serviceProvider.GetRequiredService<ILoggerFactory>().CreateLogger<Repository<ENTITY>>().Log(LogLevel.Warning, $"NOUVELLE INSTANCE DE REPO : {this.GetType().Name}");
            _context = context;
            _serviceProvider = serviceProvider;
        }

        public ENTITY GetNew()
        {
            return new ENTITY();
        }

        public void Create(ENTITY entity)
        {
            try
            {
                _context.Entry(entity).State = EntityState.Added;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Create(IEnumerable<ENTITY> entities)
        {
            try
            {
                foreach (var entity in entities)
                {
                    _context.Entry(entity).State = EntityState.Added;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Delete(ENTITY entity)
        {
            try
            {
                _context.Entry(entity).State = EntityState.Deleted;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Delete(string id)
        {
            Delete(_dbSet.Find(id));
        }

        public void Delete(IEnumerable<ENTITY> entitiesToRemove)
        {
            try
            {
                foreach (var entity in entitiesToRemove)
                {
                    _context.Entry(entity).State = EntityState.Deleted;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public virtual async Task<List<ENTITY>> FindAllAsync(int? maximumRows = null, int skipRows = 0, bool preloadProperties = true)
        {
            var set = preloadProperties ? _loadProperties(_dbSet) : _dbSet;
            IQueryable<ENTITY> results = set.OrderByDescending(e => e.CreatedAt);


            if (skipRows > 0)
            {
                results = results.Skip(skipRows);
            }

            if (maximumRows.HasValue)
            {
                results = results.Take(maximumRows.Value);
            }

            return await results.ToListAsync();
        }

        public virtual async Task<ENTITY> FindAsync(string id, bool preloadProperties = true)
        {
            Ensure.Arguments.ThrowIfNotValidGuid(id, nameof(id));

            var set = preloadProperties ? _loadProperties(_dbSet) : _dbSet;

            return await set.SingleOrDefaultAsync(e => e.Id == id);
        }

        public virtual async Task<List<ENTITY>> FindAsync(IEnumerable<string> ids, bool preloadProperties = true)
        {
            var set = preloadProperties ? _loadProperties(_dbSet) : _dbSet;
            IQueryable<ENTITY> results = set.Where(e => ids.Contains(e.Id));

            return await results.ToListAsync();
        }

        public async Task<ENTITY> FindOneByPredicateAsync(Expression<Func<ENTITY, bool>> predicate, bool preloadProperties = true)
        {
            var set = preloadProperties ? _loadProperties(_dbSet) : _dbSet;
            return await set.SingleOrDefaultAsync(predicate);
        }

        public async Task<List<ENTITY>> FindByPredicateAsync(Expression<Func<ENTITY, bool>> predicate, bool preloadProperties = true)
        {
            var set = preloadProperties ? _loadProperties(_dbSet) : _dbSet;
            return await set.Where(predicate).ToListAsync();
        }

        public void Update(ENTITY entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }

        public void Update(IEnumerable<ENTITY> entities)
        {
            foreach (var entity in entities)
            {
                _context.Entry(entity).State = EntityState.Modified;
            }
        }

        public async Task ReloadEntityAsync(ENTITY entity)
        {
            await _context.Entry(entity).ReloadAsync();
        }

        public async Task<bool> SaveChangesAsync()
        {
            try
            {
                var codeResult = await _context.SaveChangesAsync();

                return codeResult >= 0;
            }
            catch (Exception ex)
            {
                string message = $"An error occured during the SaveChangesAsync : {ex.InnerException?.Message ?? ex.Message}";
                throw new Exception(message, ex);
            }
        }

        public bool SaveChanges()
        {
            try
            {
                var codeResult = _context.SaveChanges();

                return codeResult >= 0;
            }
            catch (Exception ex)
            {
                string message = $"An error occured during the SaveChanges : {ex.InnerException.InnerException.Message}";
                throw new Exception(message, ex);
            }
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Include properties to load with the entity not to use lazy loading (executing other requests)
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        protected abstract IQueryable<ENTITY> _loadProperties(IQueryable<ENTITY> entities);

        ~Repository()
        {

        }


    }
}
