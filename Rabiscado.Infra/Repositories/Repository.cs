using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Rabiscado.Domain.Contracts;
using Rabiscado.Domain.Contracts.Pagination;
using Rabiscado.Domain.Contracts.Repositories;
using Rabiscado.Domain.Entities;
using Rabiscado.Infra.Contexts;
using Rabiscado.Infra.Extensions;

namespace Rabiscado.Infra.Repositories;

public abstract class Repository<T> : IRepository<T> where T : BaseEntity, IAggregateRoot, new()
{
    private bool _isDisposed;
    private readonly DbSet<T> _dbSet;
    protected readonly RabiscadoContext Context;
    public IUnitOfWork UnitOfWork => Context;

    protected Repository(RabiscadoContext context)
    {
        Context = context;
        _dbSet = context.Set<T>();
    }

    public virtual async Task<IResultPaginated<T>> Search(ISearchPaginated<T> filtro)
    {
        var queryable = _dbSet.AsQueryable();
        
        filtro.ApplyFilter(ref queryable);
        filtro.ApplyOrdernation(ref queryable);
        
        return await queryable.SearchPaginatedAsync(filtro.Page, filtro.PageSize);
    }
    
    public async Task<IResultPaginated<T>> Search(IQueryable<T> queryable, ISearchPaginated<T> filtro)
    {
        filtro.ApplyFilter(ref queryable);
        filtro.ApplyOrdernation(ref queryable);
        
        return await queryable.SearchPaginatedAsync(filtro.Page, filtro.PageSize);
    }
    
    public virtual async Task<List<T>> Search(Expression<Func<T, bool>> predicate)
    {
        return await _dbSet.AsNoTrackingWithIdentityResolution().Where(predicate).ToListAsync();
    }

    public async Task<T?> FirstOrDefault(Expression<Func<T, bool>> expression)
    {
        return await _dbSet.AsNoTrackingWithIdentityResolution().Where(expression).FirstOrDefaultAsync();
    }

    public async Task<int> Count(Expression<Func<T, bool>> predicate)
    {
        return await _dbSet.CountAsync(predicate);
    }

    public async Task<bool> Any(Expression<Func<T, bool>> expression)
    {
        return await _dbSet.AnyAsync(expression);
    }
    
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
    
    protected virtual void Dispose(bool disposing)
    {
        if (_isDisposed) return;

        if (disposing)
        {
            // free managed resources
            Context.Dispose();
        }

        _isDisposed = true;
    }
    
    ~Repository()
    {
        Dispose(false);
    }
}