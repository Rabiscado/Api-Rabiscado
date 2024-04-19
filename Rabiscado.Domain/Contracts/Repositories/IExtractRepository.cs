using Rabiscado.Domain.Entities;

namespace Rabiscado.Domain.Contracts.Repositories;

public interface IExtractRepository : IRepository<Extract>
{
    Task<Extract?> GetById(int id);
    Task<List<Extract>> GetAll();
    void Create(Extract extract);
    void Update(Extract extract);
    void Disable(Extract extract);
    void Active(Extract extract);
}