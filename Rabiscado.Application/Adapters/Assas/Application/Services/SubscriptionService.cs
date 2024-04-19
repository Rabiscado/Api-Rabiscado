using System.Net;
using AutoMapper;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Rabiscado.Application.Adapters.Assas.Application.Contracts;
using Rabiscado.Application.Adapters.Assas.Application.Dtos.V1.Subscriptions;
using Rabiscado.Application.Dtos.V1.Subscription;
using Rabiscado.Application.Notifications;
using Rabiscado.Application.Notifications.CustomerEntitiesError;
using Rabiscado.Application.Services;
using Rabiscado.Core.Settings;
using RestSharp;
using SubscriptionDto = Rabiscado.Application.Adapters.Assas.Application.Dtos.V1.Payments.SubscriptionDto;

namespace Rabiscado.Application.Adapters.Assas.Application.Services;

public class SubscriptionService : BaseService, ISubscriptionService
{
    private readonly AssasSettings _assasSettings;

    public SubscriptionService(IMapper mapper, INotificator notificator, IOptions<AssasSettings> assasSettings) : base(
        mapper, notificator)
    {
        _assasSettings = assasSettings.Value;
    }

    public async Task<SubscriptionResponseDto?> Create(SubscriptionDto dto)
    {
        var options = new RestClientOptions($"{_assasSettings.BaseUrl}/subscriptions");
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
            },
            Formatting = Formatting.None,
            DateFormatString = "yyyy-MM-dd"
        });
        request.AddJsonBody(json, false);
        var response = await client.ExecutePostAsync(request);
        switch (response.StatusCode)
        {
            case HttpStatusCode.OK:
            {
                var subscription = JsonConvert.DeserializeObject<SubscriptionResponseDto>(response.Content!);
                return subscription;
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

    public async Task<SubscriptionResponseListDto?> GetByCustomerId(string customerId)
    {
        var options = new RestClientOptions($"{_assasSettings.BaseUrl}/subscriptions/?customer={customerId}");
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
                var customers = JsonConvert.DeserializeObject<SubscriptionResponseListDto>(response.Content!);
                return customers;
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

    public async Task<SubscriptionUnsubscribreResponseDto?> Unsubscribe(string id)
    {
        var options = new RestClientOptions($"{_assasSettings.BaseUrl}/subscriptions/{id}");
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
                var subscription = JsonConvert.DeserializeObject<SubscriptionUnsubscribreResponseDto>(response.Content!);
                return subscription!;
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
}