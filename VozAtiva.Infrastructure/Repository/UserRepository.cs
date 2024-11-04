using VozAtiva.Domain.Entities;
using VozAtiva.Domain.Interfaces;
using VozAtiva.Infrastructure.Context;

namespace VozAtiva.Infrastructure.Repository;

public class UserRepository(AppDbContext context) : Repository<User>(context), IUserRepository
{
}