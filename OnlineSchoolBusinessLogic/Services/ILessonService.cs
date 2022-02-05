using OnlineSchoolBusinessLogic.Models;

namespace OnlineSchoolBusinessLogic.Services
{
    public interface ILessonService
    {
        Task<Lesson> AddLessonAsync(Lesson lesson);

        Task<Lesson> GetLessonAsync(Guid lessonId);

        Task<IEnumerable<Lesson>> GetAllLessonsAsync();

        Task<Lesson> UpdateLessonAsync(Lesson lesson);

        Task DeleteLessonAsync(Lesson lesson);
    }
}
