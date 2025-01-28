using Refit;
using System.Threading.Tasks;
using VozAtiva.Function.Models;

namespace VozAtiva.Function.Application.Email
{
    public interface IEmailServiceClient
    {
        [Post("/sendemail/gov/new-alert")]
        Task SendEmailAsync([Body] SendEmailMessage message);
    }
}
