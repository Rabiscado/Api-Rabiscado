using Rabiscado.Domain.Contracts;

namespace Rabiscado.Domain.Entities;

public class Subscription : Entity, ISoftDelete, IAggregateRoot
{
    public int UserId { get; set; }
    public int CourseId { get; set; }
    public User User { get; set; } = null!;
    public Course Course { get; set; } = null!;
    public DateTime? SubscriptionEnd { get; set; }
    public bool IsHide { get; set; }
    public bool Disabled { get; set; }
}