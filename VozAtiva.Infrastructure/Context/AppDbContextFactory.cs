using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace VozAtiva.Infrastructure.Context;

public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        //// Caminho para o diretório onde o appsettings.json está localizado
        //var basePath = Path.Combine(Directory.GetCurrentDirectory(), "../VozAtiva.API");

        //// Crie o builder para carregar as configurações
        //var configuration = new ConfigurationBuilder()
        //    .SetBasePath(basePath)
        //    .AddJsonFile("appsettings.json")
        //    .Build();

        //// Leia a connection string do appsettings.json
        //var connectionString = configuration.GetConnectionString("DefaultConnection");

        // Configure o DbContextOptions
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
        optionsBuilder.UseNpgsql("Server=localhost;DataBase=vozativa;Uid=postgres;Pwd=Daora2010%");

        return new AppDbContext(optionsBuilder.Options);
    }
}
