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
                TeacherId = timetableEntity.Teacher.Id,
            };
        }

        public static TimetableEntity ToTimetableEntity(this TimetableEntry timetableEntry)
        {
            return new TimetableEntity
            {
                Subject = timetableEntry.Subject.ToSubjectEntity(),
                Lesson = timetableEntry.Lesson.ToLessonEntity(),
                Class = timetableEntry.Class.ToClassEntity(),
                Teacher = new TeacherEntity { Id = timetableEntry.TeacherId },
                Day = Enum.Parse<DayOfWeek>(timetableEntry.DayOfWeek),
            };
        }
    }
}
