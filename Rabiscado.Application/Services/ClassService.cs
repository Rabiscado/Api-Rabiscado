using AutoMapper;
using Rabiscado.Application.Contracts;
using Rabiscado.Application.Dtos.V1.Class;
using Rabiscado.Application.Notifications;
using Rabiscado.Domain.Contracts.Repositories;
using Rabiscado.Domain.Entities;

namespace Rabiscado.Application.Services;

public class ClassService : BaseService, IClassService
{
    private readonly IClassRepository _classRepository;
    private readonly IModuleRepository _moduleRepository;
    private readonly IFileService _fileService;

    public ClassService(IMapper mapper, INotificator notificator, IClassRepository classRepository, IModuleRepository moduleRepository, IFileService fileService) : base(mapper, notificator)
    {
        _classRepository = classRepository;
        _moduleRepository = moduleRepository;
        _fileService = fileService;
    }

    public async Task<ClassDto?> GetById(int id)
    {
        var classe = await _classRepository.GetById(id);
        if(classe is not null) return Mapper.Map<ClassDto>(classe);
        Notificator.HandleNotFoundResourse();
        return null;
    }

    public async Task<List<ClassDto>> GetAll()
    {
        var classes = await _classRepository.GetAll();
        return Mapper.Map<List<ClassDto>>(classes);
    }

    public async Task<ClassDto?> Create(CreateClassDto dto)
    {
        var classe = Mapper.Map<Class>(dto);
        if (!await Validate(classe))
        {
            return null;
        }

        if (dto.TumbImage is not null)
        {
            var tumbUrl = await _fileService.UploadFile(dto.TumbImage);
            if (tumbUrl is null)
            {
                return null;
            }
            
            classe.Tumb = tumbUrl;
        }
        
        if (dto.GifClass is not null)
        {
            var gifUrl = await _fileService.UploadFile(dto.GifClass);
            if (gifUrl is null)
            {
                return null;
            }
            
            classe.Gif = gifUrl;
        }

        if (dto.ImagesClass.Any())
        {
            var images = new List<Step>();
            foreach (var image in dto.ImagesClass)
            {
                var imageUrl = await _fileService.UploadFile(image);
                if (imageUrl is null)
                {
                    return null;
                }
                
                images.Add(new Step { Url = imageUrl });
            }
            classe.Steps = images;
        }
        
        _classRepository.Add(classe);
        var module = await _moduleRepository.GetById(classe.ModuleId);
        if (module!.Course.Subscriptions.Any())
        {
            foreach (var subscription in module.Course.Subscriptions)
            {
                subscription.User.UserClasses.Add(new UserClass { Class = classe });
            }
        }
        
        if (await _classRepository.UnitOfWork.Commit())
        {
            return Mapper.Map<ClassDto>(classe);
        }
        
        Notificator.Handle("An error occurred while saving the class.");
        return null;
    }

    public async Task<ClassDto?> Update(int id, UpdateClassDto dto)
    {
        if (id != dto.Id)
        {
            Notificator.Handle("The IDs provided are different!.");
            return null;
        }
        
        var classe = await _classRepository.GetById(id);
        if (classe is null)
        {
            Notificator.HandleNotFoundResourse();
            return null;
        }
        
        Mapper.Map(dto, classe);
        if (!await Validate(classe))
        {
            return null;
        }
        
        if (dto.TumbImage is not null && !classe.Tumb.Contains(dto.TumbImage.FileName))
        {
            var tumbUrl = await _fileService.UploadFile(dto.TumbImage);
            if (tumbUrl is null)
            {
                return null;
            }
            
            classe.Tumb = tumbUrl;
        }
        
        if (dto.GifClass is not null && classe.Gif is not null && !classe.Gif.Contains(dto.GifClass.FileName))
        {
            var gifUrl = await _fileService.UploadFile(dto.GifClass);
            if (gifUrl is null)
            {
                return null;
            }
            
            classe.Gif = gifUrl;
        }
        
        if (dto.ImagesClass.Any())
        {
            if (classe.Steps.Any())
            {
                var imagesToRemove = classe.Steps.Where(c => dto.ImagesClass.All(i => !c.Url.Contains(i.FileName))).ToList();
                foreach (var image in imagesToRemove)
                {
                    await _fileService.DeleteDocument(image.Url);
                    classe.Steps.Remove(image);
                }
            }
            
            var imagesToAdd = dto.ImagesClass.Where(i => classe.Steps.All(c => !c.Url.Contains(i.FileName))).ToList();
            if (imagesToAdd.Any())
            {
                var images = new List<Step>();
                foreach (var image in imagesToAdd)
                {
                    var imageUrl = await _fileService.UploadFile(image);
                    if (imageUrl is null)
                    {
                        return null;
                    }
                    
                    images.Add(new Step { Url = imageUrl });
                }
                classe.Steps.AddRange(images);
            }
        }
        
        _classRepository.Update(classe);
        if (await _classRepository.UnitOfWork.Commit())
        {
            return Mapper.Map<ClassDto>(classe);
        }
        
        Notificator.Handle("An error occurred while updating the class.");
        return null;
    }

    public async Task<bool> Disable(int id)
    {
        var classe = await _classRepository.GetById(id);
        if (classe is null)
        {
            Notificator.HandleNotFoundResourse();
            return false;
        }
        
        _classRepository.Disable(classe);
        if (await _classRepository.UnitOfWork.Commit())
        {
            return true;
        }
        
        Notificator.Handle("An error occurred while disabling the class.");
        return false;
    }

    public async Task<bool> Active(int id)
    {
        var classe = await _classRepository.GetById(id);
        if (classe is null)
        {
            Notificator.HandleNotFoundResourse();
            return false;
        }
        
        _classRepository.Active(classe);
        if (await _classRepository.UnitOfWork.Commit())
        {
            return true;
        }
        
        Notificator.Handle("An error occurred while enabling the class.");
        return false;
    }

    private async Task<bool> Validate(Class @class)
    {
        if (!@class.Validate(out var validationResult))
        {
            Notificator.Handle(validationResult.Errors);
        }

        if (await _classRepository.Any(c => c.Id != @class.Id && c.Name == @class.Name && c.ModuleId == @class.ModuleId))
        {
            Notificator.Handle("There is already a class with this name in this module.");
        }

        if (!await _moduleRepository.Any(c => c.Id == @class.ModuleId))
        {
            Notificator.Handle("Module not found.");
        }
        
        return !Notificator.HasNotification;
    }
}