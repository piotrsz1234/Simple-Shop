using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Data.EF.Entities.Interfaces;

namespace Data.Infrastructure.Repositories.Interfaces
{
    public interface IRepositoryGenericBase<T> where T: class, IEntityBase
    {
        T? GetOne(long key);
        Task<T?> GetOneAsync(long key, params Expression<Func<T, object>>[] includes);
        Task<T?> GetOneAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes);
        IReadOnlyCollection<T> GetAll(Expression<Func<T, bool>> expression,
            params Expression<Func<T, object>>[] includes);
        Task<IReadOnlyCollection<T>> GetAllAsync(Expression<Func<T, bool>> expression,
            params Expression<Func<T, object>>[] includes);

        Task<bool> AnyAsync(Expression<Func<T, bool>> expression);
        bool Any(Expression<Func<T, bool>> expression);
        
        void Add(T entity);
        Task AddAsync(T entity);
        void SaveChanges();
        Task SaveChangesAsync();
    }
}