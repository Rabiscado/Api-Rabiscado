using Microsoft.EntityFrameworkCore;
using Rabiscado.Domain.Contracts.Repositories;
using Rabiscado.Domain.Entities;
using Rabiscado.Infra.Contexts;

namespace Rabiscado.Infra.Repositories;

public class ForWhoRepository : Repository<ForWho>, IForWhoRepository
{
    public ForWhoRepository(RabiscadoContext context) : base(context)
    {
    }

    public async Task<ForWho?> GetById(int id)
    {
        return await Context.ForWhos.FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<List<ForWho>> GetAll()
    {
        return await Context.ForWhos.ToListAsync();
    }
}