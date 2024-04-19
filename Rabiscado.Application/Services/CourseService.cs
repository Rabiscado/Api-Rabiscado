using AutoMapper;
using Rabiscado.Application.Contracts;
using Rabiscado.Application.Dtos.V1.Base;
using Rabiscado.Application.Dtos.V1.Courses;
using Rabiscado.Application.Notifications;
using Rabiscado.Domain.Contracts.Repositories;
using Rabiscado.Domain.Entities;

namespace Rabiscado.Application.Services;

public class CourseService : BaseService, ICourseService
{
    private readonly ICourseRepository _courseRepository;
    private readonly IUserRepository _userRepository;
    private readonly ILevelRepository _levelRepository;
    private readonly IForWhoRepository _forWhoRepository;
    private readonly IFileService _fileService;

    public CourseService(IMapper mapper, INotificator notificator, ICourseRepository courseRepository, IUserRepository userRepository, ILevelRepository levelRepository, IForWhoRepository forWhoRepository, IFileService fileService) : base(mapper, notificator)
    {
        _courseRepository = courseRepository;
        _userRepository = userRepository;
        _levelRepository = levelRepository;
        _forWhoRepository = forWhoRepository;
        _fileService = fileService;
    }

    public async Task<CourseDto?> GetById(int id)
    {
        var course = await _courseRepository.GetById(id);
        if (course is not null) return Mapper.Map<CourseDto>(course);
        Notificator.HandleNotFoundResourse();
        return null;
    }

    public async Task<List<CourseDto>> GetAll()
    {
        var courses = await _courseRepository.GetAll();
        return Mapper.Map<List<CourseDto>>(courses);
    }

    public async Task<PagedDto<CourseDto>> Search(SearchCourseDto dto)
    {
        var courses = await _courseRepository.Search(dto);
        return Mapper.Map<PagedDto<CourseDto>>(courses);
    }

    public async Task<CourseDto?> Create(CreateCourseDto dto)
    {
        if (dto.LevelIds.Count == 0 || dto.ForWhoIds.Count == 0)
        {
            Notificator.Handle("The course or forWho must have at least one level.");
            return null;
        }
        
        var course = Mapper.Map<Course>(dto);
        if (!await Validate(course))
        {
            return null;
        }
        
        if (dto.Tumb is not null)
        {
            var image = await _fileService.UploadFile(dto.Tumb);
            if (image is null)
            {
                return null;
            }
            
            course.Image = image;
        }
        
        foreach (var levelId in dto.LevelIds)
        {
            var level = await _levelRepository.GetById(levelId);
            if (level != null)
            {
                course.CourseLevels.Add(new CourseLevel { Level = level });
            }
            else
            {
                Notificator.Handle($"There is no level with this id. {levelId}");
                return null;
            }
        }
        
        foreach (var forWhoId in dto.ForWhoIds)
        {
            var forWho = await _forWhoRepository.GetById(forWhoId);
            if (forWho != null)
            {
                course.CourseForWhos.Add(new CourseForWho { ForWho = forWho });
            }
            else
            {
                Notificator.Handle($"There is no forWho with this id. {forWhoId}");
                return null;
            }
        }
        
        _courseRepository.Create(course);
        if (await _courseRepository.UnitOfWork.Commit())
        {
            return Mapper.Map<CourseDto>(course);
        }
        
        Notificator.Handle("An error occurred while creating the course");
        return null;
    }

    public async Task<CourseDto?> Update(int id, UpdateCourseDto dto)
    {
        if (id != dto.Id)
        {
            Notificator.Handle("The ids do not match");
            return null;
        }
        
        var course = await _courseRepository.GetById(dto.Id);
        if (course is null)
        {
            Notificator.HandleNotFoundResourse();
            return null;
        }

        Mapper.Map(dto, course);
        if (!await Validate(course))
        {
            return null;
        }

        if (dto.Tumb is not null)
        {
            if (!course.Image.Contains(dto.Tumb.FileName))
            {
                await _fileService.DeleteDocument(course.Image);
            }
            
            var image = await _fileService.UploadFile(dto.Tumb);
            if (image is null)
            {
                return null;
            }
            
            course.Image = image;
        }
        
        course.CourseLevels.RemoveAll(cl => !dto.LevelIds.Contains(cl.LevelId));
        var existingLevelIds = course.CourseLevels.Select(cl => cl.LevelId).ToList();
        var newLevelIds = dto.LevelIds.Except(existingLevelIds).ToList();

        foreach (var newLevelId in newLevelIds)
        {
            var level = await _levelRepository.GetById(newLevelId);
            if (level != null)
            {
                course.CourseLevels.Add(new CourseLevel { Level = level });
            }
            else
            {
                Notificator.Handle($"There is no level with this id. {newLevelId}");
                return null;
            }
        }
        
        
        course.CourseForWhos.RemoveAll(cf => !dto.ForWhoIds.Contains(cf.ForWhoId));
        var existingForWhos = course.CourseForWhos.Select(cf => cf.ForWhoId).ToList();
        var newForWhos = dto.ForWhoIds.Except(existingForWhos).ToList();

        foreach (var newForWho in newForWhos)
        {
            var forWho = await _forWhoRepository.GetById(newForWho);
            if (forWho != null)
            {
                course.CourseForWhos.Add(new CourseForWho { ForWho = forWho });
            }
            else
            {
                Notificator.Handle($"There is no forWho with this id. {newForWho}");
                return null;
            }
        }

        _courseRepository.Update(course);
        if (await _courseRepository.UnitOfWork.Commit())
        {
            return Mapper.Map<CourseDto>(course);
        }
        
        Notificator.Handle("An error occurred while updating the course");
        return null;
    }

    public async Task<bool> Disable(int id)
    {
        var course = await _courseRepository.GetById(id);
        if (course is null)
        {
            Notificator.HandleNotFoundResourse();
            return false;
        }
        
        _courseRepository.Disable(course);
        if (await _courseRepository.UnitOfWork.Commit())
        {
            return true;
        }
        
        Notificator.Handle("An error occurred while disabling the course.");
        return false;
    }

    public async Task<bool> Active(int id)
    {
        var course = await _courseRepository.GetById(id);
        if (course is null)
        {
            Notificator.HandleNotFoundResourse();
            return false;
        }
        
        _courseRepository.Active(course);
        if (await _courseRepository.UnitOfWork.Commit())
        {
            return true;
        }
        
        Notificator.Handle("An error occurred while disabling the course.");
        return false;
    }

    public async Task Delete(int id)
    {
        var course = await _courseRepository.GetById(id);
        if (course is null)
        {
            Notificator.HandleNotFoundResourse();
            return;
        }
        
        _courseRepository.Delete(course);
        if (!await _courseRepository.UnitOfWork.Commit())
        {
            Notificator.Handle("An error occurred while deleting the course.");
        }
    }

    private async Task<bool> Validate(Course course)
    {
        if (!course.Validate(out var validationResult))
        {
            Notificator.Handle(validationResult.Errors);
        }
        
        if (await _courseRepository.Any(c => c.Id != course.Id && c.Name == course.Name && c.School == course.School && c.ProfessorEmail == course.ProfessorEmail))
        {
            Notificator.Handle($"There is already a {(course.Disabled ? "disabled" : "actived")} course with these identifications.");
        }

        var userProfessor = await _userRepository.FirstOrDefault(u => u.Email == course.ProfessorEmail);
        if (userProfessor is not null)
        {
            if(!userProfessor.IsProfessor)
                Notificator.Handle("The user is not a professor.");
        }
        
        if (userProfessor is null)
        {
            Notificator.Handle("There is no user with this email.");
        }
        
        return !Notificator.HasNotification;
    }
}