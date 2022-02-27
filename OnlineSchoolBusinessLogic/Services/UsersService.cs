using BusinessLayer.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using OnlineSchoolBusinessLogic.Interfaces;
using OnlineSchoolBusinessLogic.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace OnlineSchoolBusinessLogic.Services
{
    public class UsersService : IUsersService
    {
        private readonly IUsersRepository usersRepository;
        private readonly IConfiguration configuration;

        public UsersService(IUsersRepository usersRepository, IConfiguration configuration)
        {
            this.usersRepository = usersRepository;
            this.configuration = configuration;
        }

        public async Task<AuthenticateModel> AuthenticateAsync(string usernameOrEmail, string password, bool hashedPassword = false)
        {
            var user = await usersRepository.GetUserAsync(usernameOrEmail, password, hashedPassword);

            var claimList = new Claim[]
            {
                new Claim("Id", user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.RoleName),
            };

            var key = configuration.GetSection("JwtSecret").Value;
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.ASCII.GetBytes(key);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claimList),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
            };
            var jwtToken = tokenHandler.CreateToken(tokenDescriptor);
            var jwtTokenString = tokenHandler.WriteToken(jwtToken);

            var refreshToken = new RefreshToken
            {
                Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
                ExpiresOn = DateTime.UtcNow.AddDays(7),
            };

            return new AuthenticateModel(user.Username, user.Email, user.RoleName, jwtTokenString, refreshToken.Token);
        }

        public async Task<AuthenticateModel> RefreshTokenAsync(ClaimsPrincipal user, string refreshToken)
        {
            var userEmail = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;

            var userModel = await usersRepository.GetUserAsync(userEmail!);

            return await AuthenticateAsync(userModel.Email, userModel.Password, true);
        }

        public async Task RegisterAsync(User user) =>
            await this.usersRepository.RegisterAsync(user);
    }
}
