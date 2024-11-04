using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VozAtiva.Domain.Interfaces;
using VozAtiva.Infrastructure.Context;
using VozAtiva.Infrastructure.Repository;

namespace VozAtiva.CrossCutting.IoC;

public static class DependencyInjectionAPI
{
    public static IServiceCollection AddInfrastructureAPI(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddScoped<IUserRepository, UserRepository>();

        services.AddScoped<IAlertRepository, AlertRepository>();

        services.AddScoped<IImageRepository, ImageRepository>();

        services.AddScoped<IPublicAgentRepository, PublicAgentRepository>();

        return services;
    }
}
