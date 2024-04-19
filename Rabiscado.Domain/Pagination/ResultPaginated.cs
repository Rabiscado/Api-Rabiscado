using Rabiscado.Domain.Contracts.Pagination;

namespace Rabiscado.Domain.Pagination;

public class ResultPaginated<T> : IResultPaginated<T>
{
    public ResultPaginated()
    {
        Itens = new List<T>();
        Pagination = new Pagination();
    }
    
    public ResultPaginated(int page, int pageSize, int total) : this()
    {
        Pagination = new Pagination
        {
            Page = page,
            PageSize = pageSize,
            Total = total
        };
    }
    
    public IList<T> Itens
    { get; set; }
    public IPagination Pagination { get; set; }
}