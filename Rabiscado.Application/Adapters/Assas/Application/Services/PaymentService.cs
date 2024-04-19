using AutoMapper;
using Rabiscado.Application.Adapters.Assas.Application.Contracts;
using Rabiscado.Application.Adapters.Assas.Application.Dtos.V1.Payments;
using Rabiscado.Application.Notifications;
using Rabiscado.Application.Services;
using Rabiscado.Domain.Contracts.Repositories;
using Rabiscado.Domain.Entities;

namespace Rabiscado.Application.Adapters.Assas.Application.Services;

public class PaymentService : BaseService, IPaymentService
{
    private readonly ICustomerService _customerService;
    private readonly IUserRepository _userRepository;
    private readonly ISubscriptionService _subscriptionService;
    private readonly IExtractReceiptRepository _extractReceiptRepository;
    public PaymentService(IMapper mapper, INotificator notificator, ICustomerService customerService, IUserRepository userRepository, ISubscriptionService subscriptionService, IExtractReceiptRepository extractReceiptRepository) : base(mapper, notificator)
    {
        _customerService = customerService;
        _userRepository = userRepository;
        _subscriptionService = subscriptionService;
        _extractReceiptRepository = extractReceiptRepository;
    }

    public async Task VerifyPayment(SubscriptionHookDto dto)
    {
        switch (dto.Event)
        {
            case "PAYMENT_CREATED":
            {
                var customer = await _customerService.GetById(dto.Payment.Customer);
                if (customer is null)
                {
                    Notificator.Handle("An error occurred while trying to get the customer");
                    return;
                }
                
                var user = await _userRepository.GetByEmail(customer.Email);
                if (user is null)
                {
                    Notificator.HandleNotFoundResourse();
                    return;
                }
                
                var subscriptions = await _subscriptionService.GetByCustomerId(customer.Id);
                if (!(subscriptions!.Data!.OrderBy(s => s.NextDueDate).First().NextDueDate >= DateTime.Now))
                {
                    return;
                }
                
                user.AddCoin(user.Plan.CoinValue);
                _userRepository.Update(user);
                if (await _userRepository.UnitOfWork.Commit())
                {
                    return;
                }
                
                Notificator.Handle("An error occurred while trying to update the user");
                return;
            }
                
            case "PAYMENT_CONFIRMED":
            {
                var customer = await _customerService.GetById(dto.Payment.Customer);
                if (customer is null)
                {
                    Notificator.Handle("An error occurred while trying to get the customer");
                    return;
                }
                
                var user = await _userRepository.GetByEmail(customer.Email);
                if (user is null)
                {
                    Notificator.HandleNotFoundResourse();
                    return;
                }
                
                _extractReceiptRepository.Add(new ExtractReceipt
                {
                    User = user,
                    Value = user.Plan.Price,
                    Plan = user.Plan
                });
                
                var subscriptions = await _subscriptionService.GetByCustomerId(customer.Id);
                if (subscriptions!.Data!.OrderBy(s => s.NextDueDate).First().NextDueDate >= DateTime.Now)
                {
                    return;
                }
                
                user.AddCoin(user.Plan.CoinValue);
                _userRepository.Update(user);
                if (await _userRepository.UnitOfWork.Commit())
                {
                    return;
                }
                
                Notificator.Handle("An error occurred while trying to update the user");
                return;
            }
                
            case "PAYMENT_CREDIT_CARD_CAPTURE_REFUSED":
            {
                var customer = await _customerService.GetById(dto.Payment.Customer);
                if (customer is null)
                {
                    Notificator.Handle("An error occurred while trying to get the customer");
                    return;
                }
                
                var user = await _userRepository.GetByEmail(customer.Email);
                if (user is null)
                {
                    Notificator.HandleNotFoundResourse();
                    return;
                }

                var subscriptions = await _subscriptionService.GetByCustomerId(customer.Id);
                if (!(subscriptions!.Data!.OrderBy(s => s.NextDueDate).First().NextDueDate >= DateTime.Now))
                {
                    return;
                }
                
                var toRemover = user.Coin;
                user.RemoveCoin(toRemover);
                if (user.Subscriptions.Any())
                {
                    user.Subscriptions.Clear();
                }

                if (user.Extracts.Any())
                {
                    user.Extracts.Clear();
                }
                
                user.Reimbursements.Add(new Reimbursement
                {
                    User = user
                });
                
                user.PlanId = null;
                _userRepository.Update(user);
                if (await _userRepository.UnitOfWork.Commit())
                {
                    return;
                }
                
                Notificator.Handle("An error occurred while trying to update the user");
                return;
            }
        }
    }
}