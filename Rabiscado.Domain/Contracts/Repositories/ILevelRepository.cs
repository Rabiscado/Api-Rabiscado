using Rabiscado.Domain.Entities;

namespace Rabiscado.Domain.Contracts.Repositories;

public interface ILevelRepository : IRepository<Level>
{
    Task<Level?> GetById(int id);
    Task<List<Level>> GetAll();
}