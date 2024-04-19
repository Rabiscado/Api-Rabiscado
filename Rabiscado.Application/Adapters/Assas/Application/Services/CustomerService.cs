using System.Net;
using AutoMapper;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Rabiscado.Application.Adapters.Assas.Application.Contracts;
using Rabiscado.Application.Adapters.Assas.Application.Dtos.V1.Customers;
using Rabiscado.Application.Notifications;
using Rabiscado.Application.Notifications.CustomerEntitiesError;
using Rabiscado.Application.Services;
using Rabiscado.Core.Settings;
using RestSharp;

namespace Rabiscado.Application.Adapters.Assas.Application.Services;

public class CustomerService : BaseService, ICustomerService
{
    private readonly AssasSettings _assasSettings;

    public CustomerService(IMapper mapper, INotificator notificator, IOptions<AssasSettings> assasSettings) : base(
        mapper, notificator)
    {
        _assasSettings = assasSettings.Value;
    }

    public async Task<CustomerResponseDto?> GetById(string id)
    {
        var options = new RestClientOptions($"{_assasSettings.BaseUrl}/customers/{id}");
        var client = new RestClient(options);
        var request = new RestRequest("");
        request.AddHeader("accept", "application/json");
        request.AddHeader("content-type", "application/json");
        request.AddHeader("access_token", $"{_assasSettings.AccessToken}");
        var response = await client.ExecuteGetAsync(request);
        if (response.IsSuccessful)
        {
            var customers = JsonConvert.DeserializeObject<CustomerResponseDto>(response.Content!,
                new JsonSerializerSettings
                {
                    ContractResolver = new DefaultContractResolver
                    {
                        NamingStrategy = new CamelCaseNamingStrategy()
                    }
                });
            return customers;
        }
        
        Notificator.Handle(JsonConvert.DeserializeObject<AssasError>(response.Content!)!);
        return null;
    }

    public async Task<List<CustomerResponseDto>?> GetByEmail(string email)
    {
        var options = new RestClientOptions($"{_assasSettings.BaseUrl}/customers?email={email}");
        var client = new RestClient(options);
        var request = new RestRequest("");
        request.AddHeader("accept", "application/json");
        request.AddHeader("content-type", "application/json");
        request.AddHeader("access_token", $"{_assasSettings.AccessToken}");
        var response = await client.ExecuteGetAsync(request);
        switch (response.StatusCode)
        {
            case HttpStatusCode.OK:
            {
                var customers = JsonConvert.DeserializeObject<CustomerResponseListDto>(response.Content!, new JsonSerializerSettings
                {
                    ContractResolver = new DefaultContractResolver
                    {
                        NamingStrategy = new CamelCaseNamingStrategy()
                    }
                });
                return customers!.Data;
            }

            case HttpStatusCode.NotFound:
            {
                return null;
            }

            case HttpStatusCode.Unauthorized:
            {
                Notificator.Handle($"Assas error: 401 - Unauthorized");
                return null;
            }
            
            default:
            {
                Notificator.Handle(JsonConvert.DeserializeObject<AssasError>(response.Content!)!);
                return null;
            }
        }
    }

    public async Task<CustomerResponseDto?> Create(CreateCustomerDto dto)
    {
        var options = new RestClientOptions($"{_assasSettings.BaseUrl}/customers");
        var client = new RestClient(options);
        var request = new RestRequest("");
        request.AddHeader("accept", "application/json");
        request.AddHeader("content-type", "application/json");
        request.AddHeader("access_token", $"{_assasSettings.AccessToken}");
        var json = JsonConvert.SerializeObject(dto, new JsonSerializerSettings
        {
            ContractResolver = new DefaultContractResolver
            {
                NamingStrategy = new CamelCaseNamingStrategy()
            }
        });

        request.AddJsonBody(json, false);
        var response = await client.ExecutePostAsync(request);
        switch (response.StatusCode)
        {
            case HttpStatusCode.OK:
            {
                return JsonConvert.DeserializeObject<CustomerResponseDto>(response.Content!, new JsonSerializerSettings
                {
                    ContractResolver = new DefaultContractResolver
                    {
                        NamingStrategy = new CamelCaseNamingStrategy()
                    }
                });
            }
            
            case HttpStatusCode.BadRequest:
            {
                Notificator.Handle(JsonConvert.DeserializeObject<AssasError>(response.Content!)!);
                return null;
            }
            
            case HttpStatusCode.Unauthorized:
            {
                Notificator.Handle($"Assas error: 401 - Unauthorized");
                return null;
            }
            
            default:
            {
                Notificator.Handle(JsonConvert.DeserializeObject<AssasError>(response.Content!)!);
                return null;
            }
            
        }
    }

    public async Task<bool> Remove(string id)
    {
        var options = new RestClientOptions($"{_assasSettings.BaseUrl}/customer/{id}");
        var client = new RestClient(options);
        var request = new RestRequest("");
        request.AddHeader("accept", "application/json");
        request.AddHeader("content-type", "application/json");
        request.AddHeader("access_token", $"{_assasSettings.AccessToken}");
        var response = await client.DeleteAsync(request);

        switch (response.StatusCode)
        {
            case HttpStatusCode.OK:
            {
                return true;
            }
            
            case HttpStatusCode.NotFound:
            {
                Notificator.Handle(JsonConvert.DeserializeObject<AssasError>(response.Content!)!);
                return false;
            }
            
            case HttpStatusCode.Unauthorized:
            {
                Notificator.Handle(JsonConvert.DeserializeObject<AssasError>(response.Content!)!);
                return false;
            }
            
            default:
            {
                Notificator.Handle(JsonConvert.DeserializeObject<AssasError>(response.Content!)!);
                return false;
            }
        }
    }
}