using Microsoft.EntityFrameworkCore;
using OnlineSchoolBusinessLogic.Common;
using OnlineSchoolBusinessLogic.Models;
using OnlineSchoolData.Mappers;

namespace OnlineSchoolData.Seeders
{
    public static class UserSeeder
    {
        public static async Task SeedStudentAsync(this ApplicationDbContext context)
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
                    Email = "student@abv.bg",
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

        public static async Task SeedTeacherAsync(this ApplicationDbContext context)
        {
            await context.Database.EnsureCreatedAsync();
            if (!await context.Teachers.AnyAsync() && await context.Roles.AnyAsync())
            {
                var teacherRole = await context.Roles.FirstOrDefaultAsync(r => r.Name == Roles.Teacher);
                var subjects = await context.Subjects.ToListAsync();

                if (teacherRole is null)
                {
                    return;
                }

                var users = new List<User>
                {
                        new User
                        {
                            Username = "Rosen_Valchev",
                            FirstName = "Росен",
                            LastName = "Вълчев",
                            Email = "rosenvalchev@schoolmath.eu",
                            Password = "Тeacher1!",
                            RoleName = Roles.Teacher,
                            SubjectId = subjects.FirstOrDefault(s => s.Name == "Приложения с графичен потребителски интерфейс")?.Id
                        },
                        new User
                        {
                            Username = "Pepa_Ilkova",
                            FirstName = "Пепа",
                            LastName = "Илкова",
                            Email = "pepailkova@schoolmath.eu",
                            Password = "Тeacher1!",
                            RoleName = Roles.Teacher,
                            SubjectId = subjects.FirstOrDefault(s => s.Name == "Компютърна графика и дизайн")?.Id
                        },
                        new User
                        {
                            Username = "Dafinka_Andreeva",
                            FirstName = "Дафинка",
                            LastName = "Андреева",
                            Email = "dafinkaandreeva@schoolmath.eu",
                            Password = "Тeacher1!",
                            RoleName = Roles.Teacher,
                            SubjectId = subjects.FirstOrDefault(s => s.Name == "Математика")?.Id
                        },
                        new User
                        {
                            Username = "Nina_Tosheva",
                            FirstName = "Нина",
                            LastName = "Тошева",
                            Email = "ninatosheva@schoolmath.eu",
                            Password = "Тeacher1!",
                            RoleName = Roles.Teacher,
                            SubjectId = subjects.FirstOrDefault(s => s.Name == "БЕЛ")?.Id
                        },
                        new User
                        {
                            Username = "Nedko_Kableshkov",
                            FirstName = "Недко",
                            LastName = "Каблешков",
                            Email = "nedkokableshkov@schoolmath.eu",
                            Password = "Тeacher1!",
                            RoleName = Roles.Teacher,
                            SubjectId = subjects.FirstOrDefault(s => s.Name == "ФВС")?.Id
                        },
                        new User
                        {
                            Username = "Lilia_Nedeleva",
                            FirstName = "Лилия",
                            LastName = "Неделева",
                            Email = "lilianedeleva@schoolmath.eu",
                            Password = "Тeacher1!",
                            RoleName = Roles.Teacher,
                            SubjectId = subjects.FirstOrDefault(s => s.Name == "Чужд език по професията")?.Id
                        },
                        new User
                        {
                            Username = "Ivan_Peev",
                            FirstName = "Иван",
                            LastName = "Пеев",
                            Email = "ivanpeev@schoolmath.eu",
                            Password = "Тeacher1!",
                            RoleName = Roles.Teacher,
                            SubjectId = subjects.FirstOrDefault(s => s.Name == "Интернет програмиране")?.Id
                        },
                        new User
                        {
                            Username = "Miroslava_Iovkova",
                            FirstName = "Мирослава",
                            LastName = "Йовкова",
                            Email = "miroslavaiovkova@schoolmath.eu",
                            Password = "Тeacher1!",
                            RoleName = Roles.Teacher,
                            SubjectId = subjects.FirstOrDefault(s => s.Name == "Немски език")?.Id
                        },
                        new User
                        {
                            Username = "Hrisi_Plachkova",
                            FirstName = "Хриси",
                            LastName = "Плачкова",
                            Email = "hrisiplachkova@schoolmath.eu",
                            Password = "Тeacher1!",
                            RoleName = Roles.Teacher,
                            SubjectId = subjects.FirstOrDefault(s => s.Name == "Софтуерно инженерство")?.Id
                        },
                        new User
                        {
                            Username = "Desislava_Vachkova_Mateeva",
                            FirstName = "Десислава",
                            LastName = "Вачкова-Матеева",
                            Email = "desislavavachkovamateeva@schoolmath.eu",
                            Password = "Тeacher1!",
                            RoleName = Roles.Teacher,
                            SubjectId = subjects.FirstOrDefault(s => s.Name == "Гражданско образование")?.Id
                        },
                        new User
                        {
                            Username = "Yanislav_Kartelov",
                            FirstName = "Янислав",
                            LastName = "Картелов",
                            Email = "yanislavkartelov@schoolmath.eu",
                            Password = "Тeacher1!",
                            RoleName = Roles.Teacher,
                            SubjectId = subjects.FirstOrDefault(s => s.Name == "Мрежови протоколи и технологии")?.Id
                        },
                        new User
                        {
                            Username = "Zhivko_Radev",
                            FirstName = "Живко",
                            LastName = "Радев",
                            Email = "zhivkoradev@schoolmath.eu",
                            Password = "Тeacher1!",
                            RoleName = Roles.Teacher,
                            SubjectId = subjects.FirstOrDefault(s => s.Name == "Програмиране за вградени системи")?.Id
                        },
                        new User
                        {
                            Username = "Denitsa_Grozeva",
                            FirstName = "Деница",
                            LastName = "Грозева",
                            Email = "denitsagrozeva@schoolmath.eu",
                            Password = "Тeacher1!",
                            RoleName = Roles.Teacher,
                            SubjectId = subjects.FirstOrDefault(s => s.Name == "Мрежови протоколи и технологии")?.Id
                        }
            };

                var userEntities = users.Select(u => 
                { 
                    var userEntity = u.ToUserEntity(teacherRole); 
                    userEntity.Status = AccountStatus.Approved;
                    return userEntity;
                }).ToList();

                await context.Teachers.AddRangeAsync(users.Select((u, i) => u.ToTeacherEntity(userEntities[i])));
                await context.SaveChangesAsync();
            }
        }

        public static async Task SeedAdminAsync(this ApplicationDbContext context)
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

        public static async Task SeedUsersAsync(this ApplicationDbContext context)
        {
            await context.SeedStudentAsync();
            await context.SeedTeacherAsync();
            await context.SeedAdminAsync();
        }
    }
}