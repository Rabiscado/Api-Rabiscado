using Rabiscado.Domain.Contracts;

namespace Rabiscado.Domain.Entities;

public class ForWho : Entity, ISoftDelete, IAggregateRoot 
{
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public bool Disabled { get; set; }
    public List<CourseForWho> CourseForWhos { get; set; } = new();
}