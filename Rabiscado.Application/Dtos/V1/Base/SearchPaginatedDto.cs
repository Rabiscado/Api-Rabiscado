using System.Linq.Expressions;
using Rabiscado.Core.Utils;
using Rabiscado.Domain.Contracts;
using Rabiscado.Domain.Contracts.Pagination;

namespace Rabiscado.Application.Dtos.V1.Base;

public abstract class SearchPaginatedDto<T> : IViewModel, ISearchPaginated<T> where T : IEntity
{
    private const int MaxPageSize = 100;
    private const string DefaultDirectionOrdenation = "asc";
    private readonly string[] _optionsDirectionsOrdenations = { "asc", "desc" };

    public int Page { get; set; } = 1;
    private int _pageSize = 10;
    public int PageSize
    {
        get => _pageSize;
        set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
    }

    public string OrderBy { get; set; } = "id";
    private string _ordernationDirection = DefaultDirectionOrdenation;
    public string OrderDirection
    {
        get => _ordernationDirection;
        set =>
            _ordernationDirection = _optionsDirectionsOrdenations.Contains(value.ToLower()) 
                ? value.ToLower() 
                : DefaultDirectionOrdenation;
    }

    public virtual void ApplyFilter(ref IQueryable<T> query)
    { }

    public virtual void ApplyOrdernation(ref IQueryable<T> query)
    {
        query = query.OrderBy(o => o.Id);
    }

    public virtual Expression<Func<T, bool>> BuildExpression()
    {
        return PredicateUtils.True<T>();
    }
}
