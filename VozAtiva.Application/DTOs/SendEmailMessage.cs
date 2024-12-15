namespace VozAtiva.Application.DTOs;

public class SendEmailMessage
{
    public Guid AlertId { get; set; }
    public string? Name { get; set; }
    public string? Email { get; set; }
}
