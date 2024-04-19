using Rabiscado.Application.Adapters.Assas.Application.Dtos.V1.Payments;
using Rabiscado.Application.Dtos.V1.Plan;

namespace Rabiscado.Application.Contracts;

public interface IPlanService
{
    Task<PlanDto?> GetById(int id);
    Task<List<PlanDto>> GetAll();
    Task<PlanDto?> Create(CreatePlanDto dto);
    Task<PlanDto?> Update(int id, UpdatePlanDto dto);
    Task<bool> Disable(int id);
    Task<bool> Active(int id);
    Task VerifyPayment(SubscriptionHookDto dto);
}