using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Rabiscado.Domain.Contracts.Pagination;
using Rabiscado.Domain.Contracts.Repositories;
using Rabiscado.Domain.Entities;
using Rabiscado.Infra.Contexts;
using Rabiscado.Infra.Extensions;

namespace Rabiscado.Infra.Repositories;

public class ExtractRepository : Repository<Extract>, IExtractRepository
{
    public ExtractRepository(RabiscadoContext context) : base(context)
    {
    }

    public async Task<Extract?> GetById(int id)
    {
        return await Context.Extracts
            .Include(e => e.User)
            .Include(e => e.Professor)
            .Include(e => e.Course)
            .FirstOrDefaultAsync(e => e.Id == id);
    }

    public async Task<List<Extract>> GetAll()
    {
        return await Context.Extracts
            .Include(e => e.User)
            .Include(e => e.Professor)
            .Include(e => e.Course)
            .ToListAsync();
    }
    
    public override async Task<IResultPaginated<Extract>> Search(ISearchPaginated<Extract> filtro)
    {
        var queryable = Context.Extracts
            .Include(s => s.User)
            .Include(e => e.Professor)
            .Include(e => e.Course)
            .AsQueryable();

        filtro.ApplyFilter(ref queryable);
        filtro.ApplyOrdernation(ref queryable);

        return await queryable.SearchPaginatedAsync(filtro.Page, filtro.PageSize);
    }

    public void Create(Extract extract)
    {
        Context.Extracts.Add(extract);
    }

    public void Update(Extract extract)
    {
        Context.Extracts.Update(extract);
    }

    public void Disable(Extract extract)
    {
        extract.Disabled = true;
        Context.Extracts.Update(extract);
    }

    public void Active(Extract extract)
    {
        extract.Disabled = false;
        Context.Extracts.Update(extract);
    }
    
    public override async Task<List<Extract>> Search(Expression<Func<Extract, bool>> predicate)
    {
        return await Context.Extracts.AsNoTrackingWithIdentityResolution()
            .Include(s => s.Course)
            .Include(s => s.User)
            .Where(predicate)
            .ToListAsync();
    }
}