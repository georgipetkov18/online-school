using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using OnlineSchoolBusinessLogic.Common;
using OnlineSchoolBusinessLogic.Interfaces;
using OnlineSchoolBusinessLogic.Models;
using OnlineSchoolData.Entities;
using OnlineSchoolData.Mappers;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace OnlineSchoolData.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        private readonly ApplicationDbContext context;
        private readonly IConfiguration configuration;

        public UsersRepository(ApplicationDbContext context, IConfiguration configuration)
        {
            this.context = context;
            this.configuration = configuration;
        }

        public async Task<AuthenticateModel> Authenticate(string usernameOrEmail, string password, bool hashedPassword = false)
        { 
            var user = await this.context.Users
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u =>
                    (u.Username == usernameOrEmail || u.Email == usernameOrEmail) &&
                    u.Password == password);

            if (user is null)
            {
                throw new ArgumentException($"User: {usernameOrEmail} does not exist");
            }

            var passwordIsValid = hashedPassword ? user.Password == password : BCrypt.Net.BCrypt.Verify(password, user.Password);

            if (!passwordIsValid)
            {
                throw new ArgumentException("Invalid password");
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

        public async Task<AuthenticateModel> Register(User user)
        {
            var role = await this.context.Roles.FirstOrDefaultAsync(r => r.Name.ToLower() == user.RoleName.ToLower());

            if (role is null)
            {
                // Throw exception
                throw new Exception();
            }

            await this.context.Users.AddAsync(user.ToUserEntity(role));

            switch (role.Name)
            {
                case Roles.Student:
                    // Check if classId is not null
                    await this.context.Students.AddAsync(user.ToStudentEntity(user.ToUserEntity(role)));
                    break;

                case Roles.Teacher:
                    // Check if subjectId is not null
                    await this.context.Teachers.AddAsync(user.ToTeacherEntity(user.ToUserEntity(role)));
                    break;
            }

            await this.context.SaveChangesAsync();
            return await this.Authenticate(user.Email, user.Password);

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
