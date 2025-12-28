using Application.DTOs.User;
using Application.Security.Jwt;
using Application.ServiceInterfaces;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            var result = await _userService.LoginAsync(dto.Username, dto.Password);

            if (!result.IsSuccess)
                return Unauthorized(result.Error);


            return Ok(new { Token = result.Value });
        }
    }
}
