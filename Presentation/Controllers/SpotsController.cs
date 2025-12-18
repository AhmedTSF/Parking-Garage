using Application.DTOs.Spot;
using Application.ServiceInterfaces;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SpotsController : ControllerBase
    {
        private readonly ISpotService _spotService;

        public SpotsController(ISpotService spotService)
        {
            _spotService = spotService;
        }

        // POST: api/spots
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateSpotDto dto)
        {
            if (dto == null)
                return BadRequest("Invalid spot data.");

            var spotId = await _spotService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetAll), new { id = spotId }, new { Id = spotId });
        }

        // GET: api/spots?pageNumber=1&pageSize=10
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var spots = await _spotService.GetAllAsync(pageNumber, pageSize);
            return Ok(spots);
        }

        // GET: api/spots/available?pageNumber=1&pageSize=10
        [HttpGet("available")]
        public async Task<IActionResult> GetAvailable([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var spots = await _spotService.GetAvailableSpotsAsync(pageNumber, pageSize);
            return Ok(spots);
        }

        // GET: api/spots/available/count
        [HttpGet("available/count")]
        public async Task<IActionResult> GetAvailableCount()
        {
            var count = await _spotService.GetAvailableSpotCountAsync();
            return Ok(new { AvailableSpotCount = count });
        }
    }
}
