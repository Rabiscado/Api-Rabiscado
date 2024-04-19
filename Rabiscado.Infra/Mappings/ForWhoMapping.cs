using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rabiscado.Domain.Entities;

namespace Rabiscado.Infra.Mappings;

public class ForWhoMapping : IEntityTypeConfiguration<ForWho>
{
    public void Configure(EntityTypeBuilder<ForWho> builder)
    {
        builder.Property(l => l.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(l => l.Description)
            .IsRequired()
            .HasMaxLength(500);
    }
}