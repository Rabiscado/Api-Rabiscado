using Newtonsoft.Json;

namespace Rabiscado.Application.Adapters.Assas.Application.Dtos.V1.Customers;

public class CreateCustomerDto
{
    public string Name { get; set; } = null!;
    public string? Email { get; set; }
    public string? Phone { get; set; }
    [JsonProperty("cpfCnpj")]
    public string CpfCnpj { get; set; } = null!;
}