using Rabiscado.Domain.Entities;

namespace Rabiscado.Domain.Contracts.Repositories;

public interface IClassRepository : IRepository<Class>
{
    Task<Class?> GetById(int id);
    Task<List<Class>> GetAll();
    void Add(Class plan);
    void Update(Class plan);
    void Disable(Class plan);
    void Active(Class plan);
}