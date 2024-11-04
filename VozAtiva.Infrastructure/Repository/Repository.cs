using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using VozAtiva.Domain.Interfaces;
using VozAtiva.Infrastructure.Context;

namespace VozAtiva.Infrastructure.Repository;

public class Repository<T>(AppDbContext context) : IRepository<T> where T : class
{
    public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, object>>? include = null)
    {
        IQueryable<T> query = context.Set<T>().AsNoTracking();

        if (include != null)
        {
            query = query.Include(include);
        }

        return await query.ToListAsync();
    }

    public async Task<T> GetByPropertyAsync(Expression<Func<T, bool>> predicate, Expression<Func<T, object>>? include = null)
    {
        IQueryable<T> query = context.Set<T>().AsNoTracking().Where(predicate);

        if (include != null)
        {
            query = query.Include(include);
        }

        return await query.SingleOrDefaultAsync();
    }

    public virtual async Task<bool> AddAsync(T entity)
    {
        await context.Set<T>().AddAsync(entity);
        return await context.SaveChangesAsync() > 0;
    }

    public async Task<bool> DeleteAsync(T entity)
    {
        if (context != null)
        {
            context.Set<T>().Remove(entity);
            return await context.SaveChangesAsync() > 0;
        }
        return false;
    }

    public async Task<bool> UpdateAsync(T entity)
    {
        context.Entry(entity).State = EntityState.Modified;
        context.Set<T>().Update(entity);
        return await context.SaveChangesAsync() > 0;
    }
}
