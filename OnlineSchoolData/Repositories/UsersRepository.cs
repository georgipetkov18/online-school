using Microsoft.EntityFrameworkCore;
using OnlineSchoolBusinessLogic.Common;
using OnlineSchoolBusinessLogic.Interfaces;
using OnlineSchoolBusinessLogic.Models;
using OnlineSchoolData.Mappers;

namespace OnlineSchoolData.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        private readonly ApplicationDbContext context;

        public UsersRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task RegisterAsync(User user)
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

        public async Task<User> GetUserAsync(string usernameOrEmail, string password, bool hashedPassword = false)
        {
            var user = await this.context.Users
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.Username == usernameOrEmail || u.Email == usernameOrEmail);

            if (user is null)
            {
                throw new ArgumentException($"User: {usernameOrEmail} does not exist");
            }

            var passwordIsValid = hashedPassword ? user.Password == password : BCrypt.Net.BCrypt.Verify(password, user.Password);

            if (!passwordIsValid)
            {
                throw new ArgumentException($"Invalid password was provided");
            }

            return user.ToUser();
        }

        public async Task<User> GetUserAsync(string usernameOrEmail)
        {
            var userEntity = await this.context.Users
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.Username == usernameOrEmail || u.Email == usernameOrEmail);

            if (userEntity is null)
            {
                throw new ArgumentException($"User: {usernameOrEmail} does not exist");
            }

            return userEntity.ToUser();
        }
    }
}
