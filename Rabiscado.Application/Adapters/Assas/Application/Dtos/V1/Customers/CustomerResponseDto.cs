namespace Rabiscado.Application.Adapters.Assas.Application.Dtos.V1.Customers;

public class CustomerResponseDto
{
    public string Object { get; set; } = null!;
    public string Id { get; set; } = null!;
    public DateTime DateCreated { get; set; }
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string? Phone { get; set; }
    public string? MobilePhone { get; set; }
    public string? Address { get; set; }
    public string? AddressNumber { get; set; }
    public string? Complement { get; set; }
    public string? Province { get; set; }
    public string? PostalCode { get; set; }
    public string CpfCnpj { get; set; } = null!;
    public string? PersonType { get; set; }
    public bool? Deleted { get; set; }
    public string? AdditionalEmails { get; set; }
    public string? ExternalReference { get; set; }
    public bool? NotificationDisabled { get; set; }
    public int? City { get; set; }
    public string? State { get; set; }
    public string? Country { get; set; }
    public string? Observations { get; set; }
}