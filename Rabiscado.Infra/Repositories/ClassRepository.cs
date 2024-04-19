using Microsoft.EntityFrameworkCore;
using Rabiscado.Domain.Contracts.Repositories;
using Rabiscado.Domain.Entities;
using Rabiscado.Infra.Contexts;

namespace Rabiscado.Infra.Repositories;

public class ClassRepository : Repository<Class>, IClassRepository
{
    public ClassRepository(RabiscadoContext context) : base(context)
    {
    }

    public async Task<Class?> GetById(int id)
    {
        return await Context.Classes
            .Include(c => c.Steps)
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<List<Class>> GetAll()
    {
        return await Context.Classes
            .Include(c => c.Steps)
            .ToListAsync();
    }

    public void Add(Class plan)
    {
        Context.Classes.Add(plan);
    }

    public void Update(Class plan)
    {
        Context.Classes.Update(plan);
    }

    public void Disable(Class plan)
    {
        plan.Disabled = true;
        Context.Classes.Update(plan);
    }

    public void Active(Class plan)
    {
        plan.Disabled = false;
        Context.Classes.Update(plan);
    }
}