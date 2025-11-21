using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PerifaFlow.Domain.Entities;

namespace PerifaFlowReal.Infastructure.Percistence.Mapping;

public class MissaoMapping : IEntityTypeConfiguration<Missao>
{
    public void Configure(EntityTypeBuilder<Missao> builder)
    {
        builder.ToTable("Missao");
        
        builder.HasKey(m => m.Id);
        
        builder.Property(m => m.Titulo)
            .HasMaxLength(100)
            .IsRequired();
        
        builder.Property(m => m.Descricao)
            .HasMaxLength(100)
            .IsRequired();
        
        builder.HasOne(m => m.Trilha)
            .WithMany(t => t.Missao)
            .HasForeignKey(m => m.TrilhaId);
    }
}