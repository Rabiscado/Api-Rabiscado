using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rabiscado.Application.Adapters.Assas.Application.Dtos.V1.Subscriptions;
using Rabiscado.Application.Contracts;
using Rabiscado.Application.Dtos.V1.Base;
using Rabiscado.Application.Dtos.V1.Plan;
using Rabiscado.Application.Dtos.V1.Subscription;
using Rabiscado.Application.Dtos.V1.Users;
using Rabiscado.Application.Notifications;
using Rabiscado.Core.Authorization;
using Swashbuckle.AspNetCore.Annotations;

namespace Rabiscado.Api.Controllers.V1.User;

[Route("api/v{version:apiVersion}/[controller]")]
public class UsersController : BaseController
{
    private readonly IUserService _userService;
    public UsersController(INotificator notificator, IUserService userService) : base(notificator)
    {
        _userService = userService;
    }
    
    [HttpPost]
    [AllowAnonymous]
    [SwaggerOperation(Summary = "Add User.", Tags = new [] { "Users - User" })]
    [ProducesResponseType(typeof(UserDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Add([FromBody] CreateUserDto dto)
    {
        return OkResponse(await _userService.Create(dto));
    }
    
    [HttpPut("{id:int}")]
    [SwaggerOperation(Summary = "Update User.", Tags = new [] { "Users - User" })]
    [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateUserDto dto)
    {
        return OkResponse(await _userService.Update(id,dto));
    }
    
    [HttpPatch("toogle-admin/{id:int}")]
    [ClaimsAuthorize("AdminType")]
    [SwaggerOperation(Summary = "Toogle Admin.", Tags = new [] { "Users - User" })]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ToogleAdmin(int id)
    {
        await _userService.ToggleAdmin(id);
        return NoContentResponse();
    }
    
    [HttpPatch("toogle-instructor/{id:int}")]
    [ClaimsAuthorize("AdminType")]
    [SwaggerOperation(Summary = "Toogle Instructor.", Tags = new [] { "Users - User" })]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ToogleInstructor(int id)
    {
        await _userService.ToggleInstructor(id);
        return NoContentResponse();
    }
    

    [HttpGet("{id:int}")]
    [SwaggerOperation(Summary = "Get User By Id.", Tags = new[] { "Users - User" })]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(int id)
    {
        return OkResponse(await _userService.GetById(id));
    }
    
    [HttpGet]
    [ClaimsAuthorize("AdminType")]
    [SwaggerOperation(Summary = "Get All Users.", Tags = new[] { "Users - User" })]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetAll()
    {
        return OkResponse(await _userService.GetAll());
    }
    
    [HttpGet("search")]
    [SwaggerOperation(Summary = "Search Users.", Tags = new[] { "Users - User" })]
    [ProducesResponseType(typeof(PagedDto<UserDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Search([FromQuery] UserSearchDto dto)
    {
        return OkResponse(await _userService.Search(dto));
    }
    
    [HttpDelete("{id:int}")]
    [SwaggerOperation(Summary = "Disable User.", Tags = new[] { "Users - User" })]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Disable(int id)
    {
        await _userService.Disable(id);
        return NoContentResponse();
    }
    
    [HttpPatch("{id:int}")]
    [SwaggerOperation(Summary = "Active User.", Tags = new[] { "Users - User" })]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Active(int id)
    {
        await _userService.Active(id);
        return NoContentResponse();
    }
    
    [HttpPost("course-subscribe")]
    [SwaggerOperation(Summary = "Sign up for a course.", Tags = new [] { "Users - User" })]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Subscribe([FromBody] SubscribeDto dto)
    {
        var response = await _userService.Subscribe(dto);
        return OkResponse(response);
    }
    
    [HttpDelete("course-unsubscribe")]
    [SwaggerOperation(Summary = "Unsubscribe for a course.", Tags = new [] { "Users - User" })]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Unsubscribe([FromBody] UnsubscribeDto dto)
    {
        var response = await _userService.Unsubscribe(dto);
        return OkResponse(response);
    }
    
    [HttpPatch("hide-subscription/{id:int}")]
    [SwaggerOperation(Summary = "Hide Subscription.", Tags = new[] { "Users - User" })]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> HideSubscription(int id)
    {
        var response = await _userService.HideSubscription(id);
        return OkResponse(response);
    }
    
    
    [HttpPost("plan-subscribe")]
    [SwaggerOperation(Summary = "Subscribe To A Plan.", Tags = new [] { "Users - User" })]
    [ProducesResponseType(typeof(SubscriptionResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Create([FromBody] SubscribePlanDto dto)
    {
        return OkResponse(await _userService.Subscribe(dto));
    }
    
    [HttpDelete("plan-unsubscribe")]
    [SwaggerOperation(Summary = "Unsubscribe For A Plan.", Tags = new [] { "Users - User" })]
    [ProducesResponseType(typeof(SubscriptionUnsubscribreResponseDto) ,StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Unsubscribe([FromQuery] string email)
    {
        return OkResponse(await _userService.Unsubscribe(email));
    }
    
    [HttpGet("dashboard-admin")]
    [ClaimsAuthorize("AdminType")]
    [SwaggerOperation(Summary = "Get Dashboard ADM.", Tags = new[] { "Users - User" })]
    [ProducesResponseType(typeof(DashboardAdminDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetDashboardAdmin()
    {
        return OkResponse(await _userService.DashboardAdmin());
    }
    
    [HttpGet("dashboard-professor")]
    [ClaimsAuthorize("ProfessorType")]
    [SwaggerOperation(Summary = "Get Dashboard Professor.", Tags = new[] { "Users - User" })]
    [ProducesResponseType(typeof(DashboardProfessorDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetDashboardProfessor()
    {
        return OkResponse(await _userService.DashboardProfessor());
    }
    
    [HttpGet("receipts-pdf")]
    [ClaimsAuthorize("AdminType")]
    [SwaggerOperation(Summary = "Generate Receipts PDF", Tags = new[] { "Users - User" })]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GenerateReceiptsPdf()
    {
        return OkResponse(await _userService.GenerateReceiptsPdf());
    }
    
    [HttpGet("professors-pdf")]
    [ClaimsAuthorize("AdminType")]
    [SwaggerOperation(Summary = "Generate Professors PDF", Tags = new[] { "Users - User" })]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GenerateProfessorsPdf()
    {
        return OkResponse(await _userService.GenerateProfessorsPdf());
    }
    
    [HttpGet("professors-receipt-pdf")]
    [ClaimsAuthorize("AdminType")]
    [SwaggerOperation(Summary = "Generate Professors Receipt PDF", Tags = new[] { "Users - User" })]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GenerateProfessorsReceiptPdf()
    {
        return OkResponse(await _userService.GenerateProfessorsReceiptPdf());
    }
    
    [HttpGet("professors-monthly-receipt-pdf")]
    [ClaimsAuthorize("ProfessorType")]
    [SwaggerOperation(Summary = "Generate Professors Monthly Receipt PDF", Tags = new[] { "Users - User" })]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GenerateProfessorMonthlyReceiptPdf()
    {
        return OkResponse(await _userService.GenerateProfessorMonthlyReceiptPdf());
    }
    
    [HttpPatch("mark-as-watched/{id:int}")]
    [SwaggerOperation(Summary = "Mark As Watched.", Tags = new[] { "Users - Class" })]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> MarkAsWatched(int id)
    {
        await _userService.MarkAsWatched(id);
        return NoContentResponse();
    }
    
    [HttpPatch("mark-as-unwatched/{id:int}")]
    [SwaggerOperation(Summary = "Mark As Unwatched.", Tags = new[] { "Users - Class" })]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> MarkAsUnwatched(int id)
    {
        await _userService.MarkAsUnWatched(id);
        return NoContentResponse();
    }
}