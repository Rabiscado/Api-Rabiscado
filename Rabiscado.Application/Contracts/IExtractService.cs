using Rabiscado.Application.Dtos.V1.Base;
using Rabiscado.Application.Dtos.V1.Extracts;

namespace Rabiscado.Application.Contracts;

public interface IExtractService
{
    Task<ExtractDto?> GetById(int id);
    Task<List<ExtractDto>> GetAll();
    Task<PagedDto<ExtractDto>> Search(SearchExtractDto dto);
    Task<ExtractDto?> Create(CreateExtractDto dto);
    Task<ExtractDto?> Update(int id, UpdateExtractDto dto);
    Task<bool> Disable(int id);
    Task<bool> Active(int id);
}