using Microsoft.AspNetCore.Mvc;
using Rabiscado.Application.Contracts;
using Rabiscado.Application.Dtos.V1.Class;
using Rabiscado.Application.Notifications;
using Rabiscado.Core.Authorization;
using Swashbuckle.AspNetCore.Annotations;

namespace Rabiscado.Api.Controllers.V1.Course;

[Route("api/v{version:apiVersion}/[controller]")]
public class ClassesController : BaseController
{
    private readonly IClassService _classService;
    public ClassesController(INotificator notificator, IClassService classService) : base(notificator)
    {
        _classService = classService;
    }
    
    [HttpPost]
    [ClaimsAuthorize("AdminType")]
    [SwaggerOperation(Summary = "Add Class.", Tags = new [] { "Courses - Class" })]
    [ProducesResponseType(typeof(ClassDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromForm] CreateClassDto dto)
    {
        return OkResponse(await _classService.Create(dto));
    }
    
    [HttpPut("{id:int}")]
    [ClaimsAuthorize("AdminType")]
    [SwaggerOperation(Summary = "Update Class.", Tags = new [] { "Courses - Class" })]
    [ProducesResponseType(typeof(ClassDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(int id, [FromForm] UpdateClassDto dto)
    {
        return OkResponse(await _classService.Update(id,dto));
    }

    [HttpGet("{id:int}")]
    [SwaggerOperation(Summary = "Get Class By Id.", Tags = new[] { "Courses - Class" })]
    [ProducesResponseType(typeof(ClassDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(int id)
    {
        return OkResponse(await _classService.GetById(id));
    }
    
    [HttpGet]
    [SwaggerOperation(Summary = "Get All Classs.", Tags = new[] { "Courses - Class" })]
    [ProducesResponseType(typeof(ClassDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetAll()
    {
        return OkResponse(await _classService.GetAll());
    }
    
    [HttpDelete("{id:int}")]
    [ClaimsAuthorize("AdminType")]
    [SwaggerOperation(Summary = "Disable Class.", Tags = new[] { "Courses - Class" })]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Disable(int id)
    {
        await _classService.Disable(id);
        return NoContentResponse();
    }
    
    [HttpPatch("{id:int}")]
    [ClaimsAuthorize("AdminType")]
    [SwaggerOperation(Summary = "Active Class.", Tags = new[] { "Courses - Class" })]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Active(int id)
    {
        await _classService.Active(id);
        return NoContentResponse();
    }
}