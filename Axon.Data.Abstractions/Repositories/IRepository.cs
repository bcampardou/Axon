using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Axon.Data.Abstractions.Entities.Base;

namespace Axon.Data.Abstractions.Repositories
{
    public interface IRepository<ENTITY> : IDisposable
        where ENTITY : Entity, new()
    {
        ENTITY GetNew();
        /// <summary>
        /// Return every entities in database
        /// </summary>
        /// <returns></returns>
        Task<List<ENTITY>> FindAllAsync(int? maximumRows = null, int skipRows = 0, bool preloadProperties = true);

        Task<ENTITY> FindOneByPredicateAsync(Expression<Func<ENTITY, bool>> predicate, bool preloadProperties = true);
        Task<List<ENTITY>> FindByPredicateAsync(Expression<Func<ENTITY, bool>> predicate, bool preloadProperties = true);

        /// <summary>
        /// Async: Create the new entity in database
        /// </summary>
        /// <param name="entity">data</param>
        /// <returns></returns>
        void Create(ENTITY entity);

        /// <summary>
        /// Async: Creat the new entities in database
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        void Create(IEnumerable<ENTITY> entities);

        /// <summary>
        /// Async: Update the entity in database
        /// </summary>
        /// <param name="entity">data</param>
        /// <returns></returns>
        void Update(ENTITY entity);

        void Update(IEnumerable<ENTITY> entity);

        Task ReloadEntityAsync(ENTITY entity);

        /// <summary>
        /// Async: Remove the complete list from the database
        /// </summary>
        /// <param name="entitiesToRemove"></param>
        /// <returns></returns>
        void Delete(IEnumerable<ENTITY> entitiesToRemove);

        /// <summary>
        /// Async: delete the given entity from the database
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        void Delete(ENTITY entity);

        bool SaveChanges();
        Task<bool> SaveChangesAsync();
    }

    public interface IRepositoryWithIdentifier<ENTITY> : IRepository<ENTITY>, IDisposable
        where ENTITY : IdentifiedEntity, new()
    {

        /// <summary>
        /// Return the entity with the given primary key
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<ENTITY> FindAsync(Guid id, bool preloadProperties = true);

        Task<List<ENTITY>> FindAsync(IEnumerable<Guid> ids, bool preloadProperties = true);
        /// <summary>
        /// Async: delete the entity with the given id from the database
        /// </summary>
        /// <param name="id">the id of the entity</param>
        /// <returns></returns>
        void Delete(string id);
    }
}
