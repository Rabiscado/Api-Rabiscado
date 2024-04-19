namespace Rabiscado.Application.Adapters.Assas.Application.Dtos.V1.Payments;

public class SubscriptionHookDto
{
    public string Event { get; set; } = null!;
    public PaymentInfo Payment { get; set; } = null!;
}

public class PaymentInfo
{
    public string Object { get; set; } = null!;
    public string Id { get; set; } = null!;
    public DateTime? DateCreated { get; set; }
    public string Customer { get; set; } = null!;
    public string? Subscription { get; set; }
    public string? Installment { get; set; }
    public string? PaymentLink { get; set; }
    public DateTime? DueDate { get; set; }
    public DateTime? OriginalDueDate { get; set; }
    public decimal? Value { get; set; }
    public decimal? NetValue { get; set; }
    public decimal? OriginalValue { get; set; }
    public decimal? InterestValue { get; set; }
    public string? NossoNumero { get; set; }
    public string? Description { get; set; }
    public string? ExternalReference { get; set; }
    public string? BillingType { get; set; }
    public string? Status { get; set; }
    public PixTransactionInfo? PixTransaction { get; set; }
    public DateTime? ConfirmedDate { get; set; }
    public DateTime? PaymentDate { get; set; }
    public DateTime? ClientPaymentDate { get; set; }
    public int? InstallmentNumber { get; set; }
    public DateTime? CreditDate { get; set; }
    public string? Custody { get; set; }
    public DateTime? EstimatedCreditDate { get; set; }
    public string? InvoiceUrl { get; set; }
    public string? BankSlipUrl { get; set; }
    public string? TransactionReceiptUrl { get; set; }
    public string? InvoiceNumber { get; set; }
    public bool? Deleted { get; set; }
    public bool? Anticipated { get; set; }
    public bool? Anticipable { get; set; }
    public DateTime? LastInvoiceViewedDate { get; set; }
    public DateTime? LastBankSlipViewedDate { get; set; }
    public bool? PostalService { get; set; }
    public CreditCardInfo CreditCard { get; set; } = null!;
    public DiscountInfo? Discount { get; set; }
    public FineInfo? Fine { get; set; }
    public InterestInfo? Interest { get; set; }
    public SplitInfo[]? Split { get; set; }
    public ChargebackInfo? Chargeback { get; set; }
    public RefundInfo[]? Refunds { get; set; }
}

public class PixTransactionInfo
{
    // Adicione propriedades conforme necessário
}

public class RefundInfo
{
    // Adicione propriedades conforme necessário
}
