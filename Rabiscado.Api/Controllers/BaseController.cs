using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Rabiscado.Api.Responses;
using Rabiscado.Application.Notifications;

namespace Rabiscado.Api.Controllers;

[Authorize]
[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public abstract class BaseController : Controller
{
    private readonly INotificator _notificator;
    
    protected BaseController(INotificator notificator)
    {
        _notificator = notificator;
    }
    
    protected IActionResult NoContentResponse() 
        => CustomResponse(NoContent());

    protected IActionResult CreatedResponse(string uri = "", object? result = null) 
        => CustomResponse(Created(uri, result));

    protected IActionResult OkResponse(object? result = null) 
        => CustomResponse(Ok(result));

    private IActionResult CustomResponse(IActionResult objectResult)
    {
        if (ValidOperation)
        {
            return objectResult;
        }
        
        if (_notificator.IsNotFoundResource)
        {
            return NotFound();
        }

        var response = new BadRequestResponse(_notificator.GetNotifications().ToList());
        return BadRequest(response);
    }
    
    protected IActionResult CustomResponse(ModelStateDictionary modelState)
    {
        var erros = modelState.Values.SelectMany(e => e.Errors);
        foreach (var erro in erros)
        {
            AddErrorProcessing(erro.ErrorMessage);
        }

        return CustomResponse(Ok(null));
    }

    protected IActionResult CustomResponse(ValidationResult validationResult)
    {
        foreach (var erro in validationResult.Errors)
        {
            AddErrorProcessing(erro.ErrorMessage);
        }

        return CustomResponse(Ok(null));
    }
    
    private bool ValidOperation => !(_notificator.HasNotification || _notificator.IsNotFoundResource);

    private void AddErrorProcessing(string erro) => _notificator.Handle(erro);
}