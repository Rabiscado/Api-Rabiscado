using Rabiscado.Domain.Contracts;
using Rabiscado.Domain.Entities;
using Rabiscado.Infra.Contexts;

namespace Rabiscado.Infra.Repositories;

public class ReimbursementRepository : Repository<Reimbursement>, IReimbursementRepository
{
    public ReimbursementRepository(RabiscadoContext context) : base(context)
    {
    }
}