using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rabiscado.Domain.Entities;

namespace Rabiscado.Infra.Mappings;

public class ScheduledpaymentMapping : IEntityTypeConfiguration<Scheduledpayment>
{
    public void Configure(EntityTypeBuilder<Scheduledpayment> builder)
    {
        builder
            .Property(s => s.PaidOut)
            .HasDefaultValue(false)
            .IsRequired();
        
        builder
            .Property(s => s.UserId)
            .IsRequired();
        
        builder
            .Property(s => s.CourseId)
            .IsRequired();
        
        builder
            .Property(s => s.Value)
            .IsRequired()
            .HasColumnType("decimal(10,2)");

        builder
            .HasOne(s => s.User)
            .WithMany(u => u.Scheduledpayments)
            .HasForeignKey(s => s.UserId)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder
            .HasOne(s => s.Course)
            .WithMany(u => u.Scheduledpayments)
            .HasForeignKey(s => s.CourseId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}