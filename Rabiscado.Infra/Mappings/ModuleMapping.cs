using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rabiscado.Domain.Entities;

namespace Rabiscado.Infra.Mappings;

public class ModuleMapping : IEntityTypeConfiguration<Module>
{
    public void Configure(EntityTypeBuilder<Module> builder)
    {
        builder
            .Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(255);

        builder
            .Property(x => x.Description)
            .IsRequired()
            .HasMaxLength(500);

        builder
            .Property(x => x.CourseId)
            .IsRequired();

        builder
            .HasOne(m => m.Course)
            .WithMany(c => c.Modules)
            .HasForeignKey(x => x.CourseId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}