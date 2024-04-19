using Rabiscado.Domain.Contracts;

namespace Rabiscado.Domain.Entities;

public class Reimbursement : Entity, ISoftDelete, IAggregateRoot
{
    public int UserId { get; set; }
    public User User { get; set; } = null!;
    public bool Disabled { get; set; }
}