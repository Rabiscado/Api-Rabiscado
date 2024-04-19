using Rabiscado.Domain.Entities;

namespace Rabiscado.Domain.Contracts.Repositories;

public interface ICourseRepository : IRepository<Course>
{
    Task<Course?> GetById(int id);
    Task<List<Course>> GetAll();
    void Create(Course plan);
    void Update(Course plan);
    void Disable(Course plan);
    void Active(Course plan);
    void Delete(Course plan);
}