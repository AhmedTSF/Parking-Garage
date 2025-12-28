using Application.DTOs.Payment;
using Application.Security;
using Application.ServiceInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Authorize(Roles = $"{Roles.User},{Roles.Admin}")]
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentsController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public PaymentsController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpPut("pay")]
        public async Task<IActionResult> Pay([FromBody] PayPaymentDto dto)
        {
            var result = await _paymentService.PayAsync(dto);

            if (!result.IsSuccess)
                return BadRequest(new { Message = result.Error });

            return Ok(new { Paid = true });
        }

        [HttpGet("session/{sessionId}")]
        public async Task<IActionResult> GetBySessionId(int sessionId)
        {
            var result = await _paymentService.GetBySessionIdAsync(sessionId);

            if (!result.IsSuccess)
                return NotFound(new { Message = result.Error });

            return Ok(result.Value);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllDetailed([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var payments = await _paymentService.GetAllDetailedAsync(pageNumber, pageSize);
            return Ok(payments);
        }

        [HttpGet("plate-number/{palteNumber}")]
        public async Task<IActionResult> GetByCarPlateNumberAsync(string palteNumber)
        {
            var carPayments = await _paymentService.GetByCarPlateNumberAsync(palteNumber);

            return Ok(carPayments);
        }
    }
}
