using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineSchoolBusinessLogic;
using OnlineSchoolBusinessLogic.Common;
using OnlineSchoolBusinessLogic.InputModels;
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
                ModelState.AddModelError("User", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [Authorize]
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
                var authenticatedUser = await usersService.RefreshTokenAsync(User);
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

        [HttpPut("[action]/{userId}")]
        [Authorize(Policy = Policies.RequireAuthorityRole)]
        public async Task<IActionResult> Approve(Guid userId)
        {
            try
            {
                var user = await usersService.ApproveUserAsync(userId, User);
                return Ok(user.ToUserResponse());
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (InvalidOperationException)
            {
                return Forbid();
            }
        }

        [HttpPut("[action]/{userId}")]
        [Authorize(Policy = Policies.RequireAuthorityRole)]
        public async Task<IActionResult> Reject(Guid userId)
        {
            try
            {
                var user = await usersService.RejectUserAsync(userId, User);
                return Ok(user.ToUserResponse());
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (InvalidOperationException)
            {
                return Forbid();
            }
        }

        [HttpGet("[controller]/[action]")]
        [Authorize(Policy = Policies.RequireAuthorityRole)]
        public async Task<IActionResult> Pending()
        {
            var users = await this.usersService.GetPendingUsersAsync();
            return Ok(users);
        }


        private CookieOptions GetRefreshTokenOptions()
        {
            return new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTime.UtcNow.AddDays(7)
            };
        }
    }
}
