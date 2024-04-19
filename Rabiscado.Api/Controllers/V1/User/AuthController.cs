using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rabiscado.Application.Contracts;
using Rabiscado.Application.Dtos.V1.Auth;
using Rabiscado.Application.Dtos.V1.User;
using Rabiscado.Application.Dtos.V1.Users;
using Rabiscado.Application.Notifications;
using Swashbuckle.AspNetCore.Annotations;

namespace Rabiscado.Api.Controllers.V1.User;

[AllowAnonymous]
[Route("api/v{version:apiVersion}/[controller]")]
public class AuthController : BaseController
{
    private readonly IUserAuthService _userAuthService;
    public AuthController(INotificator notificator, IUserAuthService userAuthService) : base(notificator)
    {
        _userAuthService = userAuthService;
    }
    
    [HttpPost]
    [SwaggerOperation(Summary = "User Login.", Tags = new [] { "User - Auth" })]
    [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Login([FromBody] LoginDto dto)
    {
        var token = await _userAuthService.Login(dto);
        return token != null ? OkResponse(token) : BadRequest(new[] { "User and/or password are incorrect" });
    }
    
    [HttpPost("recover-password")]
    [SwaggerOperation(Summary = "Send email to recover user password.", Tags = new [] { "User - Auth" })]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> RecoverPassword([FromQuery] string email)
    {
        await _userAuthService.SendEmailRecoverPassword(email);
        return NoContentResponse();
    }
    
    [HttpPost("reset-password")]
    [Authorize]
    [SwaggerOperation(Summary = "Reset user password.", Tags = new [] { "User - Auth" })]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordUserDto dto)
    {
        await _userAuthService.ChangePassword(dto);
        return NoContentResponse();
    }
    
    [HttpPost("change-password")]
    [SwaggerOperation(Summary = "Change user password.", Tags = new [] { "User - Auth" })]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordAuthenticatedUserDto dto)
    {
        await _userAuthService.ChangePassword(dto);
        return OkResponse();
    }
}