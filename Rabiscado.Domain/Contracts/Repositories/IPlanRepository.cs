using Rabiscado.Domain.Entities;

namespace Rabiscado.Domain.Contracts.Repositories;

public interface IPlanRepository : IRepository<Plan>
{
    Task<Plan?> GetById(int id);
    Task<List<Plan>> GetAll();
    void Add(Plan plan);
    void Update(Plan plan);
    void Disable(Plan plan);
    void Active(Plan plan);
}