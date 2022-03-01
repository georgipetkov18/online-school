using Microsoft.EntityFrameworkCore;
using OnlineSchoolBusinessLogic.Common;
using OnlineSchoolBusinessLogic.Interfaces;
using OnlineSchoolBusinessLogic.Models;
using OnlineSchoolData.Mappers;
using System.Security.Claims;

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
            if (await this.context.Users.AnyAsync(u => u.Username == user.Username))
            {
                throw new ArgumentException($"Username: {user.Username} already exists!");
            }

            if (await this.context.Users.AnyAsync(u => u.Email == user.Email))
            {
                throw new ArgumentException($"Email: {user.Email} is already taken!");
            }

            var role = await this.context.Roles.FirstOrDefaultAsync(r => r.Name.ToLower() == user.RoleName.ToLower());

            if (role is null)
            {
                throw new ArgumentException($"Role with name {user.RoleName} does not exist!");
            }
            var userEntity = user.ToUserEntity(role);

            await this.context.Users.AddAsync(userEntity);

            switch (role.Name)
            {
                case Roles.Administrator:
                    await context.Administrators.AddAsync(user.ToAdministartorEntity(userEntity));
                    break;

                case Roles.Student:
                    if (user.ClassId is null || !await this.context.Classes.AnyAsync(c => c.Id == user.ClassId))
                    {
                        throw new ArgumentException($"Provide a valid classId");
                    }
                    await this.context.Students.AddAsync(user.ToStudentEntity(userEntity));
                    break;

                case Roles.Teacher:
                    if (user.SubjectId is null || !await this.context.Subjects.AnyAsync(s => s.Id == user.SubjectId))
                    {
                        throw new ArgumentException($"Provide a valid subjectId");
                    }
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

            if (user.Status == AccountStatus.Pending)
            {
                throw new ArgumentException($"User: {usernameOrEmail} is not approved");
            }

            var passwordIsValid = hashedPassword ? user.Password == password : BCrypt.Net.BCrypt.Verify(password, user.Password);

            if (!passwordIsValid)
            {
                throw new ArgumentException($"Invalid password was provided");
            }

            return user.ToUser();
        }

        public async Task<User> ApproveUserAsync(Guid userId, ClaimsPrincipal approver)
        {
            var userEntity = await context.Users
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (userEntity is null)
            {
                throw new ArgumentException($"User with id: {userId} does not exist");
            }

            if (approver.IsInRole(Roles.Teacher) && userEntity.Role.Name != Roles.Student)
            {
                throw new InvalidOperationException();
            }

            userEntity.Status = AccountStatus.Approved;
            context.Update(userEntity);
            await context.SaveChangesAsync();

            return userEntity.ToUser();
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
