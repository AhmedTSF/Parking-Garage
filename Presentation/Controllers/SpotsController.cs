using Application.DTOs.Spot;
using Application.Security;
using Application.ServiceInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.ActionFilters;
using Presentation.Helpers;

namespace Presentation.Controllers;


[ApiController]
[Route("api/[controller]")]
public class SpotsController : ControllerBase
{
    private readonly ISpotService _spotService;

    public SpotsController(ISpotService spotService)
    {
        _spotService = spotService;
    }

    [Authorize(Roles = $"{Roles.User},{Roles.Admin}")]
    [HttpGet("{id}")]
    public async Task<ActionResult> GetById(int id)
    {
        var spot = await _spotService.GetByIdAsync(id);

        if(!spot.IsSuccess)
            return NotFound(spot.Error);

        return Ok(spot.Value);
    }

    [Authorize(Roles = $"{Roles.Admin}")]
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateSpotDto dto)
    {
        if (dto == null)
            return BadRequest("Invalid spot data.");

        var spotId = await _spotService.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = spotId }, new { Id = spotId });
    }

    [Authorize(Roles = $"{Roles.User},{Roles.Admin}")]
    [ValidatePositive(Params.Pagination.PageNumber, Params.Pagination.PageSize)]
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
    {
        var spots = await _spotService.GetAllAsync(pageNumber, pageSize);
        return Ok(spots);
    }

    [AllowAnonymous]
    [ValidatePositive(Params.Pagination.PageNumber, Params.Pagination.PageSize)]
    [HttpGet("available")]
    public async Task<IActionResult> GetAvailable([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
    {
        var spots = await _spotService.GetAvailableSpotsAsync(pageNumber, pageSize);
        return Ok(spots);
    }

    [AllowAnonymous]
    [HttpGet("available/count")]
    public async Task<IActionResult> GetAvailableCount()
    {
        var count = await _spotService.GetAvailableSpotCountAsync();
        return Ok(new { AvailableSpotCount = count });
    }
}
