using Application.ServiceInterfaces;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CarsController : ControllerBase
    {
        private readonly ICarService _carService;

        public CarsController(ICarService carService)
        {
            _carService = carService;
        }

        // GET: api/cars/{id}
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

        // GET: api/cars?pageNumber=1&pageSize=10
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var cars = await _carService.GetAllAsync(pageNumber, pageSize);
            return Ok(cars);
        }

        // GET: api/cars/detailed?pageNumber=1&pageSize=10
        [HttpGet("detailed")]
        public async Task<IActionResult> GetAllDetailed([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var cars = await _carService.GetAllDetailedAsync(pageNumber, pageSize);
            return Ok(cars);
        }
    }
}
