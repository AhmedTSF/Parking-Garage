using Application.DTOs.Payment;
using Application.ServiceInterfaces;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentsController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public PaymentsController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        // POST: api/payments
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreatePaymentDto dto)
        {
            var result = await _paymentService.CreatePaymentAsync(dto);

            if (!result.IsSuccess)
                return BadRequest(new { Message = result.Error });

            return CreatedAtAction(nameof(GetBySessionId), new { sessionId = dto.SessionId }, result.Value);
        }

        // POST: api/payments/pay
        [HttpPost("pay")]
        public async Task<IActionResult> Pay([FromBody] PayPaymentDto dto)
        {
            var result = await _paymentService.PayAsync(dto);

            if (!result.IsSuccess)
                return BadRequest(new { Message = result.Error });

            return Ok(new { Paid = true });
        }

        // GET: api/payments/session/{sessionId}
        [HttpGet("session/{sessionId}")]
        public async Task<IActionResult> GetBySessionId(int sessionId)
        {
            var result = await _paymentService.GetBySessionIdAsync(sessionId);

            if (!result.IsSuccess)
                return NotFound(new { Message = result.Error });

            return Ok(result.Value);
        }

        // GET: api/payments?pageNumber=1&pageSize=10
        [HttpGet]
        public async Task<IActionResult> GetAllDetailed([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var payments = await _paymentService.GetAllDetailedAsync(pageNumber, pageSize);
            return Ok(payments);
        }
    }
}
