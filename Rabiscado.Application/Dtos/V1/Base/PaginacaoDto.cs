using Rabiscado.Domain.Contracts.Pagination;

namespace Rabiscado.Application.Dtos.V1.Base;

public class PaginacaoDto : IPagination
{
    public int Total { get; set; }
    public int TotalInPage { get; set; }
    public int Page { get; set; }
    public int PageSize { get; set; }
    public int TotalPages { get; set; }

    public int LastPage => TotalPages;

    public bool HasPages => TotalPages > 0;

    public bool OnFirstPage => Page == 1;

    public bool OnLastPage => Page == TotalPages;

    public bool HasMorePages => TotalPages > Page;
}
