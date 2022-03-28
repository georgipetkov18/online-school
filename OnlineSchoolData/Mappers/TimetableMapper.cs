using OnlineSchoolBusinessLogic.Models;
using OnlineSchoolData.Entities;

namespace OnlineSchoolData.Mappers
{
    public static class TimetableMapper
    {
        public static TimetableEntry ToTimetableEntry(this TimetableEntity timetableEntity)
        {
            if (timetableEntity.Teacher is null)
            {
                return new TimetableEntry
                {
                    Id = timetableEntity.Id,
                    Subject = timetableEntity.Subject.ToSubject(),
                    Lesson = timetableEntity.Lesson.ToLesson(),
                    DayOfWeek = timetableEntity.Day.ToString(),
                    Class = timetableEntity.Class.ToClass(),
                };
            }

            return new TimetableEntry
            {
                Id = timetableEntity.Id,
                Subject = timetableEntity.Subject.ToSubject(),
                Lesson = timetableEntity.Lesson.ToLesson(),
                DayOfWeek = timetableEntity.Day.ToString(),
                Class = timetableEntity.Class.ToClass(),
                Teacher = timetableEntity.Teacher.ToTeacher(),
            };
        }

        public static TimetableEntity ToTimetableEntity(this TimetableEntry timetableEntry)
        {
            return new TimetableEntity
            {
                SubjectId = timetableEntry.SubjectId,
                LessonId = timetableEntry.LessonId,
                ClassId = timetableEntry.ClassId,
                TeacherId = timetableEntry.TeacherId,
                Day = Enum.Parse<DayOfWeek>(timetableEntry.DayOfWeek),
            };
        }
    }
}
