using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Axon.Core.Guards;
using Axon.Data.Abstractions;
using Axon.Data.Abstractions.Entities;
using Axon.Data.Abstractions.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Axon.Data.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        protected IServiceProvider _serviceProvider;
        protected DbSet<User> _dbSet => _context.Set<User>();
        protected AxonDbContext _context;
        public UsersRepository(AxonDbContext context, IServiceProvider serviceProvider)
        {
            serviceProvider.GetRequiredService<ILoggerFactory>().CreateLogger<UsersRepository>().Log(LogLevel.Debug, $"NOUVELLE INSTANCE DE REPO : {this.GetType().Name}");
            _context = context;
            _serviceProvider = serviceProvider;
        }

        public User GetNew()
        {
            return new User();
        }

        public void Create(User entity)
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

        public void Create(IEnumerable<User> entities)
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

        public void Delete(User entity)
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

        public void Delete(IEnumerable<User> entitiesToRemove)
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

        public virtual async Task<List<User>> FindAllAsync(int? maximumRows = null, int skipRows = 0, bool preloadProperties = true)
        {
            var set = preloadProperties ? _loadProperties(_dbSet) : _dbSet;
            IQueryable<User> results = set.OrderByDescending(e => e.CreatedAt);


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

        public async Task<User> FindOneByPredicateAsync(Expression<Func<User, bool>> predicate, bool preloadProperties = true)
        {
            var set = preloadProperties ? _loadProperties(_dbSet) : _dbSet;
            return await set.SingleOrDefaultAsync(predicate);
        }

        public async Task<List<User>> FindByPredicateAsync(Expression<Func<User, bool>> predicate, bool preloadProperties = true)
        {
            var set = preloadProperties ? _loadProperties(_dbSet) : _dbSet;
            return await set.Where(predicate).ToListAsync();
        }

        public void Update(User entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }

        public void Update(IEnumerable<User> entities)
        {
            foreach (var entity in entities)
            {
                _context.Entry(entity).State = EntityState.Modified;
            }
        }

        public async Task ReloadEntityAsync(User entity)
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

        public void Delete(Guid id)
        {
            Delete(_dbSet.Find(id));
        }



        public virtual async Task<User> FindAsync(Guid id, bool preloadProperties = true)
        {
            Ensure.Arguments.ThrowIfNotValidGuid(id, nameof(id));

            var set = preloadProperties ? _loadProperties(_dbSet) : _dbSet;

            return await set.SingleOrDefaultAsync(e => e.Id == id);
        }

        public virtual async Task<List<User>> FindAsync(IEnumerable<Guid> ids, bool preloadProperties = true)
        {
            var set = preloadProperties ? _loadProperties(_dbSet) : _dbSet;
            IQueryable<User> results = set.Where(e => ids.Contains(e.Id));

            return await results.ToListAsync();
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        protected IQueryable<User> _loadProperties(IQueryable<User> entities)
        {
            return entities.Include(u => u.Tenant).ThenInclude(t => t.Licenses);
        }
    }
}
