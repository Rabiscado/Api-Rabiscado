using Rabiscado.Domain.Contracts.Pagination;

namespace Rabiscado.Domain.Pagination;

public class Pagination : IPagination
{
    public int Total { get; set; }
    public int TotalInPage { get; set; }
    public int Page { get; set; }
    public int PageSize { get; set; }
    public int TotalPages { get; set; }
}
