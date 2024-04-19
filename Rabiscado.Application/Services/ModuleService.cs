using AutoMapper;
using Rabiscado.Application.Contracts;
using Rabiscado.Application.Dtos.V1.Module;
using Rabiscado.Application.Notifications;
using Rabiscado.Domain.Contracts.Repositories;
using Rabiscado.Domain.Entities;

namespace Rabiscado.Application.Services;

public class ModuleService : BaseService, IModuleService
{
    private readonly IModuleRepository _moduleRepository;
    private readonly ICourseRepository _courseRepository;
    public ModuleService(IMapper mapper, INotificator notificator, IModuleRepository moduleRepository, ICourseRepository courseRepository) : base(mapper, notificator)
    {
        _moduleRepository = moduleRepository;
        _courseRepository = courseRepository;
    }

    public async Task<ModuleDto?> GetById(int id)
    {
        var module = await _moduleRepository.GetById(id);
        if (module is not null) return Mapper.Map<ModuleDto>(module);
        Notificator.HandleNotFoundResourse();
        return null;
    }

    public async Task<List<ModuleDto>> GetAll()
    {
        var modules = await _moduleRepository.GetAll();
        return Mapper.Map<List<ModuleDto>>(modules);
    }

    public async Task<ModuleDto?> Create(CreateModuleDto dto)
    {
        var module = Mapper.Map<Module>(dto);
        if (!await Validate(module))
        {
            return null;
        }

        _moduleRepository.Add(module);
        if (await _moduleRepository.UnitOfWork.Commit())
        {
            return Mapper.Map<ModuleDto>(module);
        }
        
        Notificator.Handle("An error occurred while creating the module");
        return null;
    }

    public async Task<ModuleDto?> Update(int id, UpdateModuleDto dto)
    {
        if (id != dto.Id)
        {
            Notificator.Handle("The ids do not match");
            return null;
        }
        
        var module = await _moduleRepository.GetById(dto.Id);
        if (module is null)
        {
            Notificator.HandleNotFoundResourse();
            return null;
        }

        Mapper.Map(dto, module);
        if (!await Validate(module))
        {
            return null;
        }

        _moduleRepository.Update(module);
        if (await _moduleRepository.UnitOfWork.Commit())
        {
            return Mapper.Map<ModuleDto>(module);
        }
        
        Notificator.Handle("An error occurred while updating the module");
        return null;
    }

    public async Task<bool> Disable(int id)
    {
        var module = await _moduleRepository.GetById(id);
        if (module is null)
        {
            Notificator.HandleNotFoundResourse();
            return false;
        }
        
        _moduleRepository.Disable(module);
        if (await _moduleRepository.UnitOfWork.Commit())
        {
            return true;
        }
        
        Notificator.Handle("An error occurred while disabling the module");
        return false;
    }

    public async Task<bool> Active(int id)
    {
        var module = await _moduleRepository.GetById(id);
        if (module is null)
        {
            Notificator.HandleNotFoundResourse();
            return false;
        }
        
        _moduleRepository.Active(module);
        if (await _moduleRepository.UnitOfWork.Commit())
        {
            return true;
        }
        
        Notificator.Handle("An error occurred while enabling the module");
        return false;
    }
    
    private async Task<bool> Validate(Module module)
    {
        if (!module.Validate(out var validationResult))
        {
            Notificator.Handle(validationResult.Errors);
        }

        if (await _moduleRepository.Any(c => c.Id != module.Id && c.Name == module.Name && c.CourseId == module.CourseId))
        {
            Notificator.Handle("There is already a module with this name and course");
        }
        
        if(!await _courseRepository.Any(c => c.Id == module.CourseId))
        {
            Notificator.Handle("Course not found");
        }
        
        return !Notificator.HasNotification;
    }
}