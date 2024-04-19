namespace Rabiscado.Application.Dtos.V1.Scheduledpayments;

public class CreateScheduledpaymentDto
{
    public decimal Value { get; set; }
    public string Professor { get; set; } = null!;
    public DateTime Date { get; set; }
    public bool PaidOut { get; set; }
    public int UserId { get; set; }
}