using Microsoft.EntityFrameworkCore;
using VozAtiva.Domain.Entities;
using VozAtiva.Domain.Entities.Types;

namespace VozAtiva.Infrastructure.Context;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Alert> Alerts { get; set; } = null!;
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<PublicAgent> PublicAgents { get; set; } = null!;
    public DbSet<Image> Images { get; set; } = null!;
    public DbSet<AgentType> AgentTypes { get; set; } = null!;
    public DbSet<AlertType> AlertTypes { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(entity =>
        {
            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd(); // Para gerar Guid automaticamente

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()") // Define o valor padrão como o horário atual
                .ValueGeneratedOnAdd(); // Gera automaticamente na inserção

            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("now()") // Define o valor padrão como o horário atual
                .ValueGeneratedOnAddOrUpdate(); // Atualiza automaticamente na atualização

            entity.Property(e => e.Active)
                .HasDefaultValue(true); // Define o valor padrão como true

            entity.Property(e => e.Disabled)
                .HasDefaultValue(false); // Define o valor padrão como false
        });

        modelBuilder.Entity<Alert>(entity =>
        {
            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd(); // Para gerar Guid automaticamente

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()") // Define o valor padrão como o horário atual
                .ValueGeneratedOnAdd(); // Gera automaticamente na inserção

            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("now()") // Define o valor padrão como o horário atual
                .ValueGeneratedOnAddOrUpdate(); // Atualiza automaticamente na atualização

            entity.Property(e => e.Active)
                .HasDefaultValue(true); // Define o valor padrão como true

            entity.Property(e => e.Disabled)
                .HasDefaultValue(false); // Define o valor padrão como false
        });



        modelBuilder.Entity<Alert>()
            .Property(a => a.Date)
            .HasColumnType("timestamp without time zone")
            .HasDefaultValueSql("CURRENT_TIMESTAMP");

        modelBuilder.Entity<Alert>()
            .HasOne(a => a.User)
            .WithMany(u => u.Alerts)
            .HasForeignKey(a => a.UserId);

        modelBuilder.Entity<Alert>()
            .HasOne(a => a.PublicAgent)
            .WithMany(pa => pa.Alerts)
            .HasForeignKey(a => a.PublicAgentId);

        modelBuilder.Entity<Alert>()
            .HasOne(a => a.AlertType)
            .WithMany(at => at.Alerts)
            .HasForeignKey(a => a.AlertTypeId);

        modelBuilder.Entity<Image>()
            .HasOne(i => i.Alert)
            .WithMany(a => a.Images)
            .HasForeignKey(i => i.AlertId);

        modelBuilder.Entity<PublicAgent>()
            .HasOne(pa => pa.AgentType)
            .WithMany(at => at.PublicAgents)
            .HasForeignKey(pa => pa.AgentTypeId);

        modelBuilder.Entity<User>()
            .Property(u => u.Birthdate)
            .HasColumnType("timestamp without time zone")
            .HasDefaultValueSql("CURRENT_TIMESTAMP");
    }
}
