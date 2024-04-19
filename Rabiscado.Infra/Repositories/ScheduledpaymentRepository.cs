using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Rabiscado.Domain.Contracts.Pagination;
using Rabiscado.Domain.Contracts.Repositories;
using Rabiscado.Domain.Entities;
using Rabiscado.Infra.Contexts;
using Rabiscado.Infra.Extensions;

namespace Rabiscado.Infra.Repositories;

public class ScheduledpaymentRepository : Repository<Scheduledpayment>, IScheduledpaymentRepository
{
    public ScheduledpaymentRepository(RabiscadoContext context) : base(context)
    {
    }

    public async Task<Scheduledpayment?> GetById(int id)
    {
        return await Context.Scheduledpayments
            .Include(s => s.User)
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<List<Scheduledpayment>> GetAll()
    {
        return await Context.Scheduledpayments
            .Include(s => s.User)
            .ToListAsync();
    }
    
    public override async Task<IResultPaginated<Scheduledpayment>> Search(ISearchPaginated<Scheduledpayment> filtro)
    {
        var queryable = Context.Scheduledpayments
            .Include(s => s.User)
            .AsQueryable();

        filtro.ApplyFilter(ref queryable);
        filtro.ApplyOrdernation(ref queryable);

        return await queryable.SearchPaginatedAsync(filtro.Page, filtro.PageSize);
    }

    public void Create(Scheduledpayment scheduledpayment)
    {
        Context.Scheduledpayments.Add(scheduledpayment);
    }

    public void Update(Scheduledpayment scheduledpayment)
    {
        Context.Scheduledpayments.Update(scheduledpayment);
    }

    public void Disable(Scheduledpayment scheduledpayment)
    {
        scheduledpayment.Disabled = true;
        Context.Scheduledpayments.Update(scheduledpayment);
    }

    public void Active(Scheduledpayment scheduledpayment)
    {
        scheduledpayment.Disabled = false;
        Context.Scheduledpayments.Update(scheduledpayment);
    }

    public void MarkAsPaidOut(Scheduledpayment scheduledpayment)
    {
        scheduledpayment.PaidOut = true;
        Context.Scheduledpayments.Update(scheduledpayment);
    }

    public void MarkAsUnPaid(Scheduledpayment scheduledpayment)
    {
        scheduledpayment.PaidOut = false;
        Context.Scheduledpayments.Update(scheduledpayment);
    }
    
    public async Task<(decimal TotalReceipt, decimal ToReceive, decimal Received)> TotalReceipt(string professorEmail)
    {
        var totalReceipt = await Context.Scheduledpayments
            .Where(e => e.User.Email == professorEmail)
            .SumAsync(e => e.Value);

        var toReceive = await Context.Scheduledpayments
            .Where(e => e.User.Email == professorEmail && e.PaidOut == false)
            .SumAsync(e => e.Value);

        var received = await Context.Scheduledpayments
            .Where(e => e.User.Email == professorEmail && e.PaidOut == true)
            .SumAsync(e => e.Value);

        return (totalReceipt, toReceive, received);
    }
    
    public override async Task<List<Scheduledpayment>> Search(Expression<Func<Scheduledpayment, bool>> predicate)
    {
        return await Context.Scheduledpayments.AsNoTrackingWithIdentityResolution()
            .Include(s => s.Course)
            .Include(s => s.User)
            .Where(predicate)
            .ToListAsync();
    }
}