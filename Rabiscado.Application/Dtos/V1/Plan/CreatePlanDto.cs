namespace Rabiscado.Application.Dtos.V1.Plan;

public class CreatePlanDto
{
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public decimal CoinValue { get; set; }
}