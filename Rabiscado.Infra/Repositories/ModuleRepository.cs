using Microsoft.EntityFrameworkCore;
using Rabiscado.Domain.Contracts.Repositories;
using Rabiscado.Domain.Entities;
using Rabiscado.Infra.Contexts;

namespace Rabiscado.Infra.Repositories;

public class ModuleRepository : Repository<Module>, IModuleRepository
{
    public ModuleRepository(RabiscadoContext context) : base(context)
    {
    }

    public async Task<Module?> GetById(int id)
    {
        return await Context.Modules
            .Include(m => m.Course)
            .ThenInclude(c => c.Subscriptions)
            .ThenInclude(s => s.User)
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<List<Module>> GetAll()
    {
        return await Context.Modules.ToListAsync();
    }

    public void Add(Module module)
    {
        Context.Modules.Add(module);
    }

    public void Update(Module module)
    {
        Context.Modules.Update(module);
    }

    public void Disable(Module module)
    {
        module.Disabled = true;
        Context.Modules.Update(module);
    }

    public void Active(Module module)
    {
        module.Disabled = false;
        Context.Modules.Update(module);
    }
}