using Application.Security;
using Application.ServiceInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.ActionFilters;
using Presentation.Helpers;

namespace Presentation.Controllers;

[Authorize(Roles = $"{Roles.User},{Roles.Admin}")]
[ApiController]
[Route("api/[controller]")]
public class CarsController : ControllerBase
{
    private readonly ICarService _carService;

    public CarsController(ICarService carService)
    {
        _carService = carService;
    }

    [ValidatePositive(Params.Identification.Id)]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        try
        {
            var car = await _carService.GetByIdAsync(id);
            return Ok(car);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { Message = ex.Message });
        }
    }

    [ValidatePositive(Params.Pagination.PageNumber, Params.Pagination.PageSize)]
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
    {
        var cars = await _carService.GetAllAsync(pageNumber, pageSize);
        return Ok(cars);
    }

    [ValidatePositive(Params.Pagination.PageNumber, Params.Pagination.PageSize)]
    [HttpGet("detailed")]
    public async Task<IActionResult> GetAllDetailed([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
    {
        var cars = await _carService.GetAllDetailedAsync(pageNumber, pageSize);
        return Ok(cars);
    }
}
