using Rabiscado.Domain.Contracts;
using Rabiscado.Domain.Contracts.Pagination;

namespace Rabiscado.Application.Dtos.V1.Base;

public class PagedDto<T> : IResultPaginated<T>, IViewModel
{
    public IList<T> Itens { get; set; }
    public IPagination Pagination { get; set; }

    public PagedDto()
    {
        Itens = new List<T>();
        Pagination = new PaginacaoDto();
    }
} 
