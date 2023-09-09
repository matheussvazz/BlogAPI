using Microsoft.AspNetCore.Mvc;
using Blog.Services;

namespace Blog.Controllers
{
    [ApiController]
    public class AccountController : ControllerBase
    {

        [HttpPost("v1/login")]
        public IActionResult Login([FromServices] TokenService tokenService) // dependem do item TokenService
        {
            var token = tokenService.GenerateToken(null); // gerando token baseado no tokenService

            return Ok(token);
        }
    }
}