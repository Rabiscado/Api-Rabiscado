using Rabiscado.Application.Dtos.V1.Class;

namespace Rabiscado.Application.Dtos.V1.Module;

public class ModuleDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public bool Disabled { get; set; }
    public int CourseId { get; set; }
    public List<ClassDto> Classes { get; set; } = new();
}