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

        public async Task DeleteLessonAsync(Guid id) => await this.lessonRepository.DeleteLessonAsync(id);

        public async Task<IEnumerable<Lesson>> GetAllLessonsAsync() => await this.lessonRepository.GetAllLessonsAsync();

        public async Task<Lesson> GetLessonAsync(Guid lessonId) => await this.lessonRepository.GetLessonAsync(lessonId);

        public async Task<Lesson> UpdateLessonAsync(Lesson lesson) => await this.lessonRepository.UpdateLessonAsync(lesson);
    }
}
