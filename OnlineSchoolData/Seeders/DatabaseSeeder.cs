namespace OnlineSchoolData.Seeders
{
    public static class DatabaseSeeder
    {
        public async static Task SeedDatabase(ApplicationDbContext context)
        {
            await RoleSeeder.SeedRolesAsync(context);
            await SubjectSeeder.SeedSubjectAsync(context);
            await ClassSeeder.SeedClassAsync(context);
            await LessonSeeder.SeedLessonAsync(context);
            await UserSeeder.SeedUsersAsync(context);
        }
    }
}
