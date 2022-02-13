using Microsoft.AspNetCore.Mvc;

namespace OnlineSchoolApi.Controllers
{
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        [HttpPost("[action]")]
        public async Task<IActionResult> Authenticate()
        {
            return Ok();
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Register()
        {
            return Ok();
        }
    }
}
