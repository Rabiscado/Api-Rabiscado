using Rabiscado.Domain.Contracts;

namespace Rabiscado.Domain.Entities;

public class UserPlanSubscription : Entity, ISoftDelete, IAggregateRoot
{
    public int UserId { get; set; }
    public int PlanId { get; set; }
    public User User { get; set; } = null!;
    public Plan Plan { get; set; } = null!;
    public DateTime? SubscriptionEnd { get; set; }
    public bool Disabled { get; set; }
}