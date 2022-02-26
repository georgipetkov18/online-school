using Microsoft.EntityFrameworkCore;
using OnlineSchoolData.Entities;

namespace OnlineSchoolData.Seeders
{
    public static class ClassSeeder
    {
        public static async Task SeedClassAsync(ApplicationDbContext context)
        {
            await context.Database.EnsureCreatedAsync();
            if (!await context.Classes.AnyAsync())
            {
                await context.Classes.AddAsync(new ClassEntity
                {
                    Name = "12e",
                });

                await context.SaveChangesAsync();
            }
        }
    }
}
