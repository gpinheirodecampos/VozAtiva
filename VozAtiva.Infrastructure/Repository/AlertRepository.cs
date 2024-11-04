using VozAtiva.Domain.Entities;
using VozAtiva.Domain.Interfaces;
using VozAtiva.Infrastructure.Context;

namespace VozAtiva.Infrastructure.Repository;

public class AlertRepository(AppDbContext context) : Repository<Alert>(context), IAlertRepository
{
}
