using OnlineSchoolBusinessLogic.Models;

namespace OnlineSchoolBusinessLogic.Interfaces
{
    public interface IUsersRepository
    {
        Task<AuthenticateModel> Authenticate(string usernameOrEmail, string password, bool hashedPassword = false);
        Task Register(User user);
        Task<AuthenticateModel> RefreshToken(string refreshToken);
    }

}
