using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rabiscado.Domain.Entities;

namespace Rabiscado.Infra.Mappings;

public class UserMapping : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.Property(u => u.Name)
            .IsRequired()
            .HasMaxLength(255);
        
        builder.Property(u => u.Email)
            .IsRequired()
            .HasMaxLength(255);
        
        builder.Property(u => u.Password)
            .IsRequired()
            .HasMaxLength(255);
        
        builder.Property(u => u.Cpf)
            .IsRequired()
            .HasMaxLength(11);
        
        builder.Property(u => u.Phone)
            .IsRequired()
            .HasMaxLength(11);
        
        builder.Property(u => u.Cep)
            .IsRequired()
            .HasMaxLength(8);
        
        builder.Property(c => c.Coin)
            .HasColumnType("decimal(10,2)")
            .IsRequired();
        
        builder.Property(u => u.PlanId)
            .IsRequired(false);

        builder.Property(u => u.IsAdmin)
            .HasDefaultValue(false);

        builder.Property(u => u.IsProfessor)
            .HasDefaultValue(false);
    }
}