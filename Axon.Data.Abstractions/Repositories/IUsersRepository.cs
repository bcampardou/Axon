using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Axon.Data.Abstractions.Entities;

namespace Axon.Data.Abstractions.Repositories
{
    public interface IUsersRepository
    {
        User GetNew();
        /// <summary>
        /// Return every entities in database
        /// </summary>
        /// <returns></returns>
        Task<List<User>> FindAllAsync(int? maximumRows = null, int skipRows = 0, bool preloadProperties = true);

        Task<User> FindOneByPredicateAsync(Expression<Func<User, bool>> predicate, bool preloadProperties = true);
        Task<List<User>> FindByPredicateAsync(Expression<Func<User, bool>> predicate, bool preloadProperties = true);

        /// <summary>
        /// Async: Create the new entity in database
        /// </summary>
        /// <param name="entity">data</param>
        /// <returns></returns>
        void Create(User entity);

        /// <summary>
        /// Async: Creat the new entities in database
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        void Create(IEnumerable<User> entities);

        /// <summary>
        /// Async: Update the entity in database
        /// </summary>
        /// <param name="entity">data</param>
        /// <returns></returns>
        void Update(User entity);

        void Update(IEnumerable<User> entity);

        Task ReloadEntityAsync(User entity);

        /// <summary>
        /// Async: Remove the complete list from the database
        /// </summary>
        /// <param name="entitiesToRemove"></param>
        /// <returns></returns>
        void Delete(IEnumerable<User> entitiesToRemove);

        /// <summary>
        /// Async: delete the given entity from the database
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        void Delete(User entity);

        bool SaveChanges();
        Task<bool> SaveChangesAsync();
        /// <summary>
        /// Return the entity with the given primary key
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<User> FindAsync(Guid id, bool preloadProperties = true);

        Task<List<User>> FindAsync(IEnumerable<Guid> ids, bool preloadProperties = true);
        /// <summary>
        /// Async: delete the entity with the given id from the database
        /// </summary>
        /// <param name="id">the id of the entity</param>
        /// <returns></returns>
        void Delete(Guid id);
    }
}
