using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rabiscado.Domain.Entities;

namespace Rabiscado.Infra.Mappings;

public class CourseMapping : IEntityTypeConfiguration<Course>
{
    public void Configure(EntityTypeBuilder<Course> builder)
    {
        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(255);
        
        builder.Property(x => x.ProfessorEmail)
            .IsRequired(false)
            .HasMaxLength(255);
        
        builder.Property(x => x.Description)
            .IsRequired(false)
            .HasMaxLength(500);
        
        builder.Property(x => x.Image)
            .IsRequired(false)
            .HasMaxLength(500);
        
        builder.Property(x => x.Video)
            .IsRequired(false)
            .HasMaxLength(500);
        
        builder.Property(x => x.Value)
            .HasColumnType("decimal(10,2)")
            .IsRequired();
        
        builder.Property(x => x.Style)
            .IsRequired(false)
            .HasMaxLength(255);
        
        builder.Property(x => x.School)
            .IsRequired(false)
            .HasMaxLength(255);
        
        builder.Property(x => x.Localization)
            .IsRequired(false)
            .HasMaxLength(255);
    }
}