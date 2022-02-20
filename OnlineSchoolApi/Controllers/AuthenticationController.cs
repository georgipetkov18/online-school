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

                var cookieOptions = new CookieOptions
                {
                    HttpOnly = true,
                    Expires = DateTime.UtcNow.AddDays(7)
                };

                this.Response.Cookies.Append("refreshToken", authenticatedUserModel.RefreshToken, cookieOptions);

                return this.Ok(authenticatedUserModel.ToAuthenticateResponse());
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
