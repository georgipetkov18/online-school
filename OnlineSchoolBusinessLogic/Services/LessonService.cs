using OnlineSchoolBusinessLogic.Interfaces;
using OnlineSchoolBusinessLogic.Models;

namespace OnlineSchoolBusinessLogic.Services
{
    public class LessonService : ILessonService
    {
        private readonly ILessonRepository lessonRepository;

        public LessonService(ILessonRepository lessonRepository)
        {
            this.lessonRepository = lessonRepository;
        }

        public async Task<Lesson> AddLessonAsync(Lesson subject) => await this.lessonRepository.AddLessonAsync(subject);

        public async Task DeleteLessonAsync(Guid id) => await this.lessonRepository.DeleteLessonAsync(id);

        public async Task<Lesson> GetLessonAsync(Guid subjectId) => await this.lessonRepository.GetLessonAsync(subjectId);

        public async Task<Lesson> UpdateLessonAsync(Lesson subject) => await this.lessonRepository.UpdateLessonAsync(subject);
    }
}
