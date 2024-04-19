using Rabiscado.Application.Dtos.V1.Plan;
using Rabiscado.Application.Dtos.V1.Subscription;
using Rabiscado.Application.Dtos.V1.User;
using Rabiscado.Application.Dtos.V1.UserPlanSubscriptions;

namespace Rabiscado.Application.Dtos.V1.Users;

public class UserDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Cpf { get; set; } = null!;
    public string Phone { get; set; } = null!;
    public string Cep { get; set; } = null!;
    public string Email { get; set; } = null!;
    public decimal Coin { get; set; }
    public bool Active { get; set; }
    public bool IsAdmin { get; set; }
    public bool IsProfessor { get; set; }
    public int? PlanId { get; set; }
    public PlanDto? Plan { get; set; }
    public List<SubscriptionDto> Subscriptions { get; set; } = new();
    public List<UserClassDTO> UserClasses { get; set; } = new();
    public List<UserPlanSubscriptionDto> UserPlanSubscriptions { get; set; } = new();
}