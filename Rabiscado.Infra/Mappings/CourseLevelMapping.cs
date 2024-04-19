using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rabiscado.Domain.Entities;

namespace Rabiscado.Infra.Mappings;

public class CourseLevelMapping : IEntityTypeConfiguration<CourseLevel>
{
    public void Configure(EntityTypeBuilder<CourseLevel> builder)
    {
        
        builder.ToTable("CourseLevels");
        
        builder.Property(cl => cl.CourseId)
            .IsRequired();
        
        builder.Property(cl => cl.LevelId)
            .IsRequired();

        builder
            .HasOne(cl => cl.Course)
            .WithMany(c => c.CourseLevels)
            .HasForeignKey(cl => cl.CourseId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder
            .HasOne(cl => cl.Level)
            .WithMany(l => l.CourseLevels)
            .HasForeignKey(cl => cl.LevelId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}