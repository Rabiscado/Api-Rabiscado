using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rabiscado.Domain.Entities;

namespace Rabiscado.Infra.Mappings;

public class UserClassMapping : IEntityTypeConfiguration<UserClass>
{
    public void Configure(EntityTypeBuilder<UserClass> builder)
    {
        builder.Property(u => u.UserId)
            .IsRequired();
        builder.Property(u => u.ClassId)
            .IsRequired();
        builder.Property(u => u.Watched)
            .HasDefaultValue(false)
            .IsRequired();
        builder.HasOne(u => u.User)
            .WithMany(c => c.UserClasses)
            .HasForeignKey(u => u.UserId)
            .OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(u => u.Class)
            .WithMany(c => c.UserClasses)
            .HasForeignKey(u => u.ClassId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}