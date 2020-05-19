using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Axon.Business.Abstractions.Models;
using Axon.Data.Abstractions.Entities.Base;

namespace Axon.Business.Abstractions.Services
{
    public interface IService : IDisposable
    {
        ICollection<string> Errors { get; }
        bool HasErrors { get; }
    }

    public interface IService<ENTITY, DTO> : IService
        where ENTITY : Entity, new()
        where DTO : EntityDTO<ENTITY>, new()
    {
        Task<DTO> CreateOrUpdateAsync(DTO data);

        Task<bool> CreateOrUpdateAsync(IEnumerable<DTO> datas);

        /// <summary>
        /// Return every entities in database
        /// </summary>
        /// <returns></returns>
        Task<List<DTO>> FindAllAsync(bool useCache = true, int? maximumRows = null, int skipRows = 0);

        Task<List<DTO>> FindAsync(IEnumerable<string> ids);

        /// <summary>
        /// Return the entity with the given primary key
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<DTO> FindAsync(string id, bool forceReloadCache = false);

        /// <summary>
        /// Async: Create the new entity in database
        /// </summary>
        /// <param name="entity">data</param>
        /// <returns></returns>
        Task<DTO> CreateAsync(DTO entity);

        Task<List<DTO>> CreateAsync(IEnumerable<DTO> objects);

        /// <summary>
        /// Async: Update the entity in database
        /// </summary>
        /// <param name="entity">data</param>
        /// <returns></returns>
        Task<DTO> UpdateAsync(DTO entity);

        Task<List<DTO>> UpdateAsync(IEnumerable<DTO> objects);

        /// <summary>
        /// Async: delete the entity with the given id from the database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> DeleteAsync(string id);

        Task<bool> DeleteAsync(DTO entity);

        Task<bool> DeleteAsync(IEnumerable<DTO> elementsToRemove);

        Task<bool> DeleteAsync(IEnumerable<string> ids);
    }
}
