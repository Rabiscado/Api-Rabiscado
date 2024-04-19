using Microsoft.EntityFrameworkCore;
using Rabiscado.Domain.Contracts.Repositories;
using Rabiscado.Domain.Entities;
using Rabiscado.Infra.Contexts;

namespace Rabiscado.Infra.Repositories;

public class LevelRepository : Repository<Level>, ILevelRepository
{
    public LevelRepository(RabiscadoContext context) : base(context)
    {
    }

    public async Task<Level?> GetById(int id)
    {
        return await Context.Levels.FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<List<Level>> GetAll()
    {
        return await Context.Levels.ToListAsync();
    }
}