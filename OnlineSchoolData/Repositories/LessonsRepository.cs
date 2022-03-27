using Microsoft.EntityFrameworkCore;
using OnlineSchoolBusinessLogic.Interfaces;
using OnlineSchoolBusinessLogic.Models;
using OnlineSchoolData.CustomExceptions;
using OnlineSchoolData.Entities;
using OnlineSchoolData.Mappers;

namespace OnlineSchoolData.Repositories
{
    public class LessonsRepository : ILessonsRepository
    {
        private readonly ApplicationDbContext context;

        public LessonsRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<Lesson> AddLessonAsync(Lesson lesson)
        {
            var lessonEntity = lesson.ToLessonEntity();
            await this.context.Lessons.AddAsync(lessonEntity);
            await this.context.SaveChangesAsync();

            return lessonEntity.ToLesson();
        }

        public async Task DeleteLessonAsync(Guid lessonId)
        {
            var lessonEntity = await this.GetLessonByIdAsync(lessonId);

            if (lessonEntity is null)
            {
                throw new InvalidIdException("Lesson with the given id does not exist!");
            }

            this.context.Lessons.Remove(lessonEntity);
            await this.context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Lesson>> GetAllLessonsAsync(string filter)
        {
            var lessonEntities = await this.context.Lessons
                .Where(s => s.From.ToString().Contains(filter))
                .AsNoTracking()
                .ToListAsync();

            return lessonEntities.Select(l => l.ToLesson());
        }

        public async Task<Lesson> GetLessonAsync(Guid lessonId)
        {
            var lessonEntity = await this.GetLessonByIdAsync(lessonId, false);

            if (lessonEntity is null)
            {
                throw new InvalidIdException("Lesson with the given id does not exist!");
            }

            return lessonEntity.ToLesson();
        }

        public async Task<Lesson> UpdateLessonAsync(Guid lessonId, Lesson lesson)
        {
            var lessonEntity = await this.GetLessonByIdAsync(lessonId);

            if (lessonEntity is null)
            {
                throw new InvalidIdException("Lesson with the given id does not exist!");
            }

            lessonEntity.From = lesson.From;
            lessonEntity.DurationInMinutes = lesson.DurationInMinutes;

            this.context.Lessons.Update(lessonEntity);
            await this.context.SaveChangesAsync();

            return lessonEntity.ToLesson();
        }

        private async Task<LessonEntity?> GetLessonByIdAsync(Guid lessonId, bool tracking = true)
        {
            var query = this.context.Lessons.AsQueryable();
            if (!tracking)
            {
                query = query.AsNoTracking();
            }
            return await query.FirstOrDefaultAsync(l => l.Id == lessonId);
        }
    }
}
