using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VozAtiva.Domain.Interfaces;
using VozAtiva.Infrastructure.Context;
using VozAtiva.Infrastructure.Repository;
using AutoMapper;
using VozAtiva.Application.DTOs.Mappings;
using VozAtiva.Application.Services.Interfaces;
using VozAtiva.Application.Services;

namespace VozAtiva.CrossCutting.IoC;

public static class DependencyInjectionAPI
{
    public static IServiceCollection AddInfrastructureAPI(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql("DATABASE_URL"));

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddScoped<IUserRepository, UserRepository>();

        services.AddScoped<IAlertRepository, AlertRepository>();

        services.AddScoped<IImageRepository, ImageRepository>();

        services.AddScoped<IPublicAgentRepository, PublicAgentRepository>();

        services.AddScoped<IUserService, UserService>();

        services.AddScoped<IPublicAgentService, PublicAgentService>();

        var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

        return services;
    }
}
