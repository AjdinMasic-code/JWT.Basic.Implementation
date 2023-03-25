using API.Helpers;
using Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace API.Controllers
{
    public class AuthController : ControllerBase
    {

        private readonly ILogger<AuthController> _logger;
        private readonly IAuthService _authService;

        public AuthController(ILogger<AuthController> logger, IAuthService authService)
        {
            _logger = logger;
            _authService = authService;
        }

        [AllowAnonymous]
        [HttpPost("token")]
        public IActionResult CreateToken([FromBody] ClientRequest request)
        {
            if (_authService.IsUserValid(request))
            {
                var token = _authService.GenerateJwtToken();
                return Ok(new { access_token = token });
            }
            else
            {
                return Unauthorized();
            }
        }

    }
}
