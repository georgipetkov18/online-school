using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineSchoolApi.InputModels;
using OnlineSchoolBusinessLogic.Interfaces;

namespace OnlineSchoolApi.Controllers
{
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService authenticationService;

        public AuthenticationController(IAuthenticationService authenticationService)
        {
            this.authenticationService = authenticationService;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Authenticate(AuthenticationInputModel authenticationInputModel)
        {
            var result = await this.authenticationService
                .Authenticate(authenticationInputModel.UsernameOrEmail, authenticationInputModel.Password);
            return Ok(result);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Register()
        {
            return Ok();
        }

        [HttpGet("[action]")]
        [Authorize]
        public IActionResult Test()
        {
            return Ok("Authenticated");
        }
    }
}
