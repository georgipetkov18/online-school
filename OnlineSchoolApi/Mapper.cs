using OnlineSchoolApi.RequestModels;
using OnlineSchoolBusinessLogic.Models;

namespace OnlineSchoolApi
{
    public static class Mapper
    {
        public static Subject ToSubject(this SubjectInputModel subjectInputModel)
        {
            if (subjectInputModel.Id is null)
            {
                return new Subject
                {
                    Name = subjectInputModel.Name,
                    Code = subjectInputModel.Code,
                };
            }

            return new Subject
            {
                Id = subjectInputModel.Id.Value,
                Name = subjectInputModel.Name,
                Code = subjectInputModel.Code,
            };
        }

        public static Lesson ToLesson(this LessonInputModel lessonInputModel)
        {
            if (lessonInputModel.Id is null)
            {
                return new Lesson
                {
                    From = lessonInputModel.From,
                    DurationInMinutes = lessonInputModel.DurationInMinutes,
                };
            }

            return new Lesson
            {
                Id = lessonInputModel.Id.Value,
                From = lessonInputModel.From,
                DurationInMinutes = lessonInputModel.DurationInMinutes,
            };
        }
    }
}
