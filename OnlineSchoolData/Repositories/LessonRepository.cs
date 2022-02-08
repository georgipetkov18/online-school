using Microsoft.EntityFrameworkCore;
using OnlineSchoolBusinessLogic.Interfaces;
using OnlineSchoolBusinessLogic.Models;
using OnlineSchoolData.CustomExceptions;
using OnlineSchoolData.Entities;
using OnlineSchoolData.Mappers;

namespace OnlineSchoolData.Repositories
{
    public class LessonRepository : ILessonRepository
    {
        private readonly ApplicationDbContext context;

        public LessonRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<Lesson> AddLessonAsync(Lesson lesson)
        {
            if (lesson is null)
            {
                throw new EmptyDataException(nameof(lesson));
            }

            var lessonEntity = lesson.ToLessonEntity();
            await this.context.Lessons.AddAsync(lessonEntity);
            await this.context.SaveChangesAsync();

            return lessonEntity.ToLesson();
        }

        public async Task DeleteLessonAsync(Guid lessonId)
        {
            if (lessonId == Guid.Empty)
            {
                throw new EmptyDataException(nameof(lessonId));
            }

            var lessonEntity = await this.GetLessonByIdAsync(lessonId);

            if (lessonEntity is null)
            {
                throw new InvalidIdException("Lesson with the given id does not exist!", lessonId);
            }

            this.context.Lessons.Remove(lessonEntity);
            await this.context.SaveChangesAsync();
        }

        public async Task<Lesson> GetLessonAsync(Guid lessonId)
        {
            if (lessonId == Guid.Empty)
            {
                throw new EmptyDataException(nameof(lessonId));
            }

            var lessonEntity = await this.GetLessonByIdAsync(lessonId, false);

            if (lessonEntity is null)
            {
                throw new InvalidIdException("Lesson with the given id does not exist!", lessonId);
            }

            return lessonEntity.ToLesson();
        }

        public async Task<Lesson> UpdateLessonAsync(Lesson lesson)
        {
            if (lesson is null)
            {
                throw new EmptyDataException(nameof(lesson));
            }

            if (lesson.From == TimeSpan.Zero)
            {
                throw new InvalidDataProvidedException(nameof(lesson.From), TimeSpan.Zero.ToString());
            }

            if (lesson.DurationInMinutes <= 0)
            {
                throw new InvalidDataProvidedException(nameof(lesson.DurationInMinutes));
            }

            var lessonEntity = await this.GetLessonByIdAsync(lesson.Id);

            if (lessonEntity is null)
            {
                throw new InvalidIdException("Lesson with the given id does not exist!", lesson.Id);
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
