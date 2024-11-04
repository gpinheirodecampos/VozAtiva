using VozAtiva.Domain.Entities;
using VozAtiva.Domain.Interfaces;
using VozAtiva.Infrastructure.Context;

namespace VozAtiva.Infrastructure.Repository;

public class PublicAgentRepository(AppDbContext context) : Repository<PublicAgent>(context), IPublicAgentRepository
{
}