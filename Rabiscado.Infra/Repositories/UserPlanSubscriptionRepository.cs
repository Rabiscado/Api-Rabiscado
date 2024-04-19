using Microsoft.EntityFrameworkCore;
using Rabiscado.Domain.Contracts.Repositories;
using Rabiscado.Domain.Entities;
using Rabiscado.Infra.Contexts;

namespace Rabiscado.Infra.Repositories;

public class UserPlanSubscriptionRepository : Repository<UserPlanSubscription>, IUserPlanSubscriptionRepository
{
    public UserPlanSubscriptionRepository(RabiscadoContext context) : base(context)
    {
    }

    public async Task<UserPlanSubscription?> GetById(int id)
    {
        return await Context.UserPlanSubscriptions.FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<List<UserPlanSubscription>> GetAll()
    {
        return await Context.UserPlanSubscriptions.ToListAsync();
    }

    public async Task<decimal> TotalReceiptsPerMonth()
    {
        return await Context.UserPlanSubscriptions
            .Where(up => up.CreateAt >= DateTime.Now.AddMonths(-1) && up.CreateAt <= DateTime.Now)
            .SumAsync(up => up.Plan.Price);
    }
    
    public async Task<decimal> TotalReceiptPlanPerMonth(int id)
    {
        return await Context.UserPlanSubscriptions
            .Where(up => up.CreateAt >= DateTime.Now.AddMonths(-1) && up.CreateAt <= DateTime.Now && up.PlanId == id)
            .SumAsync(up => up.Plan.Price);
    }

    public void Create(UserPlanSubscription userPlanSubscription)
    {
        Context.UserPlanSubscriptions.Add(userPlanSubscription);
    }

    public void Update(UserPlanSubscription userPlanSubscription)
    {
        Context.UserPlanSubscriptions.Update(userPlanSubscription);
    }

    public void Remove(UserPlanSubscription userPlanSubscription)
    {
        Context.UserPlanSubscriptions.Remove(userPlanSubscription);
    }
}