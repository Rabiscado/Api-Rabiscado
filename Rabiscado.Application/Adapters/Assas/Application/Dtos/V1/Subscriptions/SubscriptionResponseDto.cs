using Rabiscado.Application.Adapters.Assas.Application.Dtos.V1.Payments;

namespace Rabiscado.Application.Adapters.Assas.Application.Dtos.V1.Subscriptions;

public class SubscriptionResponseDto
{
    public string? Object { get; set; }
    public string? Id { get; set; }
    public DateTime? DateCreated { get; set; }
    public string? Customer { get; set; }
    public float? Value { get; set; }
    public DateTime? NextDueDate { get; set; }
    public string? Cycle { get; set; }
    public string? Description { get; set; }
    public string? BillingType { get; set; }
    public bool? Deleted { get; set; }
    public string? Status { get; set; }
    public string? ExternalReference { get; set; }
    public CreditCardInfo? CreditCard { get; set; }
    public bool? SendPaymentByPostalService { get; set; }
    public FineInfo? Fine { get; set; }
    public InterestInfo? Interest { get; set; }
    public Split? Split { get; set; }
}

public abstract class Split
{
    public string WalletId { get; set; } = null!;
    public float? FixedValue { get; set; }
    public float? PercentualValue { get; set; }
    public float? TotalFixedValue { get; set; }
}