using System.Linq.Expressions;
using VozAtiva.Domain.Entities;

namespace VozAtiva.Domain.Interfaces;

public interface IAlertRepository : IRepository<Alert>
{
  Task<IEnumerable<Alert>> GetByConditionAsync(Expression<Func<Alert, bool>> predicate);
}
