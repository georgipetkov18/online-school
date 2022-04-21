using OnlineSchoolBusinessLogic.Models;
using System.Security.Claims;

namespace OnlineSchoolBusinessLogic.Interfaces
{
    public interface IUsersService
    {
        Task<AuthenticateModel> AuthenticateAsync(string usernameOrEmail, string password, bool hashedPassword = false);
        Task RegisterAsync(User user);
        Task<AuthenticateModel> RefreshTokenAsync(ClaimsPrincipal user);
        Task<User> ApproveUserAsync(Guid userId, ClaimsPrincipal approver);
        Task<User> RejectUserAsync(Guid userId, ClaimsPrincipal rejecter);
        Task<IEnumerable<User>> GetPendingUsersAsync();
    }

}
