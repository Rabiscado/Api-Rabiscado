using Rabiscado.Application.Dtos.V1.Level;

namespace Rabiscado.Application.Contracts;

public interface ILevelService
{
    Task<LevelDto?> GetById(int id);
    Task<List<LevelDto>> GetAll();
}