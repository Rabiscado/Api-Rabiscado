using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rabiscado.Domain.Entities;

namespace Rabiscado.Infra.Mappings;

public class ClasseMapping : IEntityTypeConfiguration<Class>
{
    public void Configure(EntityTypeBuilder<Class> builder)
    {
        builder.Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(255);
        
        builder
            .Property(p => p.Description)
            .IsRequired(false)
            .HasMaxLength(500);
        
        builder.Property(p => p.Video)
            .IsRequired(false)
            .HasMaxLength(500);
        
        builder.Property(p => p.Music)
            .IsRequired(false)
            .HasMaxLength(500);
        
        builder.Property(p => p.Tumb)
            .IsRequired(false)
            .HasMaxLength(500);
        
        builder.Property(p => p.ModuleId)
            .IsRequired();
        
        builder
            .HasOne(p => p.Module)
            .WithMany(m => m.Classes)
            .HasForeignKey(p => p.ModuleId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}