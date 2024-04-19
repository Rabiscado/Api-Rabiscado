namespace Rabiscado.Application.Dtos.V1.Step;

public class UpdateStepDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Url { get; set; } = null!;
    public int ClassId { get; set; }
}