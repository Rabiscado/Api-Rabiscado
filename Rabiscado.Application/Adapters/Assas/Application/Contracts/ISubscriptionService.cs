using Rabiscado.Application.Adapters.Assas.Application.Dtos.V1.Subscriptions;
using Rabiscado.Application.Dtos.V1.Subscription;
using SubscriptionDto = Rabiscado.Application.Adapters.Assas.Application.Dtos.V1.Payments.SubscriptionDto;

namespace Rabiscado.Application.Adapters.Assas.Application.Contracts;

public interface ISubscriptionService
{
    Task<SubscriptionResponseDto?> Create(SubscriptionDto dto);
    Task<SubscriptionResponseListDto?> GetByCustomerId(string customerId);
    Task<SubscriptionUnsubscribreResponseDto?> Unsubscribe(string id);
}