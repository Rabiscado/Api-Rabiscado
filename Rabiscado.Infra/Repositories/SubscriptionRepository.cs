using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Rabiscado.Domain.Contracts.Repositories;
using Rabiscado.Domain.Entities;
using Rabiscado.Infra.Contexts;

namespace Rabiscado.Infra.Repositories;

public class SubscriptionRepository : Repository<Subscription>, ISubscriptionRepository
{
    public SubscriptionRepository(RabiscadoContext context) : base(context)
    {
    }

    public override async Task<List<Subscription>> Search(Expression<Func<Subscription, bool>> predicate)
    {
        return await Context.Subscriptions.AsNoTrackingWithIdentityResolution()
            .Include(s => s.Course)
            .Include(s => s.User)
            .Where(predicate)
            .ToListAsync();
    }

    public void Update(Subscription subscription)
    {
        Context.Update(subscription);
    }
}