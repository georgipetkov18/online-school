using OnlineSchoolBusinessLogic.Models;

namespace OnlineSchoolBusinessLogic.Interfaces;

public interface ILessonsRepository
{
    Task<Lesson> AddLessonAsync(Lesson lesson);

    Task<Lesson> GetLessonAsync(Guid lessonId);

    Task<Lesson> UpdateLessonAsync(Guid lessonId, Lesson lesson);

    Task DeleteLessonAsync(Guid lessonId);
}