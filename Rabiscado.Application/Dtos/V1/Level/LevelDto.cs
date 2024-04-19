namespace Rabiscado.Application.Dtos.V1.Level;

public class LevelDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public bool Disabled { get; set; }
}