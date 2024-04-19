using Microsoft.AspNetCore.Mvc;
using Rabiscado.Application.Contracts;
using Rabiscado.Application.Dtos.V1.Base;
using Rabiscado.Application.Dtos.V1.Scheduledpayments;
using Rabiscado.Application.Notifications;
using Rabiscado.Core.Authorization;
using Swashbuckle.AspNetCore.Annotations;

namespace Rabiscado.Api.Controllers.V1.User;

[Route("api/v{version:apiVersion}/[controller]")]
public class ScheduledpaymentsController : BaseController
{
    private readonly IScheduledpaymentService _scheduledpaymentService;
    public ScheduledpaymentsController(INotificator notificator, IScheduledpaymentService scheduledpaymentService) : base(notificator)
    {
        _scheduledpaymentService = scheduledpaymentService;
    }
    
    [HttpPost]
    [ClaimsAuthorize("AdminType")]
    [SwaggerOperation(Summary = "Add Scheduledpayment.", Tags = new [] { "User - Scheduledpayment" })]
    [ProducesResponseType(typeof(ScheduledpaymentDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateScheduledpaymentDto dto)
    {
        return OkResponse(await _scheduledpaymentService.Create(dto));
    }
    
    [HttpPut("{id:int}")]
    [ClaimsAuthorize("AdminType")]
    [SwaggerOperation(Summary = "Update Scheduledpayment.", Tags = new [] { "User - Scheduledpayment" })]
    [ProducesResponseType(typeof(ScheduledpaymentDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateScheduledpaymentDto dto)
    {
        return OkResponse(await _scheduledpaymentService.Update(id,dto));
    }

    [HttpGet("{id:int}")]
    [ClaimsAuthorize("AdminType")]
    [SwaggerOperation(Summary = "Get Scheduledpayment By Id.", Tags = new[] { "User - Scheduledpayment" })]
    [ProducesResponseType(typeof(ScheduledpaymentDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(int id)
    {
        return OkResponse(await _scheduledpaymentService.GetById(id));
    }
    
    [HttpGet]
    [ClaimsAuthorize("AdminType")]
    [SwaggerOperation(Summary = "Get All Scheduledpayments.", Tags = new[] { "User - Scheduledpayment" })]
    [ProducesResponseType(typeof(ScheduledpaymentDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetAll()
    {
        return OkResponse(await _scheduledpaymentService.GetAll());
    }
    
    [HttpGet("search")]
    [SwaggerOperation(Summary = "Search Scheduledpayments.", Tags = new[] { "User - Scheduledpayment" })]
    [ProducesResponseType(typeof(PagedDto<ScheduledpaymentDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Search([FromQuery] SearchScheduledpaymentDto dto)
    {
        return OkResponse(await _scheduledpaymentService.Search(dto));
    }
    
    [HttpDelete("{id:int}")]
    [ClaimsAuthorize("AdminType")]
    [SwaggerOperation(Summary = "Disable Scheduledpayment.", Tags = new[] { "User - Scheduledpayment" })]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Disable(int id)
    {
        await _scheduledpaymentService.Disable(id);
        return NoContentResponse();
    }
    
    [HttpPatch("{id:int}")]
    [ClaimsAuthorize("AdminType")]
    [SwaggerOperation(Summary = "Active Scheduledpayment.", Tags = new[] { "User - Scheduledpayment" })]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Active(int id)
    {
        await _scheduledpaymentService.Active(id);
        return NoContentResponse();
    }
    
    [HttpPatch("mark-paid/{id:int}")]
    [ClaimsAuthorize("AdminType")]
    [SwaggerOperation(Summary = "Mark Scheduledpayment as PaidOut.", Tags = new[] { "User - Scheduledpayment" })]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> MarkAsPaidOut(int id)
    {
        await _scheduledpaymentService.MarkAsPaid(id);
        return NoContentResponse();
    }
    
    [HttpPatch("mark-unpaid/{id:int}")]
    [ClaimsAuthorize("AdminType")]
    [SwaggerOperation(Summary = "Mark Scheduledpayment as UnPaid.", Tags = new[] { "User - Scheduledpayment" })]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> MarkAsUnPaidOut(int id)
    {
        await _scheduledpaymentService.MarkAsUnPaid(id);
        return NoContentResponse();
    }
}