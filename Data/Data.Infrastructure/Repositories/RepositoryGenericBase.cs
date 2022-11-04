using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Data.EF.Contexts;
using Data.EF.Entities.Interfaces;
using Data.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Data.Infrastructure.Repositories
{
    public abstract class RepositoryGenericBase<T> : IRepositoryGenericBase<T> where T : class, IEntityBase
    {
        protected ShopContext DbContext;
        protected ILogger Logger;

        public RepositoryGenericBase(ShopContext dbContext, ILogger logger)
        {
            DbContext = dbContext;
            Logger = logger;
        }

        public T? GetOne(long key)
        {
            try
            {
                return DbContext.Set<T>().FirstOrDefault(x => x.Id == key);
            }
            catch (Exception e)
            {
                Logger.LogError(e.Message, e);
                throw;
            }
        }

        public async Task<T?> GetOneAsync(long key)
        {
            try
            {
                return await DbContext.Set<T>().FirstOrDefaultAsync(x => x.Id == key);
            }
            catch (Exception e)
            {
                Logger.LogError(e.Message, e);
                throw;
            }
        }

        public IReadOnlyCollection<T> GetAll(Expression<Func<T, bool>> expression,
            params Expression<Func<T, object>>[] includes)
        {
            try
            {
                var query = DbContext.Set<T>().AsQueryable();

                foreach (var include in includes)
                {
                    query = query.Include(include);
                }

                return query.Where(expression).ToArray();
            }
            catch (Exception e)
            {
                Logger.LogError(e.Message, e);
                throw;
            }
        }

        public async Task<IReadOnlyCollection<T>> GetAllAsync(Expression<Func<T, bool>> expression,
            params Expression<Func<T, object>>[] includes)
        {
            try
            {
                var query = DbContext.Set<T>().AsQueryable();

                foreach (var include in includes)
                {
                    query = query.Include(include);
                }

                return await query.Where(expression).ToArrayAsync();
            }
            catch (Exception e)
            {
                Logger.LogError(e.Message, e);
                throw;
            }
        }

        public void Add(T entity)
        {
            try
            {
                if (entity.Id == 0)
                {
                    DbContext.Set<T>().Add(entity);
                }
            }
            catch (Exception e)
            {
                Logger.LogError(e.Message, e);
                throw;
            }
        }

        public async Task AddAsync(T entity)
        {
            try
            {
                if (entity.Id == 0)
                {
                    await DbContext.Set<T>().AddAsync(entity);
                }
            }
            catch (Exception e)
            {
                Logger.LogError(e.Message, e);
                throw;
            }
        }

        public void SaveChanges()
        {
            try
            {
                DbContext.SaveChanges();
            }
            catch (Exception e)
            {
                Logger.LogError(e.Message, e);
                throw;
            }
        }

        public async Task SaveChangesAsync()
        {
            try
            {
                await DbContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Logger.LogError(e.Message, e);
                throw;
            }
        }
    }
}