using Rabiscado.Application.Adapters.Assas.Application.Dtos.V1.Payments;

namespace Rabiscado.Application.Adapters.Assas.Application.Contracts;

public interface IPaymentService
{
    Task VerifyPayment(SubscriptionHookDto dto);
}