using OnlineSchoolBusinessLogic.Models;

namespace OnlineSchoolBusinessLogic.Interfaces
{
    public interface IAuthenticationRepository
    {
        Task<AuthenticateModel> Authenticate(string usernameOrEmail, string password);

        Task<AuthenticateModel> Register(User user);
    }

}
