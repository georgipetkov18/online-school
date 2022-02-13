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

        public async Task<Lesson> AddLessonAsync(Lesson lesson) => await this.lessonRepository.AddLessonAsync(lesson);

        public async Task DeleteLessonAsync(Guid lessonId) => await this.lessonRepository.DeleteLessonAsync(lessonId);

        public async Task<Lesson> GetLessonAsync(Guid lessonId) => await this.lessonRepository.GetLessonAsync(lessonId);

        public async Task<Lesson> UpdateLessonAsync(Guid lessonId, Lesson lesson) 
            => await this.lessonRepository.UpdateLessonAsync(lessonId, lesson);
    }
}
