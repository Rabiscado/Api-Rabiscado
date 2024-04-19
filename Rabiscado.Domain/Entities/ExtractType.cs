using Rabiscado.Domain.Contracts;

namespace Rabiscado.Domain.Entities;

public class ExtractType : Entity, ISoftDelete
{
    public string Name { get; set; } = null!;
    public bool Disabled { get; set; }
}