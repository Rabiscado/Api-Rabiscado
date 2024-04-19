using Microsoft.AspNetCore.Mvc;
using Rabiscado.Application.Contracts;
using Rabiscado.Application.Dtos.V1.Level;
using Rabiscado.Application.Notifications;
using Swashbuckle.AspNetCore.Annotations;

namespace Rabiscado.Api.Controllers.V1.Course;

[Route("api/v{version:apiVersion}/[controller]")]
public class LevelsController : BaseController
{
    private readonly ILevelService _levelService;
    public LevelsController(INotificator notificator, ILevelService levelService) : base(notificator)
    {
        _levelService = levelService;
    }
    
    [HttpGet("{id:int}")]
    [SwaggerOperation(Summary = "Get Level By Id.", Tags = new[] { "Courses - Level" })]
    [ProducesResponseType(typeof(LevelDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(int id)
    {
        return OkResponse(await _levelService.GetById(id));
    }
    
    [HttpGet]
    [SwaggerOperation(Summary = "Get All Levels.", Tags = new[] { "Courses - Level" })]
    [ProducesResponseType(typeof(LevelDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetAll()
    {
        return OkResponse(await _levelService.GetAll());
    }
}