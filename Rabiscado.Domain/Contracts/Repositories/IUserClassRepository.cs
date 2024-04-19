using Rabiscado.Domain.Entities;

namespace Rabiscado.Domain.Contracts.Repositories;

public interface IUserClassRepository : IRepository<UserClass>
{
    Task<UserClass?> GetById(int classId, int userId);
    void MarkAsWatched(UserClass userClass);
    void MarkAsUnwatched(UserClass userClass);
}