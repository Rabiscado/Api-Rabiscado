using Rabiscado.Domain.Entities;

namespace Rabiscado.Domain.Contracts.Repositories;

public interface IModuleRepository : IRepository<Module>
{
    Task<Module?> GetById(int id);
    Task<List<Module>> GetAll();
    void Add(Module module);
    void Update(Module module);
    void Disable(Module module);
    void Active(Module module);
}