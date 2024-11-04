using VozAtiva.Domain.Interfaces;
using VozAtiva.Infrastructure.Context;

namespace VozAtiva.Infrastructure.Repository;

public class UnitOfWork(AppDbContext context) : IUnitOfWork
{
    private IUserRepository _userRepository;
    private IAlertRepository _alertRepository;
    private IImageRepository _imageRepository;
    private IPublicAgentRepository _publicAgentRepository;

    public IUserRepository UserRepository => _userRepository ??= new UserRepository(context);

    public IAlertRepository AlertRepository => _alertRepository ??= new AlertRepository(context);

    public IImageRepository ImageRepository => _imageRepository ??= new ImageRepository(context);

    public IPublicAgentRepository PublicAgentRepository => _publicAgentRepository ??= new PublicAgentRepository(context);

    public async Task<bool> CommitAsync()
    {
        return await context.SaveChangesAsync() > 0;
    }
}
