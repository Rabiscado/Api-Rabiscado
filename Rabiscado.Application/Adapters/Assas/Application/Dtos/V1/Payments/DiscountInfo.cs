namespace Rabiscado.Application.Adapters.Assas.Application.Dtos.V1.Payments;

public class DiscountInfo
{
    public decimal? Value { get; set; }
    public int? DueDateLimitDays { get; set; }
    public DateTime? LimitedDate { get; set; }
    public string? Type { get; set; }
}