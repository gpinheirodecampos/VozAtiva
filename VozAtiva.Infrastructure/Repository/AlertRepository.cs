using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using VozAtiva.Domain.Entities;
using VozAtiva.Domain.Interfaces;
using VozAtiva.Infrastructure.Context;

namespace VozAtiva.Infrastructure.Repository;

public class AlertRepository(AppDbContext context) : Repository<Alert>(context), IAlertRepository
{
  public async Task<IEnumerable<Alert>> GetByConditionAsync(Expression<Func<Alert, bool>> predicate)
  {
      return await context.Set<Alert>()
                            .Where(predicate)
                            .ToListAsync();
  }
}
