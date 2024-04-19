using Rabiscado.Domain.Contracts;

namespace Rabiscado.Domain.Entities;

public class UserClass : Entity, ISoftDelete, IAggregateRoot
{
    public int UserId { get; set; }
    public int ClassId { get; set; }
    public User User { get; set; } = null!;
    public Class Class { get; set; } = null!;
    public bool Watched { get; set; }
    public bool Disabled { get; set; }
}