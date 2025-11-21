using Microsoft.EntityFrameworkCore;
using PerifaFlow.Domain.Entities;
using PerifaFlowReal.Infastructure.Percistence.Mapping;

namespace PerifaFlowReal.Infastructure.Percistence.Context;

public class PerifaFlowContext(DbContextOptions<PerifaFlowContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }
    public DbSet<Trilha> Trilhas { get; set; }
    public DbSet<Entrega> Entregas { get; set; }
    public DbSet<Missao> Missao { get; set; }
    public DbSet<Portfolio> Portfolio { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new UserMapping());
        modelBuilder.ApplyConfiguration(new TrilhaMapping());
        modelBuilder.ApplyConfiguration(new MissaoMapping());
        modelBuilder.ApplyConfiguration(new EntregaMapping());
        modelBuilder.ApplyConfiguration(new PortfolioMapping());
    }
}