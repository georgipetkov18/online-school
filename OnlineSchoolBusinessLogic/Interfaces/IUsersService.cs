using OnlineSchoolBusinessLogic.Models;

namespace OnlineSchoolBusinessLogic.Interfaces
{
    public interface IUsersService
    {
        Task<AuthenticateModel> AuthenticateAsync(string usernameOrEmail, string password);
        Task RegisterAsync(User user);
        Task<AuthenticateModel> RefreshTokenAsync(string refreshToken);
    }

}
