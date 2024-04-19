using AutoMapper;
using Rabiscado.Application.Contracts;
using Rabiscado.Application.Dtos.V1.Base;
using Rabiscado.Application.Dtos.V1.Extracts;
using Rabiscado.Application.Notifications;
using Rabiscado.Core.Enums;
using Rabiscado.Domain.Contracts.Repositories;
using Rabiscado.Domain.Entities;

namespace Rabiscado.Application.Services;

public class ExtractService : BaseService, IExtractService
{
    private readonly IExtractRepository _extractRepository;
    public ExtractService(IMapper mapper, INotificator notificator, IExtractRepository extractRepository) : base(mapper, notificator)
    {
        _extractRepository = extractRepository;
    }

    public async Task<ExtractDto?> GetById(int id)
    {
        var extract = await _extractRepository.GetById(id);
        if (extract is not null) return Mapper.Map<ExtractDto>(extract);
        Notificator.HandleNotFoundResourse();
        return null;
    }

    public async Task<List<ExtractDto>> GetAll()
    {
        var extracts = await _extractRepository.GetAll();
        return Mapper.Map<List<ExtractDto>>(extracts);
    }

    public async Task<PagedDto<ExtractDto>> Search(SearchExtractDto dto)
    {
        var extracts = await _extractRepository.Search(dto);
        return Mapper.Map<PagedDto<ExtractDto>>(extracts);
    }

    public async Task<ExtractDto?> Create(CreateExtractDto dto)
    {
        var extract = Mapper.Map<Extract>(dto);
        if (!await Validate(extract)) return null;
        
        _extractRepository.Create(extract);
        if (await _extractRepository.UnitOfWork.Commit())
        {
            return Mapper.Map<ExtractDto>(extract);
        }
        
        Notificator.Handle("An error occurred while saving the extract.");
        return null;
    }

    public async Task<ExtractDto?> Update(int id, UpdateExtractDto dto)
    {
        if (id != dto.Id)
        {
            Notificator.Handle("The IDs provided are different!.");
            return null;
        }
        
        var extract = await _extractRepository.GetById(id);
        if (extract is null)
        {
            Notificator.HandleNotFoundResourse();
            return null;
        }
        
        Mapper.Map(dto, extract);
        if (!await Validate(extract)) return null;
        
        _extractRepository.Update(extract);
        if (await _extractRepository.UnitOfWork.Commit())
        {
            return Mapper.Map<ExtractDto>(extract);
        }
        
        Notificator.Handle("An error occurred while saving the extract.");
        return null;
    }

    public async Task<bool> Disable(int id)
    {
        var extract = await _extractRepository.GetById(id);
        if (extract is null)
        {
            Notificator.HandleNotFoundResourse();
            return false;
        }
        
        _extractRepository.Disable(extract);
        if (await _extractRepository.UnitOfWork.Commit())
        {
            return true;
        }
        
        Notificator.Handle("An error occurred while disabling the extract.");
        return false;
    }

    public async Task<bool> Active(int id)
    {
        var extract = await _extractRepository.GetById(id);
        if (extract is null)
        {
            Notificator.HandleNotFoundResourse();
            return false;
        }
        
        _extractRepository.Active(extract);
        if (await _extractRepository.UnitOfWork.Commit())
        {
            return true;
        }
        
        Notificator.Handle("An error occurred while enabling the extract.");
        return false;
    }
    
    private async Task<bool> Validate(Extract extract)
    {
        if (!extract.Validate(out var validationResult))
        {
            Notificator.Handle(validationResult.Errors);
        }

        if (!Enum.IsDefined(typeof(EExtractType), extract.Type))
        {
            Notificator.Handle("Invalid extract type.");
        }

        if (await _extractRepository.Any(e => e.Id != extract.Id && e.Course.Value == extract.Course.Value && e.CreateAt == extract.CreateAt))
        {
            Notificator.Handle("There is already an extract with the same value and date.");
        }
        
        return !Notificator.HasNotification;
    }
}