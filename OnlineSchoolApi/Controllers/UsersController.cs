using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineSchoolApi.InputModels;
using OnlineSchoolBusinessLogic.Interfaces;

namespace OnlineSchoolApi.Controllers
{
    [ApiController]
    [Route("api")]
    public class UsersController : ControllerBase
    {
        private readonly IUsersService usersService;

        public UsersController(IUsersService authenticationService)
        {
            this.usersService = authenticationService;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Authenticate(AuthenticationInputModel authenticationInputModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var authenticatedUser = await this.usersService.AuthenticateAsync(authenticationInputModel.UsernameOrEmail, authenticationInputModel.Password);

                var cookieOptions = GetRefreshTokenOptions();

                this.Response.Cookies.Append("refreshToken", authenticatedUser.RefreshToken, cookieOptions);
                return Ok(authenticatedUser.ToAuthenticateResponse());
            }
            catch (ArgumentException ex)
            {
                ModelState.AddModelError("Credentials", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Register(UserInputModel user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                await usersService.RegisterAsync(user.ToUser());

                return Ok(new { message = $"Registered user: {user.Username}" });
            }
            catch (ArgumentException ex)
            {
                ModelState.AddModelError(user.RoleName, ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken()
        {
            var refreshToken = Request.Cookies["refreshToken"];
            if (refreshToken is null)
            {
                return BadRequest(new { message = "Token is required" });
            }

            try
            {
                var authenticatedUser = await usersService.RefreshTokenAsync(refreshToken);
                var cookieOptions = GetRefreshTokenOptions();

                this.Response.Cookies.Append("refreshToken", authenticatedUser.RefreshToken, cookieOptions);

                return Ok(authenticatedUser.ToAuthenticateResponse());
            }
            catch (ArgumentNullException)
            {
                return BadRequest(new { message = "Invalid refresh token was provided" });

            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message, tokenExpired = true });
            }
        }

        private CookieOptions GetRefreshTokenOptions()
        {
            return new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTime.UtcNow.AddDays(7)
            };
        }

        [Authorize]
        [HttpGet("test")]
        public IActionResult Test()
        {
            return Ok();
        }
    }
}
