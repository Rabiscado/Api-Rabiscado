using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rabiscado.Domain.Entities;

namespace Rabiscado.Infra.Mappings;

public class ExtractReceiptMapping : IEntityTypeConfiguration<ExtractReceipt>
{
    public void Configure(EntityTypeBuilder<ExtractReceipt> builder)
    {
        builder
            .Property(x => x.Value)
            .HasColumnType("decimal(10,2)")
            .IsRequired();

        builder
            .Property(x => x.UserId)
            .IsRequired();
        
        builder
            .Property(x => x.PlanId)
            .IsRequired();

        builder
            .HasOne(x => x.User)
            .WithMany(x => x.ExtractReceipts)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder
            .HasOne(x => x.Plan)
            .WithMany(x => x.ExtractReceipts)
            .HasForeignKey(x => x.PlanId)
            .OnDelete(DeleteBehavior.Restrict);
        
    }
}