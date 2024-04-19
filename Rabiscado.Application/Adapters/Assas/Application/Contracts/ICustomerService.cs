using Rabiscado.Application.Adapters.Assas.Application.Dtos.V1.Customers;

namespace Rabiscado.Application.Adapters.Assas.Application.Contracts;

public interface ICustomerService
{
    Task<CustomerResponseDto?> GetById(string id);
    Task<List<CustomerResponseDto>?> GetByEmail(string email);
    Task<CustomerResponseDto?> Create(CreateCustomerDto dto);
    Task<bool> Remove(string id);
}