namespace Rabiscado.Application.Dtos.V1.Plan;

public class PlanDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public decimal CoinValue { get; set; }
    public bool Disabled { get; set; }
}