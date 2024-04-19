using Rabiscado.Domain.Entities;

namespace Rabiscado.Domain.Contracts.Repositories;

public interface IUserPlanSubscriptionRepository : IRepository<UserPlanSubscription>
{
    Task<UserPlanSubscription?> GetById(int id);
    Task<List<UserPlanSubscription>> GetAll();
    Task<decimal> TotalReceiptsPerMonth();
    Task<decimal> TotalReceiptPlanPerMonth(int id);
    void Create(UserPlanSubscription userPlanSubscription);
    void Update(UserPlanSubscription userPlanSubscription);
    void Remove(UserPlanSubscription userPlanSubscription);
}