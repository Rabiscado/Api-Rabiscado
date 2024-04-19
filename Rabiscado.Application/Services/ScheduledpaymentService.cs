using AutoMapper;
using Rabiscado.Application.Contracts;
using Rabiscado.Application.Dtos.V1.Base;
using Rabiscado.Application.Dtos.V1.Scheduledpayments;
using Rabiscado.Application.Notifications;
using Rabiscado.Core.Enums;
using Rabiscado.Domain.Contracts.Repositories;
using Rabiscado.Domain.Entities;

namespace Rabiscado.Application.Services;

public class ScheduledpaymentService : BaseService, IScheduledpaymentService
{
    private readonly IScheduledpaymentRepository _scheduledpaymentRepository;
    private readonly IUserRepository _userRepository;
    private readonly IExtractRepository _extractRepository;
    public ScheduledpaymentService(IMapper mapper, INotificator notificator, IScheduledpaymentRepository scheduledpaymentRepository, IUserRepository userRepository, IExtractRepository extractRepository) : base(mapper, notificator)
    {
        _scheduledpaymentRepository = scheduledpaymentRepository;
        _userRepository = userRepository;
        _extractRepository = extractRepository;
    }

    public async Task<List<ScheduledpaymentDto>> GetAll()
    {
        var scheduledpayments = await _scheduledpaymentRepository.GetAll();
        var scheduledpaymentDtos = new List<ScheduledpaymentDto>();
        foreach (var scheduledpayment in scheduledpayments)
        {
            var professor = await _userRepository.GetById(scheduledpayment.UserId);
            if (professor is null) continue;
            var scheduledpaymentDto = Mapper.Map<ScheduledpaymentDto>(scheduledpayment);
            scheduledpaymentDto.ProfessorName = professor.Name;
            scheduledpaymentDtos.Add(scheduledpaymentDto);
        }
        
        return scheduledpaymentDtos;
    }

    public async Task<PagedDto<ScheduledpaymentDto>> Search(SearchScheduledpaymentDto dto)
    {
        var scheduledpayments = await _scheduledpaymentRepository.Search(dto);
        return Mapper.Map<PagedDto<ScheduledpaymentDto>>(scheduledpayments);
    }

    public async Task<ScheduledpaymentDto?> GetById(int id)
    {
        var scheduledpayment = await _scheduledpaymentRepository.GetById(id);
        if (scheduledpayment is not null)
        {
            var professor = await _userRepository.GetById(scheduledpayment.UserId);
            if (professor is not null)
            {
                var scheduledpaymentDto = Mapper.Map<ScheduledpaymentDto>(scheduledpayment);
                scheduledpaymentDto.ProfessorName = professor.Name;
                return scheduledpaymentDto;
            }
            
            Notificator.Handle("Professor not found.");
            return null;
        }
        Notificator.HandleNotFoundResourse();
        return null;
    }

    public async Task<ScheduledpaymentDto?> Create(CreateScheduledpaymentDto dto)
    {
        var scheduledpayment = Mapper.Map<Scheduledpayment>(dto);
        if (!await Validate(scheduledpayment))
        {
            return null;
        }
        
        var professor = await _userRepository.GetById(scheduledpayment.Id);
        if (professor is null)
        {
            Notificator.Handle("Professor not found.");
            return null;
        }

        _scheduledpaymentRepository.Create(scheduledpayment);
        if (await _scheduledpaymentRepository.UnitOfWork.Commit())
        {
            var scheduledpaymentDto = Mapper.Map<ScheduledpaymentDto>(scheduledpayment);
            scheduledpaymentDto.ProfessorName = professor.Name;
            return scheduledpaymentDto;
        }
        
        Notificator.Handle("An error occurred while trying to create the scheduled payment.");
        return null;
    }

    public async Task<ScheduledpaymentDto?> Update(int id, UpdateScheduledpaymentDto dto)
    {
        var scheduledpayment = await _scheduledpaymentRepository.GetById(id);
        if (scheduledpayment is null)
        {
            Notificator.HandleNotFoundResourse();
            return null;
        }

        Mapper.Map(dto, scheduledpayment);
        if (!await Validate(scheduledpayment))
        {
            return null;
        }
        
        var professor = await _userRepository.GetById(scheduledpayment.UserId);
        if (professor is null)
        {
            Notificator.Handle("Professor not found.");
            return null;
        }

        _scheduledpaymentRepository.Update(scheduledpayment);
        if (await _scheduledpaymentRepository.UnitOfWork.Commit())
        {
            var scheduledpaymentDto = Mapper.Map<ScheduledpaymentDto>(scheduledpayment);
            scheduledpaymentDto.ProfessorName = professor.Name;
            return scheduledpaymentDto;
        }
        
        Notificator.Handle("An error occurred while trying to update the scheduled payment.");
        return null;
    }

    public async Task<bool> Disable(int id)
    {
        var scheduledpayment = await _scheduledpaymentRepository.GetById(id);
        if (scheduledpayment is null)
        {
            Notificator.HandleNotFoundResourse();
            return false;
        }
        
        _scheduledpaymentRepository.Disable(scheduledpayment);
        if (await _scheduledpaymentRepository.UnitOfWork.Commit())
        {
            return true;
        }
        
        Notificator.Handle("An error occurred while disabling the scheduled payment.");
        return false;
    }

    public async Task<bool> Active(int id)
    {
        var scheduledpayment = await _scheduledpaymentRepository.GetById(id);
        if (scheduledpayment is null)
        {
            Notificator.HandleNotFoundResourse();
            return false;
        }
        
        _scheduledpaymentRepository.Active(scheduledpayment);
        if (await _scheduledpaymentRepository.UnitOfWork.Commit())
        {
            return true;
        }
        
        Notificator.Handle("An error occurred while enabling the scheduled payment.");
        return false;
    }

    public async Task<bool> MarkAsPaid(int id)
    {
        var scheduledpayment = await _scheduledpaymentRepository.GetById(id);
        if (scheduledpayment is null)
        {
            Notificator.HandleNotFoundResourse();
            return false;
        }
        
        var professor = await _userRepository.GetById(scheduledpayment.UserId);
        if (professor is null)
        {
            Notificator.Handle("Professor not found.");
            return false;
        }
        
        _extractRepository.Create(new Extract
        {
            Value = scheduledpayment.Value,
            Type = (int)EExtractType.Exit,
            ProfessorId = professor.Id,
            UserId = scheduledpayment.UserId,
            CourseId = scheduledpayment.CourseId
        });
        
        _scheduledpaymentRepository.MarkAsPaidOut(scheduledpayment);
        if (await _scheduledpaymentRepository.UnitOfWork.Commit())
        {
            return true;
        }
        
        Notificator.Handle("An error occurred while marking the scheduled payment as paid out.");
        return false;
    }

    public async Task<bool> MarkAsUnPaid(int id)
    {
        var scheduledpayment = await _scheduledpaymentRepository.GetById(id);
        if (scheduledpayment is null)
        {
            Notificator.HandleNotFoundResourse();
            return false;
        }
        
        _scheduledpaymentRepository.MarkAsUnPaid(scheduledpayment);
        if (await _scheduledpaymentRepository.UnitOfWork.Commit())
        {
            return true;
        }
        
        Notificator.Handle("An error occurred while marking the scheduled payment as unpaid.");
        return false;
    }

    private async Task<bool> Validate(Scheduledpayment scheduledpayment)
    {
        if (!scheduledpayment.Validate(out var validationResult))
        {
            Notificator.Handle(validationResult.Errors);
        }

        if (!await _userRepository.Any(c => c.Id == scheduledpayment.UserId && c.IsProfessor))
        {
            Notificator.Handle("The user is not a professor.");
        }
        
        return !Notificator.HasNotification;
    }
}