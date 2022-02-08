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
                From = lessonEntity.From,
                DurationInMinutes = lessonEntity.DurationInMinutes,
            };
        }

        public static LessonEntity ToLessonEntity(this Lesson lesson)
        {
            return new LessonEntity
            {
                Id = lesson.Id,
                From = lesson.From,
                DurationInMinutes = lesson.DurationInMinutes,
            };
        }
    }
}
