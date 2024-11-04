using System.Linq.Expressions;

namespace VozAtiva.Domain.Interfaces;

public interface IRepository<T>
{
    Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, object>>? include = null);
    Task<T> GetByPropertyAsync(Expression<Func<T, bool>> predicate, Expression<Func<T, object>>? include = null);
    Task<bool> AddAsync(T entity);
    Task<bool> UpdateAsync(T entity);
    Task<bool> DeleteAsync(T entity);
}
