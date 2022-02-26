using OnlineSchoolApi.InputModels;
using OnlineSchoolApi.ResponseModels;
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

        public static User ToStudent(this StudentInputModel studentInputModel)
        {
            return new User
            {
                Username = studentInputModel.Username,
                Password = studentInputModel.Password,
                Email = studentInputModel.Email,
                ClassId = studentInputModel.ClassId,
            };
        }

        public static Class ToClass(this ClassInputModel classInputModel)
        {
            return new Class
            {
                Name = classInputModel.Name,
            };
        }

        public static AuthenticateResponse ToAuthenticateResponse(this AuthenticateModel authenticateModel)
        {
            return new AuthenticateResponse
            {
                Username = authenticateModel.Username,
                Email = authenticateModel.Email,
                Role = authenticateModel.Role,
                JwtToken = authenticateModel.JwtToken,
            };
        }

        public static User ToUser(this UserInputModel userInputModel)
        {
            return new User
            {
                Username = userInputModel.Username,
                Email = userInputModel.Email,
                Password = userInputModel.Password,
                RoleName = userInputModel.RoleName,
                ClassId = userInputModel.ClassId,
                SubjectId = userInputModel.SubjectId,
            };
        }
    }
}
