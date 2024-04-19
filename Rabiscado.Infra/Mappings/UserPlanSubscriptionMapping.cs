using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rabiscado.Domain.Entities;

namespace Rabiscado.Infra.Mappings;

public class UserPlanSubscriptionMapping : IEntityTypeConfiguration<UserPlanSubscription>
{
    public void Configure(EntityTypeBuilder<UserPlanSubscription> builder)
    {
        builder
            .Property(up => up.SubscriptionEnd)
            .IsRequired(false);
        
        builder
            .HasOne(up => up.Plan)
            .WithMany(p => p.UserPlanSubscriptions)
            .HasForeignKey(up => up.PlanId)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder
            .HasOne(up => up.User)
            .WithMany(p => p.UserPlanSubscriptions)
            .HasForeignKey(up => up.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}