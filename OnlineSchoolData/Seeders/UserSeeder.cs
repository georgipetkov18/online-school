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
                    Email= "student@abv.bg",
                    Password = "student",
                    RoleName = Roles.Student,
                    ClassId = _class.Id
                };

                var userEntity = user.ToUserEntity(studentRole);

                await context.Students.AddAsync(user.ToStudentEntity(userEntity));

                await context.SaveChangesAsync();
            }
        }

        public static async Task SeedTeacherAsync(ApplicationDbContext context)
        {
            await context.Database.EnsureCreatedAsync();
            if (!await context.Teachers.AnyAsync() && await context.Roles.AnyAsync())
            {
                var teacherRole = await context.Roles.FirstOrDefaultAsync(r => r.Name == Roles.Student);
                var subject = await context.Subjects.FirstOrDefaultAsync();

                if (teacherRole is null || subject is null)
                {
                    return;
                }

                var user = new User
                {
                    Username = "Teacher",
                    Email = "teacher@abv.bg",
                    Password = "teacher",
                    RoleName = Roles.Teacher,
                    SubjectId = subject.Id
                };

                var userEntity = user.ToUserEntity(teacherRole);

                await context.Teachers.AddAsync(user.ToTeacherEntity(userEntity));

                await context.SaveChangesAsync();
            }
        }

        public static async Task SeedUsersAsync(ApplicationDbContext context)
        {
            await SeedStudentAsync(context);
            await SeedTeacherAsync(context);
        }
    }
}