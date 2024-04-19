using Rabiscado.Application.Adapters.Assas.Application.Dtos.V1.Payments;

namespace Rabiscado.Application.Dtos.V1.Plan;

public class SubscribePlanDto
{
    public int UserId { get; set; }
    public int PlanId { get; set; }
    public CreditCard CreditCard { get; set; } = null!;
    public CreditCardHolderInfo CreditCardHolderInfo { get; set; } = null!;
}