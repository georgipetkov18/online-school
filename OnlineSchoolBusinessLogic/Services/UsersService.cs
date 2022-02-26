using OnlineSchoolBusinessLogic.Interfaces;
using OnlineSchoolBusinessLogic.Models;

namespace OnlineSchoolBusinessLogic.Services
{
    public class UsersService : IUsersService
    {
        private readonly IUsersRepository usersRepository;

        public UsersService(IUsersRepository usersRepository)
        {
            this.usersRepository = usersRepository;
        }

        public async Task<AuthenticateModel> AuthenticateAsync(string usernameOrEmail, string password) =>
            await usersRepository.AuthenticateAsync(usernameOrEmail, password);

        public async Task<AuthenticateModel> RefreshTokenAsync(string refreshToken) => 
            await this.usersRepository.RefreshTokenAsync(refreshToken);

        public async Task RegisterAsync(User user) => 
            await this.usersRepository.RegisterAsync(user);
    }
}
