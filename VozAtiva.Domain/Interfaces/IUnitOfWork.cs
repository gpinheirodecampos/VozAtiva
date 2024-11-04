namespace VozAtiva.Domain.Interfaces;

public interface IUnitOfWork
{
    IUserRepository UserRepository { get; }
    IAlertRepository AlertRepository { get; }
    IImageRepository ImageRepository { get; }
    IPublicAgentRepository PublicAgentRepository { get; }
    Task<bool> CommitAsync();
}
