using Microsoft.AspNetCore.Mvc;
using Rabiscado.Application.Contracts;
using Rabiscado.Application.Dtos.V1.Base;
using Rabiscado.Application.Dtos.V1.Courses;
using Rabiscado.Application.Notifications;
using Rabiscado.Core.Authorization;
using Swashbuckle.AspNetCore.Annotations;

namespace Rabiscado.Api.Controllers.V1.Course;

[Route("api/v{version:apiVersion}/[controller]")]
public class CoursesController : BaseController
{
    private readonly ICourseService _courseService;
    public CoursesController(INotificator notificator, ICourseService courseService) : base(notificator)
    {
        _courseService = courseService;
    }
    
    [HttpPost]
    [ClaimsAuthorize("AdminType")]
    [SwaggerOperation(Summary = "Add Course.", Tags = new [] { "Courses - Course" })]
    [ProducesResponseType(typeof(CourseDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromForm] CreateCourseDto dto)
    {
        return OkResponse(await _courseService.Create(dto));
    }
    
    [HttpPut("{id:int}")]
    [ClaimsAuthorize("AdminType")]
    [SwaggerOperation(Summary = "Update Course.", Tags = new [] { "Courses - Course" })]
    [ProducesResponseType(typeof(CourseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(int id, [FromForm] UpdateCourseDto dto)
    {
        return OkResponse(await _courseService.Update(id,dto));
    }

    [HttpGet("{id:int}")]
    [SwaggerOperation(Summary = "Get Course By Id.", Tags = new[] { "Courses - Course" })]
    [ProducesResponseType(typeof(CourseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(int id)
    {
        return OkResponse(await _courseService.GetById(id));
    }
    
    [HttpGet]
    [SwaggerOperation(Summary = "Get All Courses.", Tags = new[] { "Courses - Course" })]
    [ProducesResponseType(typeof(CourseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetAll()
    {
        return OkResponse(await _courseService.GetAll());
    }
    
    [HttpGet("search")]
    [SwaggerOperation(Summary = "Search Courses.", Tags = new[] { "Courses - Course" })]
    [ProducesResponseType(typeof(PagedDto<CourseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Search([FromQuery] SearchCourseDto dto)
    {
        return OkResponse(await _courseService.Search(dto));
    }
    
    [HttpDelete("{id:int}")]
    [ClaimsAuthorize("AdminType")]
    [SwaggerOperation(Summary = "Disable Course.", Tags = new[] { "Courses - Course" })]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Disable(int id)
    {
        await _courseService.Disable(id);
        return NoContentResponse();
    }
    
    [HttpDelete("delete/{id:int}")]
    [ClaimsAuthorize("AdminType")]
    [SwaggerOperation(Summary = "Delete Course.", Tags = new[] { "Courses - Course" })]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        await _courseService.Delete(id);
        return NoContentResponse();
    }
    
    [HttpPatch("{id:int}")]
    [ClaimsAuthorize("AdminType")]
    [SwaggerOperation(Summary = "Active Course.", Tags = new[] { "Courses - Course" })]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Active(int id)
    {
        await _courseService.Active(id);
        return NoContentResponse();
    }
}