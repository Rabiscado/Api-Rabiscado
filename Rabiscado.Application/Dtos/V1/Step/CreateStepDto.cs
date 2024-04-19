namespace Rabiscado.Application.Dtos.V1.Step;

public class CreateStepDto
{
    public string Name { get; set; } = null!;
    public string Url { get; set; } = null!;
    public int ClassId { get; set; }
}