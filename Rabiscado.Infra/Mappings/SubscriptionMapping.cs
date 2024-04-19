using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rabiscado.Domain.Entities;

namespace Rabiscado.Infra.Mappings;

public class SubscriptionMapping : IEntityTypeConfiguration<Subscription>
{
    public void Configure(EntityTypeBuilder<Subscription> builder)
    {
        builder
            .Property(c => c.CourseId)
            .IsRequired();

        builder
            .Property(c => c.UserId)
            .IsRequired();
        
        builder
            .Property(c => c.IsHide)
            .HasDefaultValue(false)
            .IsRequired();

        builder
            .HasOne(c => c.User)
            .WithMany(c => c.Subscriptions)
            .HasForeignKey(c => c.UserId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder
            .HasOne(c => c.Course)
            .WithMany(c => c.Subscriptions)
            .HasForeignKey(c => c.CourseId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}