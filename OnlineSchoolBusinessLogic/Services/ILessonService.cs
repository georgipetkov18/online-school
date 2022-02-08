using OnlineSchoolBusinessLogic.Models;

namespace OnlineSchoolBusinessLogic.Services;

public interface ILessonService
{
    Task<Lesson> AddLessonAsync(Lesson subject);

    Task<Lesson> GetLessonAsync(Guid subjectId);

    Task<Lesson> UpdateLessonAsync(Lesson subject);

    Task DeleteLessonAsync(Guid id);
}

