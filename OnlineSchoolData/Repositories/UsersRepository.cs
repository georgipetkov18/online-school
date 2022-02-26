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
                .FirstOrDefaultAsync(u => u.Email == usernameOrEmail || u.Username == usernameOrEmail);

            if (user is null)
            {
                throw new ArgumentException($"User: {usernameOrEmail} does not exist");
            }

            var passwordIsValid = hashedPassword ? user.Password == password : BCrypt.Net.BCrypt.Verify(password, user.Password);

            if (!passwordIsValid)
            {
                throw new ArgumentException($"Invalid password was provided");
            }

            var key = this.configuration.GetSection("JwtSecret").Value;
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
            var refreshToken = GenerateRefreshToken();

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

        public async Task Register(User user)
        {
            var role = await this.context.Roles.FirstOrDefaultAsync(r => r.Name.ToLower() == user.RoleName.ToLower());

            if (role is null)
            {
                throw new ArgumentException($"Role with name {user.RoleName} does not exist!");
            }
            var userEntity = user.ToUserEntity(role);

            await this.context.Users.AddAsync(userEntity);

            switch (role.Name)
            {
                case Roles.Student:
                    await this.context.Students.AddAsync(user.ToStudentEntity(userEntity));
                    break;

                case Roles.Teacher:
                    await this.context.Teachers.AddAsync(user.ToTeacherEntity(userEntity));
                    break;
            }

            await this.context.SaveChangesAsync();
        }

        public async Task<AuthenticateModel> RefreshToken(string refreshToken)
        {
            var tokenEntity = await this.context.RefreshTokens
                .Include(t => t.User)
                .FirstOrDefaultAsync(t => t.Token == refreshToken);

            if (tokenEntity is null)
            {
                throw new ArgumentNullException(nameof(tokenEntity), "Invalid refresh token was provided");
            }

            if (DateTime.UtcNow >= tokenEntity.ExpiresOn)
            {
                this.context.RefreshTokens.Remove(tokenEntity);
                await this.context.SaveChangesAsync();

                throw new ArgumentException("Refresh token has expired");
            }

            var userEntity = tokenEntity.User;
            var newRefreshToken = GenerateRefreshToken();
            tokenEntity.Token = newRefreshToken.Token;
            tokenEntity.ExpiresOn = newRefreshToken.ExpiresOn;

            this.context.Update(tokenEntity);

            await this.context.SaveChangesAsync();

            return await Authenticate(userEntity.Email, userEntity.Password, true);
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
