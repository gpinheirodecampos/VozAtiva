using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Refit;
using System;
using VozAtiva.Function.Application.Email;

namespace VozAtiva.Function
{
    public class Program
    {
        static void Main(string[] args)
        {
            FunctionsDebugger.Enable();

            var host = new HostBuilder()
            .ConfigureFunctionsWorkerDefaults()
            .ConfigureServices(services =>
            {
                services.AddRefitClient<IEmailServiceClient>()
                    .ConfigureHttpClient(c =>
                    {
                        c.BaseAddress = new Uri(Environment.GetEnvironmentVariable("EmailServiceBaseUrl"));
                    });
            })
            .Build();


            host.Run();
        }
    }
}
