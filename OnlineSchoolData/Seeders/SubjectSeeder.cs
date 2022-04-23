using Microsoft.EntityFrameworkCore;
using OnlineSchoolData.Entities;

namespace OnlineSchoolData.Seeders
{
    public static class SubjectSeeder
    {
        public static async Task SeedSubjectAsync(this ApplicationDbContext context)
        {
            await context.Database.EnsureCreatedAsync();
            if (!await context.Subjects.AnyAsync())
            {
                await context.Subjects.AddRangeAsync(
                    new SubjectEntity
                    {
                        Name = "Математика",
                        Code = "matematika"
                    },
                    new SubjectEntity
                    {
                        Name = "Компютърна графика и дизайн",
                        Code = "kgdcode1"
                    },
                    new SubjectEntity
                    {
                        Name = "Приложения с графичен потребителски интерфейс",
                        Code = "pgpicode1"
                    },
                    new SubjectEntity
                    {
                        Name = "БЕЛ",
                        Code = "bel12e"
                    },
                    new SubjectEntity
                    {
                        Name = "ФВС",
                        Code = "fvs12"
                    },
                    new SubjectEntity
                    {
                        Name = "Час на класа",
                        Code = "chkcode1"
                    },
                    new SubjectEntity
                    {
                        Name = "Интернет програмиране",
                        Code = "netprogramming"
                    },
                    new SubjectEntity
                    {
                        Name = "Немски език",
                        Code = "nemski12e"
                    },
                    new SubjectEntity
                    {
                        Name = "Чужд език по професията",
                        Code = "chep12e"
                    },
                    new SubjectEntity
                    {
                        Name = "Софтуерно инженерство",
                        Code = "software engineering"
                    },
                    new SubjectEntity
                    {
                        Name = "Гражданско образование",
                        Code = "grazhdansko12e"
                    },
                    new SubjectEntity
                    {
                        Name = "Мрежови протоколи и технологии",
                        Code = "protocols12e"
                    },
                    new SubjectEntity
                    {
                        Name = "Програмиране за вградени системи",
                        Code = "technologies12e"
                    }
                );

                await context.SaveChangesAsync();
            }
        }
    }
}
