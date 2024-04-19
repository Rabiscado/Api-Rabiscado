namespace Rabiscado.Application.Dtos.V1.UserPlanSubscriptions;

public class UserPlanSubscriptionDto
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int PlanId { get; set; }
    public DateTime CreateAt { get; set; }
    public DateTime? SubscriptionEnd { get; set; }
}