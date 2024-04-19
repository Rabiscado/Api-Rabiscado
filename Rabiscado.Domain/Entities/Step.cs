namespace Rabiscado.Domain.Entities;

public class Step : Entity
{
    public string Url { get; set; } = null!;
    public int ClassId { get; set; }
    public Class Class { get; set; } = null!;
}