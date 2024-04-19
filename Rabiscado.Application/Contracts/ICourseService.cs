using Rabiscado.Application.Dtos.V1.Base;
using Rabiscado.Application.Dtos.V1.Courses;

namespace Rabiscado.Application.Contracts;

public interface ICourseService
{
    Task<CourseDto?> GetById(int id);
    Task<List<CourseDto>> GetAll();
    Task<PagedDto<CourseDto>> Search(SearchCourseDto dto);
    Task<CourseDto?> Create(CreateCourseDto dto);
    Task<CourseDto?> Update(int id, UpdateCourseDto dto);
    Task<bool> Disable(int id);
    Task<bool> Active(int id);
    Task Delete(int id);
}