using System.Linq.Expressions;

namespace Rabiscado.Domain.Contracts.Pagination;

public interface ISearchPaginated<T> where T : IEntity
{
    public int Page { get; set; }
    public int PageSize { get; set; }
    public string OrderBy { get; set; }
    public string OrderDirection { get; set; }

    public void ApplyFilter(ref IQueryable<T> query);
    public void ApplyOrdernation(ref IQueryable<T> query);
    public Expression<Func<T, bool>> BuildExpression();
}
