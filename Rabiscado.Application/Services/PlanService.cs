using AutoMapper;
using Rabiscado.Application.Adapters.Assas.Application.Contracts;
using Rabiscado.Application.Adapters.Assas.Application.Dtos.V1.Payments;
using Rabiscado.Application.Contracts;
using Rabiscado.Application.Dtos.V1.Plan;
using Rabiscado.Application.Notifications;
using Rabiscado.Domain.Contracts.Repositories;
using Rabiscado.Domain.Entities;

namespace Rabiscado.Application.Services;

public class PlanService : BaseService, IPlanService
{
    private readonly IPlanRepository _planRepository;
    private readonly IPaymentService _paymentService;
    public PlanService(IMapper mapper, INotificator notificator, IPlanRepository planRepository, IPaymentService paymentService) : base(mapper, notificator)
    {
        _planRepository = planRepository;
        _paymentService = paymentService;
    }

    public async Task<PlanDto?> GetById(int id)
    {
        var plan = await _planRepository.GetById(id);
        if (plan is not null) return Mapper.Map<PlanDto>(plan);
        Notificator.HandleNotFoundResourse();
        return null;
    }

    public async Task<List<PlanDto>> GetAll()
    {
        var plans = await _planRepository.GetAll();
        return Mapper.Map<List<PlanDto>>(plans);
    }

    public async Task<PlanDto?> Create(CreatePlanDto dto)
    {
        var plan = Mapper.Map<Plan>(dto);
        if (!await Validate(plan))
        {
            return null;
        }

        _planRepository.Add(plan);
        if (await _planRepository.UnitOfWork.Commit())
        {
            return Mapper.Map<PlanDto>(plan);
        }
        
        Notificator.Handle("An error occurred while creating the plan");
        return null;
    }

    public async Task<PlanDto?> Update(int id, UpdatePlanDto dto)
    {
        if (id != dto.Id)
        {
            Notificator.Handle("The ids do not match");
            return null;
        }
        
        var plan = await _planRepository.GetById(dto.Id);
        if (plan is null)
        {
            Notificator.HandleNotFoundResourse();
            return null;
        }

        Mapper.Map(dto, plan);
        if (!await Validate(plan))
        {
            return null;
        }

        _planRepository.Update(plan);
        if (await _planRepository.UnitOfWork.Commit())
        {
            return Mapper.Map<PlanDto>(plan);
        }
        
        Notificator.Handle("An error occurred while updating the plan");
        return null;
    }

    public async Task<bool> Disable(int id)
    {
        var plan = await _planRepository.GetById(id);
        if (plan is null)
        {
            Notificator.HandleNotFoundResourse();
            return false;
        }
        
        _planRepository.Disable(plan);
        if (await _planRepository.UnitOfWork.Commit())
        {
            return true;
        }
        
        Notificator.Handle("An error occurred while disabling the plan");
        return false;
    }

    public async Task<bool> Active(int id)
    {
        var plan = await _planRepository.GetById(id);
        if (plan is null)
        {
            Notificator.HandleNotFoundResourse();
            return false;
        }
        
        _planRepository.Active(plan);
        if (await _planRepository.UnitOfWork.Commit())
        {
            return true;
        }
        
        Notificator.Handle("An error occurred while disabling the plan");
        return false;
    }

    public async Task VerifyPayment(SubscriptionHookDto dto)
    {
        await _paymentService.VerifyPayment(dto);
    }

    private async Task<bool> Validate(Plan plan)
    {
        if (!plan.Validate(out var validationResult))
        {
            Notificator.Handle(validationResult.Errors);
        }

        if (await _planRepository.Any(c => c.Id != plan.Id && c.Name == plan.Name))
        {
            Notificator.Handle($"There is already a {(plan.Disabled ? "disabled" : "activated")} plan with this name.");
        }
        
        return !Notificator.HasNotification;
    }
}