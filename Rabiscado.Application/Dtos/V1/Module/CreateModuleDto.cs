namespace Rabiscado.Application.Dtos.V1.Module;

public class CreateModuleDto
{
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public int CourseId { get; set; }
}