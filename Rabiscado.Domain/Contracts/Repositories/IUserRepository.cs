using Rabiscado.Domain.Entities;

namespace Rabiscado.Domain.Contracts.Repositories;

public interface IUserRepository : IRepository<User>
{
    Task<User?> GetById(int id);
    Task<User?> GetByEmail(string email);
    Task<User?> GetByPhone(string phone);
    Task<List<User>> GetAll();
    void Create(User user);
    void Update(User user);
    void Disable(User user);
    void Active(User user); 
}