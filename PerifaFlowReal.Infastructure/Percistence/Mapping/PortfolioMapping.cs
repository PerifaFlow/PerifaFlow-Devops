using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PerifaFlow.Domain.Entities;

namespace PerifaFlowReal.Infastructure.Percistence.Mapping;

public class PortfolioMapping : IEntityTypeConfiguration<Portfolio>
{
    public void Configure(EntityTypeBuilder<Portfolio> builder)
    {
        builder.ToTable("Portfolios");
        
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Titulo)
            .HasMaxLength(100)
            .IsRequired();
        
        builder.Property(p => p.Url)
            .IsRequired();

        builder.HasOne(p => p.User)
            .WithOne(u => u.Portfolio)
            .HasForeignKey<Portfolio>(u => u.UserID);
        
        
    }
}