using Refit;
using System.Threading.Tasks;
using VozAtiva.Function.Models;

namespace VozAtiva.Function.Application.Email
{
    public interface IEmailServiceClient
    {
        [Post("/send-email")]
        Task SendEmailAsync([Body] SendEmailMessage message);
    }
}
