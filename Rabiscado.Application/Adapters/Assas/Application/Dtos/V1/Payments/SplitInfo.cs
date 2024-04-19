namespace Rabiscado.Application.Adapters.Assas.Application.Dtos.V1.Payments;

public class SplitInfo
{
    public string? WalletId { get; set; }
    public decimal? FixedValue { get; set; }
    public decimal? PercentualValue { get; set; }
    public string? Status { get; set; }
    public string? RefusalReason { get; set; }
}