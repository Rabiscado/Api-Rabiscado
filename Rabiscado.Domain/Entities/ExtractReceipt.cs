using Rabiscado.Domain.Contracts;

namespace Rabiscado.Domain.Entities;

public class ExtractReceipt : Entity, ISoftDelete, IAggregateRoot
{
    public decimal Value { get; set; }
    public int UserId { get; set; }
    public int PlanId { get; set; }
    public User User { get; set; } = null!;
    public Plan Plan { get; set; } = null!;
    public bool Disabled { get; set; }
}