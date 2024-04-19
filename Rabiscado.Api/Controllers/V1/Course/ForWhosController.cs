using Microsoft.AspNetCore.Mvc;
using Rabiscado.Application.Contracts;
using Rabiscado.Application.Dtos.V1.ForWho;
using Rabiscado.Application.Notifications;
using Swashbuckle.AspNetCore.Annotations;

namespace Rabiscado.Api.Controllers.V1.Course;

[Route("api/v{version:apiVersion}/[controller]")]
public class ForWhosController : BaseController
{
    private readonly IForWhoService _forWhoService;
    public ForWhosController(INotificator notificator, IForWhoService forWhoService) : base(notificator)
    {
        _forWhoService = forWhoService;
    }
    
    [HttpGet("{id:int}")]
    [SwaggerOperation(Summary = "Get ForWho By Id.", Tags = new[] { "Courses - ForWho" })]
    [ProducesResponseType(typeof(ForWhoDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(int id)
    {
        return OkResponse(await _forWhoService.GetById(id));
    }
    
    [HttpGet]
    [SwaggerOperation(Summary = "Get All ForWhos.", Tags = new[] { "Courses - ForWho" })]
    [ProducesResponseType(typeof(ForWhoDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetAll()
    {
        return OkResponse(await _forWhoService.GetAll());
    }
}