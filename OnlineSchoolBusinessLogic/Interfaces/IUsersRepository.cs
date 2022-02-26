using OnlineSchoolBusinessLogic.Models;

namespace OnlineSchoolBusinessLogic.Interfaces
{
    public interface IUsersRepository
    {
        Task<AuthenticateModel> AuthenticateAsync(string usernameOrEmail, string password, bool hashedPassword = false);
        Task RegisterAsync(User user);
        Task<AuthenticateModel> RefreshTokenAsync(string refreshToken);
    }

}
