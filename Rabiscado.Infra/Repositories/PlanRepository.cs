using Microsoft.EntityFrameworkCore;
using Rabiscado.Domain.Contracts.Repositories;
using Rabiscado.Domain.Entities;
using Rabiscado.Infra.Contexts;

namespace Rabiscado.Infra.Repositories;

public class PlanRepository : Repository<Plan>, IPlanRepository
{
    public PlanRepository(RabiscadoContext context) : base(context)
    {
    }

    public async Task<Plan?> GetById(int id)
    {
        return await Context.Plans.FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<List<Plan>> GetAll()
    {
        return await Context.Plans.ToListAsync();
    }

    public void Add(Plan plan)
    {
        Context.Plans.Add(plan);
    }

    public void Update(Plan plan)
    {
        Context.Plans.Update(plan);
    }

    public void Disable(Plan plan)
    {
        plan.Disabled = true;
        Context.Plans.Update(plan);
    }
    
    public void Active(Plan plan)
    {
        plan.Disabled = false;
        Context.Plans.Update(plan);
    }
}