using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PerifaFlow.Domain.Entities;

namespace PerifaFlowReal.Infastructure.Percistence.Mapping;

public class EntregaMapping : IEntityTypeConfiguration<Entrega>
{
    public void Configure(EntityTypeBuilder<Entrega> builder)
    {
        builder.ToTable("Entrega");
        
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Tipo)
            .HasConversion<int>()
            .IsRequired();

        builder.Property(e => e.ConteudoUrl)
            .IsRequired();


        builder.HasOne(e => e.Portfolio)
            .WithMany(p => p.Entrega)
            .HasForeignKey(e => e.PortfolioId)
            .OnDelete(DeleteBehavior.Restrict);    
        
        builder.HasOne(e => e.Missao)
            .WithOne(p => p.Entrega)
            .HasForeignKey<Entrega>(e => e.MissaoID)
            .OnDelete(DeleteBehavior.Restrict); 
        
        builder.HasOne(e => e.User)
            .WithMany(u => u.Entrega)
            .HasForeignKey(e => e.UserID)
            .OnDelete(DeleteBehavior.Restrict); 
    }
}