using Application.DTOs.Session;
using Application.ServiceInterfaces;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SessionsController : ControllerBase
    {
        private readonly ISessionService _sessionService;

        public SessionsController(ISessionService sessionService)
        {
            _sessionService = sessionService;
        }

        // POST: api/sessions/start
        [HttpPost("start")]
        public async Task<IActionResult> Start([FromBody] CreateSessionDto dto)
        {
            var result = await _sessionService.StartSessionAsync(dto);

            if (!result.IsSuccess)
                return BadRequest(new { Message = result.Error });

            return CreatedAtAction(nameof(GetById), new { sessionId = result.Value }, new { SessionId = result.Value });
        }

        // POST: api/sessions/{sessionId}/end
        [HttpPost("{sessionId}/end")]
        public async Task<IActionResult> End(int sessionId)
        {
            var result = await _sessionService.EndSessionAsync(sessionId);

            if (!result.IsSuccess)
                return NotFound(new { Message = result.Error });

            return Ok(result.Value);
        }

        // GET: api/sessions/{sessionId}
        [HttpGet("{sessionId}")]
        public async Task<IActionResult> GetById(int sessionId)
        {
            var result = await _sessionService.GetByIdAsync(sessionId);

            if (!result.IsSuccess)
                return NotFound(new { Message = result.Error });

            return Ok(result.Value);
        }

        // GET: api/sessions?pageNumber=1&pageSize=10
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var sessions = await _sessionService.GetAllAsync(pageNumber, pageSize);
            return Ok(sessions);
        }

        [HttpGet("active")]
        public async Task<IActionResult> GetAllActive([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var sessions = await _sessionService.GetAllActiveAsync(pageNumber, pageSize);
            return Ok(sessions);
        }
    }
}
