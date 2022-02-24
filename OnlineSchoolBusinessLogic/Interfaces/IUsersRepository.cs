using OnlineSchoolBusinessLogic.Models;

namespace OnlineSchoolBusinessLogic.Interfaces
{
    public interface IUsersRepository
    {
        Task<AuthenticateModel> Authenticate(string usernameOrEmail, string password);

        Task<AuthenticateModel> Register(User user);
    }

}
