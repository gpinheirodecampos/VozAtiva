namespace VozAtiva.Application.Services.Interfaces;

public interface ISendEmailService
{
    Task EnqueueSendEmailAsync(Guid alertId, string name, string email);
}
