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
                FirstName = userInputModel.FirstName,
                LastName = userInputModel.LastName,
                Email = userInputModel.Email,
                Password = userInputModel.Password,
                RoleName = userInputModel.RoleName,
                ClassId = userInputModel.ClassId,
                SubjectId = userInputModel.SubjectId,
            };
        }

        public static TimetableEntryResponse ToTimetableEntryResponse(this TimetableEntry timetableEntry)
        {
            var lessonContinuity = new TimeSpan(0, timetableEntry.Lesson.DurationInMinutes, 0);

            return new TimetableEntryResponse
            {
                Name = timetableEntry.Subject.Name,
                Code = timetableEntry.Subject.Code,
                From = timetableEntry.Lesson.From,
                To = timetableEntry.Lesson.From.Add(lessonContinuity),
                Class = timetableEntry.Class.Name,
                Teacher = timetableEntry.Teacher.ToTeacherResponse(),
            };
        }

        public static TeacherResponse ToTeacherResponse(this Teacher teacher)
        {
            return new TeacherResponse
            {
                FirstName = teacher.FirstName,
                LastName = teacher.LastName,
            };
        }

        public static UserResponse ToUserResponse(this User user)
        {
            return new UserResponse
            {
                Username = user.Username,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                RoleName = user.RoleName,
                Status = user.Status,
            };
        }

        public static TimetableEntry ToTimetableEntry(this TimetableEntryInputModel timetableEntryInputModel)
        {
            return new TimetableEntry
            {
                DayOfWeek = timetableEntryInputModel.DayOfWeek,
                SubjectId = timetableEntryInputModel.SubjectId,
                ClassId = timetableEntryInputModel.ClassId,
                LessonId = timetableEntryInputModel.LessonId,
                TeacherId = timetableEntryInputModel.TeacherId,
            };
        }

        public static Dictionary<string, List<TimetableEntryResponse>> ToTimetableResponse(
            this IEnumerable<IGrouping<string, TimetableEntry>> groups)
        {
            var output = new Dictionary<string, List<TimetableEntryResponse>>();

            foreach (var group in groups)
            {
                output.Add(group.Key, group.Select(e => e.ToTimetableEntryResponse()).ToList());
            }
            return output;
        }
    }
}
