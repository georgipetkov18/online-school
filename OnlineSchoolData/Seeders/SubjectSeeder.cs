using Microsoft.EntityFrameworkCore;
using OnlineSchoolData.Entities;

namespace OnlineSchoolData.Seeders
{
    public static class SubjectSeeder
    {
        public static async Task SeedSubjectAsync(ApplicationDbContext context)
        {
            await context.Database.EnsureCreatedAsync();
            if (!await context.Subjects.AnyAsync())
            {
                await context.Subjects.AddAsync(new SubjectEntity
                {
                    Name = "Математика",
                    Code = "matematika"
                });

                await context.SaveChangesAsync();
            }
        }
    }
}
