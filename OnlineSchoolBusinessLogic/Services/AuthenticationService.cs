using OnlineSchoolBusinessLogic.Interfaces;
using OnlineSchoolBusinessLogic.Models;

namespace OnlineSchoolBusinessLogic.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IAuthenticationRepository authenticationRepository;

        public AuthenticationService(IAuthenticationRepository authenticationRepository)
        {
            this.authenticationRepository = authenticationRepository;
        }

        public async Task<AuthenticateModel> Authenticate(string usernameOrEmail, string password) =>
            await authenticationRepository.Authenticate(usernameOrEmail, password);

        public async Task<AuthenticateModel> Register(User user) => await this.authenticationRepository.Register(user);
    }
}
