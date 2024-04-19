using Rabiscado.Application.Dtos.V1.Class;

namespace Rabiscado.Application.Contracts;

public interface IClassService
{
    Task<ClassDto?> GetById(int id);
    Task<List<ClassDto>> GetAll();
    Task<ClassDto?> Create(CreateClassDto dto);
    Task<ClassDto?> Update(int id, UpdateClassDto dto);
    Task<bool> Disable(int id);
    Task<bool> Active(int id);
}