using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rabiscado.Application.Adapters.Assas.Application.Dtos.V1.Payments;
using Rabiscado.Application.Contracts;
using Rabiscado.Application.Dtos.V1.Plan;
using Rabiscado.Application.Notifications;
using Rabiscado.Core.Authorization;
using Swashbuckle.AspNetCore.Annotations;

namespace Rabiscado.Api.Controllers.V1.User;

[Route("api/v{version:apiVersion}/[controller]")]
public class PlansController : BaseController
{
    private readonly IPlanService _planService;
    public PlansController(INotificator notificator, IPlanService planService) : base(notificator)
    {
        _planService = planService;
    }
    
    [HttpPost]
    [ClaimsAuthorize("AdminType")]
    [SwaggerOperation(Summary = "Add Plan.", Tags = new [] { "User - Plan" })]
    [ProducesResponseType(typeof(PlanDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Create([FromBody] CreatePlanDto dto)
    {
        return OkResponse(await _planService.Create(dto));
    }
    
    [HttpPut("{id:int}")]
    [ClaimsAuthorize("AdminType")]
    [SwaggerOperation(Summary = "Update Plan.", Tags = new [] { "User - Plan" })]
    [ProducesResponseType(typeof(PlanDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(int id, [FromBody] UpdatePlanDto dto)
    {
        return OkResponse(await _planService.Update(id,dto));
    }

    [HttpGet("{id:int}")]
    [SwaggerOperation(Summary = "Get Plan By Id.", Tags = new[] { "User - Plan" })]
    [ProducesResponseType(typeof(PlanDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(int id)
    {
        return OkResponse(await _planService.GetById(id));
    }
    
    [HttpGet]
    [SwaggerOperation(Summary = "Get All Plans.", Tags = new[] { "User - Plan" })]
    [ProducesResponseType(typeof(PlanDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetAll()
    {
        return OkResponse(await _planService.GetAll());
    }
    
    [HttpDelete("{id:int}")]
    [ClaimsAuthorize("AdminType")]
    [SwaggerOperation(Summary = "Disable Plan.", Tags = new[] { "User - Plan" })]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Disable(int id)
    {
        await _planService.Disable(id);
        return NoContentResponse();
    }
    
    [HttpPatch("{id:int}")]
    [ClaimsAuthorize("AdminType")]
    [SwaggerOperation(Summary = "Active Plan.", Tags = new[] { "User - Plan" })]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Active(int id)
    {
        await _planService.Active(id);
        return NoContentResponse();
    }
    
    [HttpPost("hook")]
    [AllowAnonymous]
    [SwaggerOperation(Summary = "Verify payment.", Tags = new[] { "User - Plan" })]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> VerifyPayment([FromBody] SubscriptionHookDto dto)
    {
        await _planService.VerifyPayment(dto);
        return OkResponse("");
    }
}