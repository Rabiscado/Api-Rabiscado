using Rabiscado.Application.Dtos.V1.Module;

namespace Rabiscado.Application.Contracts;

public interface IModuleService
{
    Task<ModuleDto?> GetById(int id);
    Task<List<ModuleDto>> GetAll();
    Task<ModuleDto?> Create(CreateModuleDto dto);
    Task<ModuleDto?> Update(int id, UpdateModuleDto dto);
    Task<bool> Disable(int id);
    Task<bool> Active(int id);
}