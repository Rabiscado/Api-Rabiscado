namespace Rabiscado.Application.Adapters.Assas.Application.Dtos.V1.Payments;

public class SubscriptionDto
{
    public string Customer { get; set; } = null!;
    public string BillingType { get; set; } = null!;
    public decimal Value { get; set; }
    public DateTime NextDueDate { get; set; }
    public string Cycle { get; set; } = null!;
    public CreditCard? CreditCard { get; set; }
    public CreditCardHolderInfo? CreditCardHolderInfo { get; set; }
    public string? CreditCardToken { get; set; }
}

public class CreditCard
{
    public string HolderName { get; set; } = null!;
    public string? Number { get; set; } = null!;
    public string ExpiryMonth { get; set; } = null!;
    public string ExpiryYear { get; set; } = null!;
    public string Ccv { get; set; } = null!;
}

public class CreditCardHolderInfo
{
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string? CpfCnpj { get; set; } = null!;
    public string PostalCode { get; set; } = null!;
    public string AddressNumber { get; set; } = null!;
    public string? Phone { get; set; } = null!;
}