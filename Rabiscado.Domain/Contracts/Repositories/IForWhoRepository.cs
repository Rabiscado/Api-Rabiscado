using Rabiscado.Domain.Entities;

namespace Rabiscado.Domain.Contracts.Repositories;

public interface IForWhoRepository : IRepository<ForWho>
{
    Task<ForWho?> GetById(int id);
    Task<List<ForWho>> GetAll();
}