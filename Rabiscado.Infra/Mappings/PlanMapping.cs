using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rabiscado.Domain.Entities;

namespace Rabiscado.Infra.Mappings;

public class PlanMapping : IEntityTypeConfiguration<Plan>
{
    public void Configure(EntityTypeBuilder<Plan> builder)
    {
        builder.Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(255);
        
        builder
            .Property(p => p.Description)
            .IsRequired(false)
            .HasMaxLength(500);
        
        builder.Property(p => p.Price)
            .HasColumnType("decimal(10,2)")
            .IsRequired();
        
        builder.Property(p => p.CoinValue)
            .HasColumnType("decimal(10,2)")
            .IsRequired();

        builder
            .HasMany(p => p.Users)
            .WithOne(u => u.Plan)
            .HasForeignKey(u => u.PlanId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}