using Microsoft.EntityFrameworkCore;
using OnlineSchoolData.Entities;

namespace OnlineSchoolData.Seeders
{
    public static class LessonSeeder
    {
        public static async Task SeedLessonAsync(ApplicationDbContext context)
        {
            await context.Database.EnsureCreatedAsync();
            if (!await context.Lessons.AnyAsync())
            {
                await context.Lessons.AddAsync(new LessonEntity
                {
                    From = new TimeSpan(10, 0, 0),
                    DurationInMinutes = 40,
                }); ;

                await context.SaveChangesAsync();
            }
        }
    }
}
