using OnlineSchoolBusinessLogic.Interfaces;
using OnlineSchoolBusinessLogic.Models;

namespace OnlineSchoolBusinessLogic.Services
{
    public class LessonsService : ILessonsService
    {
        private readonly ILessonsRepository lessonsRepository;

        public LessonsService(ILessonsRepository lessonsRepository)
        {
            this.lessonsRepository = lessonsRepository;
        }

        public async Task<Lesson> AddLessonAsync(Lesson lesson) => await this.lessonsRepository.AddLessonAsync(lesson);

        public async Task DeleteLessonAsync(Guid lessonId) => await this.lessonsRepository.DeleteLessonAsync(lessonId);

        public async Task<IEnumerable<Lesson>> GetAllLessonsAsync(string filter)
            => await this.lessonsRepository.GetAllLessonsAsync(filter);

        public async Task<Lesson> GetLessonAsync(Guid lessonId) => await this.lessonsRepository.GetLessonAsync(lessonId);

        public async Task<Lesson> UpdateLessonAsync(Guid lessonId, Lesson lesson) 
            => await this.lessonsRepository.UpdateLessonAsync(lessonId, lesson);
    }
}
