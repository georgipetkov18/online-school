using OnlineSchoolApi.RequestModels;
using OnlineSchoolBusinessLogic.Models;

namespace OnlineSchoolApi
{
    public static class Mapper
    {
        public static Subject ToSubject(this SubjectInputModel subjectInputModel)
        {
            return new Subject
            {
                Name = subjectInputModel.Name,
                Code = subjectInputModel.Code,
            };
        }

        public static Lesson ToLesson(this LessonInputModel lessonInputModel)
        {
            return new Lesson
            {
                From = lessonInputModel.From,
                DurationInMinutes = lessonInputModel.DurationInMinutes,
            };
        }
    }
}
