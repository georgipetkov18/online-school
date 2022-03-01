using Microsoft.EntityFrameworkCore;
using OnlineSchoolBusinessLogic.Common;
using OnlineSchoolBusinessLogic.Models;
using OnlineSchoolData.Mappers;

namespace OnlineSchoolData.Seeders
{
    public static class UserSeeder
    {
        public static async Task SeedStudentAsync(ApplicationDbContext context)
        {
            await context.Database.EnsureCreatedAsync();
            if (!await context.Students.AnyAsync() && await context.Roles.AnyAsync())
            {
                var studentRole = await context.Roles.FirstOrDefaultAsync(r => r.Name == Roles.Student);
                var _class = await context.Classes.FirstOrDefaultAsync();

                if (studentRole is null || _class is null)
                {
                    return;
                }

                var user = new User
                {
                    Username = "Student",
                    FirstName = "Student",
                    LastName = "1",
                    Email= "student@abv.bg",
                    Password = "student",
                    RoleName = Roles.Student,
                    ClassId = _class.Id
                };

                var userEntity = user.ToUserEntity(studentRole);
                userEntity.Status = AccountStatus.Approved;

                await context.Students.AddAsync(user.ToStudentEntity(userEntity));

                await context.SaveChangesAsync();
            }
        }

        public static async Task SeedTeacherAsync(ApplicationDbContext context)
        {
            await context.Database.EnsureCreatedAsync();
            if (!await context.Teachers.AnyAsync() && await context.Roles.AnyAsync())
            {
                var teacherRole = await context.Roles.FirstOrDefaultAsync(r => r.Name == Roles.Teacher);
                var subject = await context.Subjects.FirstOrDefaultAsync();

                if (teacherRole is null || subject is null)
                {
                    return;
                }

                var user = new User
                {
                    Username = "Teacher",
                    FirstName = "Teacher",
                    LastName = "1",
                    Email = "teacher@abv.bg",
                    Password = "teacher",
                    RoleName = Roles.Teacher,
                    SubjectId = subject.Id
                };

                var userEntity = user.ToUserEntity(teacherRole);
                userEntity.Status = AccountStatus.Approved;

                await context.Teachers.AddAsync(user.ToTeacherEntity(userEntity));

                await context.SaveChangesAsync();
            }
        }

        public static async Task SeedAdminAsync(ApplicationDbContext context)
        {
            await context.Database.EnsureCreatedAsync();
            if (!await context.Administrators.AnyAsync() && await context.Roles.AnyAsync())
            {
                var adminRole = await context.Roles.FirstOrDefaultAsync(r => r.Name == Roles.Administrator);

                if (adminRole is null)
                {
                    return;
                }

                var user = new User
                {
                    Username = "Admin",
                    FirstName = "Admin",
                    LastName = "1",
                    Email = "admin@abv.bg",
                    Password = "admin",
                    RoleName = Roles.Teacher,
                };

                var userEntity = user.ToUserEntity(adminRole);
                userEntity.Status = AccountStatus.Approved;

                await context.Administrators.AddAsync(user.ToAdministartorEntity(userEntity));

                await context.SaveChangesAsync();
            }
        }

        public static async Task SeedUsersAsync(ApplicationDbContext context)
        {
            await SeedStudentAsync(context);
            await SeedTeacherAsync(context);
            await SeedAdminAsync(context);
        }
    }
}