using OnlineSchoolBusinessLogic.Models;

namespace OnlineSchoolBusinessLogic.Interfaces;

public interface ILessonRepository
{
    Task<Lesson> AddLessonAsync(Lesson lesson);

    Task<Lesson> GetLessonAsync(Guid lessonId);

    Task<Lesson> UpdateLessonAsync(Lesson lesson);

    Task DeleteLessonAsync(Guid lessonId);
}