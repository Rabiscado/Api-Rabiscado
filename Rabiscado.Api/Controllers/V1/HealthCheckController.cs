using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rabiscado.Application.Notifications;

namespace Rabiscado.Api.Controllers.V1;

[AllowAnonymous]
[Route("api/v{version:apiVersion}/[controller]")]
public class HealthCheckController : BaseController
{
    public HealthCheckController(INotificator notificator) : base(notificator)
    {
    }
    
    [HttpGet]
    public IActionResult CheckApiHealth()
    {
        return OkResponse("API is running");
    }
}