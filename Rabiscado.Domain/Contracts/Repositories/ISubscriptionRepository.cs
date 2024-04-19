using Rabiscado.Domain.Entities;

namespace Rabiscado.Domain.Contracts.Repositories;

public interface ISubscriptionRepository : IRepository<Subscription>
{
    void Update(Subscription subscription);
}