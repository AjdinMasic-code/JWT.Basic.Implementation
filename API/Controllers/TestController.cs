using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        // GET: api/<TestController>
        [Authorize]
        [HttpGet]
        public IActionResult TestRequest()
        {
            return Ok("Request accepted and user is authenticated.");
        }
    }
}
