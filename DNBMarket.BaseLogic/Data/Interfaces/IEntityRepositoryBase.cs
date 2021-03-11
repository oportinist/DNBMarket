using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DNBMarket.BaseLogic.Data.Interfaces
{
    public interface IEntityRepositoryBase<T> where T : class, IEntity, new()
    {
        Task<List<T>> WhereAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties); 
        Task<List<T>> WherePassive(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties);
        Task<IList<T>> ToListAsync(Expression<Func<T, bool>> predicate = null, params Expression<Func<T, object>>[] includeProperties);
        Task<T> AddAsync(T entity);
        T Add(T entity);
        Task<int> GetLastId(T entity);
        Task SoftDelete(T entity);
        Task<bool> AnyAsync(Expression<Func<T, bool>> predicate);
        Task<int> CountAsync(Expression<Func<T, bool>> predicate);
        T AddOrUpdate(T entity, int key = 0);
        Task<T> AddOrUpdateAsync(T entity, int key = 0);
        T FirstOrDefault(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties);
        T FirstOrDefaultPassive(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties);
        Task<T> FirtsOrDefaultAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties);
        Task<T> FirtsOrDefaultPassiveAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties);
    }
}
