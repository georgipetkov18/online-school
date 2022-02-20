using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineSchoolApi.InputModels;
using OnlineSchoolApi.ResponseModels;
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
            if (!ModelState.IsValid)
            {
                return this.BadRequest(ModelState);
            }

            try
            {
                var authenticatedUserModel = await this.authenticationService
                   .Authenticate(authenticationInputModel.UsernameOrEmail, authenticationInputModel.Password);

                return this.Ok(authenticatedUserModel);
            }

            catch (ArgumentNullException)
            {
                return this.StatusCode(500, new ErrorResponse { ErrorMessage = "Something went wrong"});
            }

            catch (ArgumentException ex)
            {
                return this.BadRequest(new ErrorResponse { ErrorMessage = ex.Message });
            }
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
