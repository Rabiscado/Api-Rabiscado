using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rabiscado.Domain.Entities;

namespace Rabiscado.Infra.Mappings;

public class CourseForWhoMapping : IEntityTypeConfiguration<CourseForWho>
{
    public void Configure(EntityTypeBuilder<CourseForWho> builder)
    {
        builder.ToTable("CourseForWhos");
        
        builder.Property(cf => cf.CourseId)
            .IsRequired();
        
        builder.Property(cf => cf.ForWhoId)
            .IsRequired();

        builder
            .HasOne(cf => cf.Course)
            .WithMany(c => c.CourseForWhos)
            .HasForeignKey(cf => cf.CourseId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder
            .HasOne(cf => cf.ForWho)
            .WithMany(l => l.CourseForWhos)
            .HasForeignKey(cf => cf.ForWhoId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}