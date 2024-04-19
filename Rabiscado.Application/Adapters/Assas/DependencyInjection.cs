using Microsoft.Extensions.DependencyInjection;
using Rabiscado.Application.Adapters.Assas.Application.Contracts;
using Rabiscado.Application.Adapters.Assas.Application.Services;

namespace Rabiscado.Application.Adapters.Assas;

public static class DependencyInjection
{
    public static void ConfigureAssas(this IServiceCollection services)
    {
        services
            .AddScoped<ICustomerService, CustomerService>()
            .AddScoped<ISubscriptionService, SubscriptionService>()
            .AddScoped<IPaymentService, PaymentService>();
    }
}