using System.Linq.Expressions;
using Rabiscado.Domain.Contracts.Pagination;
using Rabiscado.Domain.Entities;

namespace Rabiscado.Domain.Contracts.Repositories;

public interface IRepository<T> : IDisposable where T : BaseEntity, IAggregateRoot
{
    IUnitOfWork UnitOfWork { get; }
    Task<IResultPaginated<T>> Search(ISearchPaginated<T> filtro);
    Task<IResultPaginated<T>> Search(IQueryable<T> queryable, ISearchPaginated<T> filtro);
    Task<List<T>> Search(Expression<Func<T, bool>> predicate);
    Task<T?> FirstOrDefault(Expression<Func<T, bool>> predicate);
    Task<int> Count(Expression<Func<T, bool>> predicate);
    Task<bool> Any(Expression<Func<T, bool>> predicate);
}

