using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rabiscado.Domain.Entities;

namespace Rabiscado.Infra.Mappings;

public class ExtractMapping : IEntityTypeConfiguration<Extract>
{
    public void Configure(EntityTypeBuilder<Extract> builder)
    {
        builder
            .Property(e => e.Type)
            .IsRequired();

        builder
            .Property(e => e.UserId)
            .IsRequired();
        
        builder
            .Property(e => e.CourseId)
            .IsRequired();
        
        builder
            .Property(e => e.ProfessorId)
            .IsRequired();

        builder
            .HasOne(e => e.User)
            .WithMany(u => u.Extracts)
            .HasForeignKey(e => e.UserId)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder
            .HasOne(e => e.Course)
            .WithMany(u => u.Extracts)
            .HasForeignKey(e => e.CourseId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}