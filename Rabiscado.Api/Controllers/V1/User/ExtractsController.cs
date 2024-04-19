using Microsoft.AspNetCore.Mvc;
using Rabiscado.Application.Contracts;
using Rabiscado.Application.Dtos.V1.Base;
using Rabiscado.Application.Dtos.V1.Extracts;
using Rabiscado.Application.Notifications;
using Rabiscado.Core.Authorization;
using Swashbuckle.AspNetCore.Annotations;

namespace Rabiscado.Api.Controllers.V1.User;

public class ExtractsController : BaseController
{
    private readonly IExtractService _extractService;
    public ExtractsController(INotificator notificator, IExtractService extractService) : base(notificator)
    {
        _extractService = extractService;
    }
    
    [HttpPost]
    [SwaggerOperation(Summary = "Add Extract.", Tags = new [] { "User - Extract" })]
    [ProducesResponseType(typeof(ExtractDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Create([FromForm] CreateExtractDto dto)
    {
        return OkResponse(await _extractService.Create(dto));
    }
    
    [HttpPut("{id:int}")]
    [ClaimsAuthorize("AdminType")]
    [SwaggerOperation(Summary = "Update Extract.", Tags = new [] { "User - Extract" })]
    [ProducesResponseType(typeof(ExtractDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(int id, [FromForm] UpdateExtractDto dto)
    {
        return OkResponse(await _extractService.Update(id,dto));
    }

    [HttpGet("{id:int}")]
    [ClaimsAuthorize("AdminType")]
    [SwaggerOperation(Summary = "Get Extract By Id.", Tags = new[] { "User - Extract" })]
    [ProducesResponseType(typeof(ExtractDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(int id)
    {
        return OkResponse(await _extractService.GetById(id));
    }
    
    [HttpGet]
    [ClaimsAuthorize("AdminType")]
    [SwaggerOperation(Summary = "Get All Extracts.", Tags = new[] { "User - Extract" })]
    [ProducesResponseType(typeof(ExtractDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetAll()
    {
        return OkResponse(await _extractService.GetAll());
    }
    
    [HttpGet("search")]
    [SwaggerOperation(Summary = "Search All Extracts.", Tags = new[] { "User - Extract" })]
    [ProducesResponseType(typeof(PagedDto<ExtractDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Search([FromQuery] SearchExtractDto dto)
    {
        return OkResponse(await _extractService.Search(dto));
    }
    
    [HttpDelete("{id:int}")]
    [ClaimsAuthorize("AdminType")]
    [SwaggerOperation(Summary = "Disable Extract.", Tags = new[] { "User - Extract" })]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Disable(int id)
    {
        await _extractService.Disable(id);
        return NoContentResponse();
    }
    
    [HttpPatch("{id:int}")]
    [ClaimsAuthorize("AdminType")]
    [SwaggerOperation(Summary = "Active Extract.", Tags = new[] { "User - Extract" })]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Active(int id)
    {
        await _extractService.Active(id);
        return NoContentResponse();
    }
}