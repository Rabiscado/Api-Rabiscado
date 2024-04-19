using Rabiscado.Domain.Contracts;

namespace Rabiscado.Domain.Entities;

public class BaseEntity : IEntity
{
    public int Id { get; set; }
}