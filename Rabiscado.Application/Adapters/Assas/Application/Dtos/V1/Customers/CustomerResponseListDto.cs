namespace Rabiscado.Application.Adapters.Assas.Application.Dtos.V1.Customers;

public class CustomerResponseListDto
{
    public string Object { get; set; } = null!;
    public bool HasMore { get; set; }
    public int TotalCount { get; set; }
    public int Limit { get; set; }
    public int Offset { get; set; }
    public List<CustomerResponseDto>? Data { get; set; } = new();
}