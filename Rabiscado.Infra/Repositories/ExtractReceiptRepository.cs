using Microsoft.EntityFrameworkCore;
using Rabiscado.Domain.Contracts.Repositories;
using Rabiscado.Domain.Entities;
using Rabiscado.Infra.Contexts;

namespace Rabiscado.Infra.Repositories;

public class ExtractReceiptRepository : Repository<ExtractReceipt>, IExtractReceiptRepository
{
    public ExtractReceiptRepository(RabiscadoContext context) : base(context)
    {
    }

    public async Task<ExtractReceipt?> GetById(int id)
    {
        return await Context.ExtractReceipts.FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<List<ExtractReceipt>> GetAll()
    {
        return await Context.ExtractReceipts.ToListAsync();
    }

    public void Add(ExtractReceipt extractReceipt)
    {
        Context.ExtractReceipts.Add(extractReceipt);
    }

    public void Update(ExtractReceipt extractReceipt)
    {
        Context.ExtractReceipts.Update(extractReceipt);
    }
}