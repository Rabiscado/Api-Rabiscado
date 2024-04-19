namespace Rabiscado.Application.Dtos.V1.ForWho;

public class ForWhoDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public bool Disabled { get; set; }
}