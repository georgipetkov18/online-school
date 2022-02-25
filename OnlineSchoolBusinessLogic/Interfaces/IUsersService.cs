using OnlineSchoolBusinessLogic.Models;

namespace OnlineSchoolBusinessLogic.Interfaces
{
    public interface IUsersService
    {
        Task<AuthenticateModel> Authenticate(string usernameOrEmail, string password);
        Task Register(User user);
        Task<AuthenticateModel> RefreshToken(string refreshToken);
    }

}
