using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PerifaFlow.Domain.Entities;

namespace PerifaFlowReal.Infastructure.Percistence.Mapping;

public class TrilhaMapping : IEntityTypeConfiguration<Trilha>
{
    public void Configure(EntityTypeBuilder<Trilha> builder)
    {
        builder.ToTable("Trilha");
        
        builder.HasKey(t => t.Id);

        builder.Property(t => t.Titulo)
            .HasMaxLength(100)
            .IsRequired();
        
        builder.Property(t => t.Descricao)
            .HasMaxLength(100)
            .IsRequired();
    }
}