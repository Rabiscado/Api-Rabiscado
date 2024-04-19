using Rabiscado.Application.Dtos.V1.ForWho;

namespace Rabiscado.Application.Contracts;

public interface IForWhoService
{
    Task<ForWhoDto?> GetById(int id);
    Task<List<ForWhoDto>> GetAll();
}