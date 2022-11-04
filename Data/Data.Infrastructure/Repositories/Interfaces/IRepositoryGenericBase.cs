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
        Task<T?> GetOneAsync(long key);

        IReadOnlyCollection<T> GetAll(Expression<Func<T, bool>> expression,
            params Expression<Func<T, object>>[] includes);
        Task<IReadOnlyCollection<T>> GetAllAsync(Expression<Func<T, bool>> expression,
            params Expression<Func<T, object>>[] includes);

        void Add(T entity);
        Task AddAsync(T entity);
        void SaveChanges();
        Task SaveChangesAsync();
    }
}