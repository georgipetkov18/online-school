using Microsoft.EntityFrameworkCore;
using OnlineSchoolBusinessLogic.Interfaces;
using OnlineSchoolBusinessLogic.Models;
using OnlineSchoolData.Mappers;

namespace OnlineSchoolData.Repositories
{
    public class LessonRepository : ILessonRepository
    {
        private readonly ApplicationDbContext context;

        public LessonRepository(ApplicationDbContext dbContext)
        {
            this.context = dbContext;
        }

        public async Task<Lesson> AddLessonAsync(Lesson lesson)
        {
            if (lesson is null)
            {
                throw new ArgumentNullException(nameof(lesson));
            }

            if (string.IsNullOrWhiteSpace(lesson.Name))
            {
                throw new ArgumentNullException(nameof(lesson.Name));
            }

            if (string.IsNullOrWhiteSpace(lesson.Code))
            {
                throw new ArgumentNullException(nameof(lesson.Code));
            }

            var lessonEntity = lesson.ToLessonEntity();
            await this.context.Lessons.AddAsync(lessonEntity);
            await this.context.SaveChangesAsync();

            return lessonEntity.ToLesson();
        }

        public Task DeleteLessonAsync(Lesson lesson)
        {
            if (lesson is null)
            {
                throw new ArgumentNullException(nameof(lesson));
            }

            this.context.Lessons.Remove(lesson.ToLessonEntity());
            return this.context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Lesson>> GetAllLessonsAsync()
        {
            var lessonEntities = await this.context.Lessons.ToListAsync();

            return lessonEntities.ToLessons();
        }

        public async Task<Lesson> GetLessonAsync(Guid lessonId)
        {
            if (lessonId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(lessonId));
            }

            var lessonEntity = await this.context.Lessons.FirstOrDefaultAsync(l => l.Id == lessonId);

            if (lessonEntity is null)
            {
                throw new ArgumentNullException(nameof(lessonEntity));
            }

            return lessonEntity.ToLesson();
        }

        public async Task<Lesson> UpdateLessonAsync(Lesson lesson)
        {
            if (lesson is null)
            {
                throw new ArgumentNullException(nameof(lesson));
            }

            if (string.IsNullOrWhiteSpace(lesson.Name))
            {
                throw new ArgumentNullException(nameof(lesson.Name));
            }

            if (string.IsNullOrWhiteSpace(lesson.Code))
            {
                throw new ArgumentNullException(nameof(lesson.Code));
            }

            var lessonEntity = await this.context.Lessons.FirstOrDefaultAsync(l => l.Id == lesson.Id);

            if (lessonEntity is null)
            {
                throw new ArgumentNullException(nameof(lessonEntity));
            }

            lessonEntity.Name = lesson.Name;
            lessonEntity.Code = lesson.Code;

            this.context.Lessons.Update(lessonEntity);
            await this.context.SaveChangesAsync();

            return lessonEntity.ToLesson();
        }
    }
}
