using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using OnlineSchoolBusinessLogic.Interfaces;
using OnlineSchoolBusinessLogic.Models;
using OnlineSchoolData.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
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
                throw new ArgumentException("Invalid credentials were provided!");
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
            var refreshToken = this.GenerateRefreshToken();       

            var currentUserToken = await this.context.RefreshTokens.FirstOrDefaultAsync(t => t.UserId == user.Id);

            if (currentUserToken is null)
            {
                refreshToken.User = user;
                await this.context.AddAsync(refreshToken);
            }
            else
            {
                currentUserToken.Token = refreshToken.Token;
                currentUserToken.ExpiresOn = refreshToken.ExpiresOn;
                currentUserToken.User = user;
                this.context.Update(currentUserToken);
            }

            await this.context.SaveChangesAsync();

            return new AuthenticateModel(user.Username, user.Email, user.Role.Name, jwtTokenString, refreshToken.Token);
        }

        private RefreshTokenEntity GenerateRefreshToken()
        {
            var refreshToken = new RefreshTokenEntity
            {
                Token = this.GetUniqueToken(),
                ExpiresOn = DateTime.UtcNow.AddDays(7),
            };

            return refreshToken;
        }

        private string GetUniqueToken()
        {
            var token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
            var tokenIsUnique = !context.Users.Any(u => u.RefreshToken.Token == token);

            if (!tokenIsUnique)
                return this.GetUniqueToken();

            return token;
        }
    }
}
