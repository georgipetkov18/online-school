using OnlineSchoolBusinessLogic.Interfaces;

namespace OnlineSchoolData.Repositories
{
    public class LessonRepository : ILessonRepository
    {
        private readonly ApplicationDbContext dbContext;

        public LessonRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
    }
}
