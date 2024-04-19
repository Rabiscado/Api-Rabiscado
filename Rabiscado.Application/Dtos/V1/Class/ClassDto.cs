using Rabiscado.Application.Dtos.V1.Step;

namespace Rabiscado.Application.Dtos.V1.Class;

public class ClassDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public string? Video { get; set; }
    public string? Music { get; set; }
    public string? Tumb { get; set; }
    public string? Gif { get; set; }
    public int ModuleId { get; set; }
    public List<StepDto> Steps { get; set; } = new();
}