namespace Rabiscado.Application.Adapters.Assas.Application.Dtos.V1.Subscriptions;

public class SubscriptionResponseListDto
{
    public string Object { get; set; } = null!;
    public bool HasMore { get; set; }
    public int TotalCount { get; set; }
    public int Limit { get; set; }
    public int Offset { get; set; }
    public List<SubscriptionResponseDto>? Data { get; set; } = new();
}