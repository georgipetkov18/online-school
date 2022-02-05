using OnlineSchoolBusinessLogic.Models;
using OnlineSchoolData.Entities;

namespace OnlineSchoolData.Mappers
{
    public static class LessonMapper
    {
        public static Lesson ToLesson(this LessonEntity lessonEntity)
        {
            return new Lesson
            {
                Id = lessonEntity.Id,
                Name = lessonEntity.Name,
                Code = lessonEntity.Code,
            };
        }

        public static IEnumerable<Lesson> ToLessons(this IEnumerable<LessonEntity> lessonEntities)
        {
            IEnumerable<Lesson> lessons = new List<Lesson>();

            foreach (var lessonEntity in lessonEntities)
            {
                lessons.Append(lessonEntity.ToLesson());
            }

            return lessons;
        }

        public static LessonEntity ToLessonEntity(this Lesson lesson)
        {
            return new LessonEntity
            {
                Id = lesson.Id,
                Name = lesson.Name,
                Code = lesson.Code,
            };
        }
    }
}
