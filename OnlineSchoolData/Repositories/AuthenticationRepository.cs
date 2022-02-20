using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using OnlineSchoolBusinessLogic.Interfaces;
using OnlineSchoolBusinessLogic.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace OnlineSchoolData.Repositories
{
    public class AuthenticationRepository : IAuthenticationRepository
    {
        private readonly ApplicationDbContext context;
        private readonly IConfiguration configuration;

        public AuthenticationRepository(ApplicationDbContext context, IConfiguration configuration)
        {
            this.context = context;
            this.configuration = configuration;
        }

        public async Task<AuthenticateModel> Authenticate(string usernameOrEmail, string password)
        { 
            var user = await this.context.Users
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u =>
                    (u.Username == usernameOrEmail || u.Email == usernameOrEmail) &&
                    u.Password == password);

            if (user is null)
            {
                throw new ArgumentException("Invalid credential were provided!");
            }

            var key = configuration.GetSection("JwtSecret").Value;
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.ASCII.GetBytes(key);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Role, user.Role.Name),
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
            };

            var jwtToken = tokenHandler.CreateToken(tokenDescriptor);
            var jwtTokenString = tokenHandler.WriteToken(jwtToken);
            return new AuthenticateModel(user.Username, user.Email, user.Role.Name, jwtTokenString, "refresh");
        }
    }
}
