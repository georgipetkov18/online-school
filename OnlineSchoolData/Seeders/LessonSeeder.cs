using Microsoft.EntityFrameworkCore;
using OnlineSchoolData.Entities;

namespace OnlineSchoolData.Seeders
{
    public static class LessonSeeder
    {
        public static async Task SeedLessonAsync(this ApplicationDbContext context)
        {
            await context.Database.EnsureCreatedAsync();
            if (!await context.Lessons.AnyAsync())
            {
                await context.Lessons.AddRangeAsync(
                    new LessonEntity
                    {
                        From = new TimeSpan(7, 30, 0),
                        DurationInMinutes = 40,
                    },
                    new LessonEntity
                    {
                        From = new TimeSpan(8, 20, 0),
                        DurationInMinutes = 40,
                    },
                    new LessonEntity
                    {
                        From = new TimeSpan(9, 10, 0),
                        DurationInMinutes = 40,
                    },
                    new LessonEntity
                    {
                        From = new TimeSpan(10, 10, 0),
                        DurationInMinutes = 40,
                    },
                    new LessonEntity
                    {
                        From = new TimeSpan(11, 0, 0),
                        DurationInMinutes = 40,
                    },
                    new LessonEntity
                    {
                        From = new TimeSpan(11, 45, 0),
                        DurationInMinutes = 40,
                    },
                    new LessonEntity
                    {
                        From = new TimeSpan(12, 30, 0),
                        DurationInMinutes = 40,
                    },
                    new LessonEntity
                    {
                        From = new TimeSpan(13, 20, 0),
                        DurationInMinutes = 40,
                    }
                );

                await context.SaveChangesAsync();
            }
        }
    }
}
