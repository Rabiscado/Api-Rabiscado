namespace Rabiscado.Application.Dtos.V1.Module;

public class UpdateModuleDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public int CourseId { get; set; }
}