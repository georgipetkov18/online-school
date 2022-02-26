using OnlineSchoolBusinessLogic.Models;

namespace OnlineSchoolBusinessLogic.Interfaces
{
    public interface IUsersRepository
    {
        Task<User> GetUserAsync(string usernameOrEmail, string password, bool hashedPassword = false);
        Task<User> GetUserAsync(string usernameOrEmail);
        Task RegisterAsync(User user);
    }

}
