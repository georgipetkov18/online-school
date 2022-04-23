namespace OnlineSchoolData.Seeders
{
    public static class DatabaseSeeder
    {
        public async static Task SeedDatabase(this ApplicationDbContext context)
        {
            await context.SeedRolesAsync();
            await context.SeedSubjectAsync();
            await context.SeedClassAsync();
            await context.SeedLessonAsync();
            await context.SeedUsersAsync();
            await context.SeedTimetableAsync();
        }
    }
}
