using OnlineSchoolBusinessLogic.Models;
using System.Security.Claims;

namespace OnlineSchoolBusinessLogic.Interfaces
{
    public interface IUsersRepository
    {
        Task<User> GetUserAsync(string usernameOrEmail, string password, bool hashedPassword = false);
        Task<User> GetUserAsync(string usernameOrEmail);
        Task<User> ApproveUserAsync(Guid userId, ClaimsPrincipal approver);
        Task RegisterAsync(User user);
    }

}
