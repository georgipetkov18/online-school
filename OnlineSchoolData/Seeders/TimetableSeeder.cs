using Microsoft.EntityFrameworkCore;
using OnlineSchoolBusinessLogic.Models;
using OnlineSchoolData.Mappers;

namespace OnlineSchoolData.Seeders
{
    public static class TimetableSeeder
    {
        public static async Task SeedTimetableAsync(this ApplicationDbContext context)
        {
            await context.Database.EnsureCreatedAsync();
            if (!await context.Timetable.AnyAsync())
            {
                var entries = new List<TimetableEntry>
                {
                    new TimetableEntry
                    {
                        DayOfWeek = "Monday",
                        SubjectId = (await context.Subjects.FirstOrDefaultAsync(s => s.Name == "Компютърна графика и дизайн"))!.Id,
                        LessonId = (await context.Lessons.FirstOrDefaultAsync(s => s.From == new TimeSpan(7, 30, 0)))!.Id,
                        ClassId = (await context.Classes.FirstOrDefaultAsync(s => s.Name == "12e"))!.Id,
                        TeacherId = (await context.Teachers.Include(t => t.User).FirstOrDefaultAsync(t => t.User.Email == "pepailkova@schoolmath.eu"))!.Id,
                    },
                    new TimetableEntry
                    {
                        DayOfWeek = "Monday",
                        SubjectId = (await context.Subjects.FirstOrDefaultAsync(s => s.Name == "Компютърна графика и дизайн"))!.Id,
                        LessonId = (await context.Lessons.FirstOrDefaultAsync(s => s.From == new TimeSpan(8, 20, 0)))!.Id,
                        ClassId = (await context.Classes.FirstOrDefaultAsync(s => s.Name == "12e"))!.Id,
                        TeacherId = (await context.Teachers.Include(t => t.User).FirstOrDefaultAsync(s => s.User.Email == "rosenvalchev@schoolmath.eu"))!.Id,
                    },
                    new TimetableEntry
                    {
                        DayOfWeek = "Monday",
                        SubjectId = (await context.Subjects.FirstOrDefaultAsync(s => s.Name == "Приложения с графичен потребителски интерфейс"))!.Id,
                        LessonId = (await context.Lessons.FirstOrDefaultAsync(s => s.From == new TimeSpan(9, 10, 0)))!.Id,
                        ClassId = (await context.Classes.FirstOrDefaultAsync(s => s.Name == "12e"))!.Id,
                        TeacherId = (await context.Teachers.Include(t => t.User).FirstOrDefaultAsync(s => s.User.Email == "rosenvalchev@schoolmath.eu"))!.Id,
                    },
                    new TimetableEntry
                    {
                        DayOfWeek = "Monday",
                        SubjectId = (await context.Subjects.FirstOrDefaultAsync(s => s.Name == "Математика"))!.Id,
                        LessonId = (await context.Lessons.FirstOrDefaultAsync(s => s.From == new TimeSpan(10, 10, 0)))!.Id,
                        ClassId = (await context.Classes.FirstOrDefaultAsync(s => s.Name == "12e"))!.Id,
                        TeacherId = (await context.Teachers.Include(t => t.User).FirstOrDefaultAsync(s => s.User.Email == "dafinkaandreeva@schoolmath.eu"))!.Id,
                    },
                    new TimetableEntry
                    {
                        DayOfWeek = "Monday",
                        SubjectId = (await context.Subjects.FirstOrDefaultAsync(s => s.Name == "Математика"))!.Id,
                        LessonId = (await context.Lessons.FirstOrDefaultAsync(s => s.From == new TimeSpan(11, 0, 0)))!.Id,
                        ClassId = (await context.Classes.FirstOrDefaultAsync(s => s.Name == "12e"))!.Id,
                        TeacherId = (await context.Teachers.Include(t => t.User).FirstOrDefaultAsync(s => s.User.Email == "dafinkaandreeva@schoolmath.eu"))!.Id,
                    },
                    new TimetableEntry
                    {
                        DayOfWeek = "Monday",
                        SubjectId = (await context.Subjects.FirstOrDefaultAsync(s => s.Name == "БЕЛ"))!.Id,
                        LessonId = (await context.Lessons.FirstOrDefaultAsync(s => s.From == new TimeSpan(11, 45, 0)))!.Id,
                        ClassId = (await context.Classes.FirstOrDefaultAsync(s => s.Name == "12e"))!.Id,
                        TeacherId = (await context.Teachers.Include(t => t.User).FirstOrDefaultAsync(s => s.User.Email == "ninatosheva@schoolmath.eu"))!.Id,
                    },
                    new TimetableEntry
                    {
                        DayOfWeek = "Monday",
                        SubjectId = (await context.Subjects.FirstOrDefaultAsync(s => s.Name == "ФВС"))!.Id,
                        LessonId = (await context.Lessons.FirstOrDefaultAsync(s => s.From == new TimeSpan(12, 30, 0)))!.Id,
                        ClassId = (await context.Classes.FirstOrDefaultAsync(s => s.Name == "12e"))!.Id,
                        TeacherId = (await context.Teachers.Include(t => t.User).FirstOrDefaultAsync(s => s.User.Email == "nedkokableshkov@schoolmath.eu"))!.Id,
                    },
                    new TimetableEntry
                    {
                        DayOfWeek = "Monday",
                        SubjectId = (await context.Subjects.FirstOrDefaultAsync(s => s.Name == "Час на класа"))!.Id,
                        LessonId = (await context.Lessons.FirstOrDefaultAsync(s => s.From == new TimeSpan(13, 20, 0)))!.Id,
                        ClassId = (await context.Classes.FirstOrDefaultAsync(s => s.Name == "12e"))!.Id,
                        TeacherId = (await context.Teachers.Include(t => t.User).FirstOrDefaultAsync(s => s.User.Email == "lilianedeleva@schoolmath.eu"))!.Id,
                    },


                    new TimetableEntry
                    {
                        DayOfWeek = "Tuesday",
                        SubjectId = (await context.Subjects.FirstOrDefaultAsync(s => s.Name == "Интернет програмиране"))!.Id,
                        LessonId = (await context.Lessons.FirstOrDefaultAsync(s => s.From == new TimeSpan(7, 30, 0)))!.Id,
                        ClassId = (await context.Classes.FirstOrDefaultAsync(s => s.Name == "12e"))!.Id,
                        TeacherId = (await context.Teachers.Include(t => t.User).FirstOrDefaultAsync(s => s.User.Email == "ivanpeev@schoolmath.eu"))!.Id,
                    },
                    new TimetableEntry
                    {
                        DayOfWeek = "Tuesday",
                        SubjectId = (await context.Subjects.FirstOrDefaultAsync(s => s.Name == "Интернет програмиране"))!.Id,
                        LessonId = (await context.Lessons.FirstOrDefaultAsync(s => s.From == new TimeSpan(8, 20, 0)))!.Id,
                        ClassId = (await context.Classes.FirstOrDefaultAsync(s => s.Name == "12e"))!.Id,
                        TeacherId = (await context.Teachers.Include(t => t.User).FirstOrDefaultAsync(s => s.User.Email == "ivanpeev@schoolmath.eu"))!.Id,
                    },
                    new TimetableEntry
                    {
                        DayOfWeek = "Tuesday",
                        SubjectId = (await context.Subjects.FirstOrDefaultAsync(s => s.Name == "Интернет програмиране"))!.Id,
                        LessonId = (await context.Lessons.FirstOrDefaultAsync(s => s.From == new TimeSpan(9, 10, 0)))!.Id,
                        ClassId = (await context.Classes.FirstOrDefaultAsync(s => s.Name == "12e"))!.Id,
                        TeacherId = (await context.Teachers.Include(t => t.User).FirstOrDefaultAsync(s => s.User.Email == "ivanpeev@schoolmath.eu"))!.Id,
                    },
                    new TimetableEntry
                    {
                        DayOfWeek = "Tuesday",
                        SubjectId = (await context.Subjects.FirstOrDefaultAsync(s => s.Name == "Интернет програмиране"))!.Id,
                        LessonId = (await context.Lessons.FirstOrDefaultAsync(s => s.From == new TimeSpan(10, 10, 0)))!.Id,
                        ClassId = (await context.Classes.FirstOrDefaultAsync(s => s.Name == "12e"))!.Id,
                        TeacherId = (await context.Teachers.Include(t => t.User).FirstOrDefaultAsync(s => s.User.Email == "ivanpeev@schoolmath.eu"))!.Id,
                    },
                    new TimetableEntry
                    {
                        DayOfWeek = "Tuesday",
                        SubjectId = (await context.Subjects.FirstOrDefaultAsync(s => s.Name == "БЕЛ"))!.Id,
                        LessonId = (await context.Lessons.FirstOrDefaultAsync(s => s.From == new TimeSpan(11, 0, 0)))!.Id,
                        ClassId = (await context.Classes.FirstOrDefaultAsync(s => s.Name == "12e"))!.Id,
                        TeacherId = (await context.Teachers.Include(t => t.User).FirstOrDefaultAsync(s => s.User.Email == "ninatosheva@schoolmath.eu"))!.Id,
                    },
                    new TimetableEntry
                    {
                        DayOfWeek = "Tuesday",
                        SubjectId = (await context.Subjects.FirstOrDefaultAsync(s => s.Name == "БЕЛ"))!.Id,
                        LessonId = (await context.Lessons.FirstOrDefaultAsync(s => s.From == new TimeSpan(11, 45, 0)))!.Id,
                        ClassId = (await context.Classes.FirstOrDefaultAsync(s => s.Name == "12e"))!.Id,
                        TeacherId = (await context.Teachers.Include(t => t.User).FirstOrDefaultAsync(s => s.User.Email == "ninatosheva@schoolmath.eu"))!.Id,
                    },
                    new TimetableEntry
                    {
                        DayOfWeek = "Tuesday",
                        SubjectId = (await context.Subjects.FirstOrDefaultAsync(s => s.Name == "Интернет програмиране"))!.Id,
                        LessonId = (await context.Lessons.FirstOrDefaultAsync(s => s.From == new TimeSpan(12, 30, 0)))!.Id,
                        ClassId = (await context.Classes.FirstOrDefaultAsync(s => s.Name == "12e"))!.Id,
                        TeacherId = (await context.Teachers.Include(t => t.User).FirstOrDefaultAsync(s => s.User.Email == "ivanpeev@schoolmath.eu"))!.Id,
                    },


                    new TimetableEntry
                    {
                        DayOfWeek = "Wednesday",
                        SubjectId = (await context.Subjects.FirstOrDefaultAsync(s => s.Name == "Немски език"))!.Id,
                        LessonId = (await context.Lessons.FirstOrDefaultAsync(s => s.From == new TimeSpan(7, 30, 0)))!.Id,
                        ClassId = (await context.Classes.FirstOrDefaultAsync(s => s.Name == "12e"))!.Id,
                        TeacherId = (await context.Teachers.Include(t => t.User).FirstOrDefaultAsync(s => s.User.Email == "miroslavaiovkova@schoolmath.eu"))!.Id,
                    },
                    new TimetableEntry
                    {
                        DayOfWeek = "Wednesday",
                        SubjectId = (await context.Subjects.FirstOrDefaultAsync(s => s.Name == "Чужд език по професията"))!.Id,
                        LessonId = (await context.Lessons.FirstOrDefaultAsync(s => s.From == new TimeSpan(8, 20, 0)))!.Id,
                        ClassId = (await context.Classes.FirstOrDefaultAsync(s => s.Name == "12e"))!.Id,
                        TeacherId = (await context.Teachers.Include(t => t.User).FirstOrDefaultAsync(s => s.User.Email == "lilianedeleva@schoolmath.eu"))!.Id,
                    },
                    new TimetableEntry
                    {
                        DayOfWeek = "Wednesday",
                        SubjectId = (await context.Subjects.FirstOrDefaultAsync(s => s.Name == "Математика"))!.Id,
                        LessonId = (await context.Lessons.FirstOrDefaultAsync(s => s.From == new TimeSpan(9, 10, 0)))!.Id,
                        ClassId = (await context.Classes.FirstOrDefaultAsync(s => s.Name == "12e"))!.Id,
                        TeacherId = (await context.Teachers.Include(t => t.User).FirstOrDefaultAsync(s => s.User.Email == "dafinkaandreeva@schoolmath.eu"))!.Id,
                    },
                    new TimetableEntry
                    {
                        DayOfWeek = "Wednesday",
                        SubjectId = (await context.Subjects.FirstOrDefaultAsync(s => s.Name == "Математика"))!.Id,
                        LessonId = (await context.Lessons.FirstOrDefaultAsync(s => s.From == new TimeSpan(10, 10, 0)))!.Id,
                        ClassId = (await context.Classes.FirstOrDefaultAsync(s => s.Name == "12e"))!.Id,
                        TeacherId = (await context.Teachers.Include(t => t.User).FirstOrDefaultAsync(s => s.User.Email == "dafinkaandreeva@schoolmath.eu"))!.Id,
                    },
                    new TimetableEntry
                    {
                        DayOfWeek = "Wednesday",
                        SubjectId = (await context.Subjects.FirstOrDefaultAsync(s => s.Name == "Софтуерно инженерство"))!.Id,
                        LessonId = (await context.Lessons.FirstOrDefaultAsync(s => s.From == new TimeSpan(11, 0, 0)))!.Id,
                        ClassId = (await context.Classes.FirstOrDefaultAsync(s => s.Name == "12e"))!.Id,
                        TeacherId = (await context.Teachers.Include(t => t.User).FirstOrDefaultAsync(s => s.User.Email == "hrisiplachkova@schoolmath.eu"))!.Id,
                    },
                    new TimetableEntry
                    {
                        DayOfWeek = "Wednesday",
                        SubjectId = (await context.Subjects.FirstOrDefaultAsync(s => s.Name == "Софтуерно инженерство"))!.Id,
                        LessonId = (await context.Lessons.FirstOrDefaultAsync(s => s.From == new TimeSpan(11, 45, 0)))!.Id,
                        ClassId = (await context.Classes.FirstOrDefaultAsync(s => s.Name == "12e"))!.Id,
                        TeacherId = (await context.Teachers.Include(t => t.User).FirstOrDefaultAsync(s => s.User.Email == "hrisiplachkova@schoolmath.eu"))!.Id,
                    },
                    new TimetableEntry
                    {
                        DayOfWeek = "Wednesday",
                        SubjectId = (await context.Subjects.FirstOrDefaultAsync(s => s.Name == "Софтуерно инженерство"))!.Id,
                        LessonId = (await context.Lessons.FirstOrDefaultAsync(s => s.From == new TimeSpan(12, 30, 0)))!.Id,
                        ClassId = (await context.Classes.FirstOrDefaultAsync(s => s.Name == "12e"))!.Id,
                        TeacherId = (await context.Teachers.Include(t => t.User).FirstOrDefaultAsync(s => s.User.Email == "hrisiplachkova@schoolmath.eu"))!.Id,
                    },
                    new TimetableEntry
                    {
                        DayOfWeek = "Wednesday",
                        SubjectId = (await context.Subjects.FirstOrDefaultAsync(s => s.Name == "Софтуерно инженерство"))!.Id,
                        LessonId = (await context.Lessons.FirstOrDefaultAsync(s => s.From == new TimeSpan(13, 20, 0)))!.Id,
                        ClassId = (await context.Classes.FirstOrDefaultAsync(s => s.Name == "12e"))!.Id,
                        TeacherId = (await context.Teachers.Include(t => t.User).FirstOrDefaultAsync(s => s.User.Email == "hrisiplachkova@schoolmath.eu"))!.Id,
                    },


                    new TimetableEntry
                    {
                        DayOfWeek = "Thursday",
                        SubjectId = (await context.Subjects.FirstOrDefaultAsync(s => s.Name == "Чужд език по професията"))!.Id,
                        LessonId = (await context.Lessons.FirstOrDefaultAsync(s => s.From == new TimeSpan(7, 30, 0)))!.Id,
                        ClassId = (await context.Classes.FirstOrDefaultAsync(s => s.Name == "12e"))!.Id,
                        TeacherId = (await context.Teachers.Include(t => t.User).FirstOrDefaultAsync(s => s.User.Email == "lilianedeleva@schoolmath.eu"))!.Id,
                    },
                    new TimetableEntry
                    {
                        DayOfWeek = "Thursday",
                        SubjectId = (await context.Subjects.FirstOrDefaultAsync(s => s.Name == "Приложения с графичен потребителски интерфейс"))!.Id,
                        LessonId = (await context.Lessons.FirstOrDefaultAsync(s => s.From == new TimeSpan(8, 20, 0)))!.Id,
                        ClassId = (await context.Classes.FirstOrDefaultAsync(s => s.Name == "12e"))!.Id,
                        TeacherId = (await context.Teachers.Include(t => t.User).FirstOrDefaultAsync(s => s.User.Email == "rosenvalchev@schoolmath.eu"))!.Id,
                    },
                    new TimetableEntry
                    {
                        DayOfWeek = "Thursday",
                        SubjectId = (await context.Subjects.FirstOrDefaultAsync(s => s.Name == "Приложения с графичен потребителски интерфейс"))!.Id,
                        LessonId = (await context.Lessons.FirstOrDefaultAsync(s => s.From == new TimeSpan(9, 10, 0)))!.Id,
                        ClassId = (await context.Classes.FirstOrDefaultAsync(s => s.Name == "12e"))!.Id,
                        TeacherId = (await context.Teachers.Include(t => t.User).FirstOrDefaultAsync(s => s.User.Email == "rosenvalchev@schoolmath.eu"))!.Id,
                    },
                    new TimetableEntry
                    {
                        DayOfWeek = "Thursday",
                        SubjectId = (await context.Subjects.FirstOrDefaultAsync(s => s.Name == "Гражданско образование"))!.Id,
                        LessonId = (await context.Lessons.FirstOrDefaultAsync(s => s.From == new TimeSpan(10, 10, 0)))!.Id,
                        ClassId = (await context.Classes.FirstOrDefaultAsync(s => s.Name == "12e"))!.Id,
                        TeacherId = (await context.Teachers.Include(t => t.User).FirstOrDefaultAsync(s => s.User.Email == "desislavavachkovamateeva@schoolmath.eu"))!.Id,
                    },
                    new TimetableEntry
                    {
                        DayOfWeek = "Thursday",
                        SubjectId = (await context.Subjects.FirstOrDefaultAsync(s => s.Name == "БЕЛ"))!.Id,
                        LessonId = (await context.Lessons.FirstOrDefaultAsync(s => s.From == new TimeSpan(11, 0, 0)))!.Id,
                        ClassId = (await context.Classes.FirstOrDefaultAsync(s => s.Name == "12e"))!.Id,
                        TeacherId = (await context.Teachers.Include(t => t.User).FirstOrDefaultAsync(s => s.User.Email == "ninatosheva@schoolmath.eu"))!.Id,
                    },
                    new TimetableEntry
                    {
                        DayOfWeek = "Thursday",
                        SubjectId = (await context.Subjects.FirstOrDefaultAsync(s => s.Name == "БЕЛ"))!.Id,
                        LessonId = (await context.Lessons.FirstOrDefaultAsync(s => s.From == new TimeSpan(11, 45, 0)))!.Id,
                        ClassId = (await context.Classes.FirstOrDefaultAsync(s => s.Name == "12e"))!.Id,
                        TeacherId = (await context.Teachers.Include(t => t.User).FirstOrDefaultAsync(s => s.User.Email == "ninatosheva@schoolmath.eu"))!.Id,
                    },
                    new TimetableEntry
                    {
                        DayOfWeek = "Thursday",
                        SubjectId = (await context.Subjects.FirstOrDefaultAsync(s => s.Name == "Мрежови протоколи и технологии"))!.Id,
                        LessonId = (await context.Lessons.FirstOrDefaultAsync(s => s.From == new TimeSpan(12, 30, 0)))!.Id,
                        ClassId = (await context.Classes.FirstOrDefaultAsync(s => s.Name == "12e"))!.Id,
                        TeacherId = (await context.Teachers.Include(t => t.User).FirstOrDefaultAsync(s => s.User.Email == "yanislavkartelov@schoolmath.eu"))!.Id,
                    },


                    new TimetableEntry
                    {
                        DayOfWeek = "Friday",
                        SubjectId = (await context.Subjects.FirstOrDefaultAsync(s => s.Name == "ФВС"))!.Id,
                        LessonId = (await context.Lessons.FirstOrDefaultAsync(s => s.From == new TimeSpan(7, 30, 0)))!.Id,
                        ClassId = (await context.Classes.FirstOrDefaultAsync(s => s.Name == "12e"))!.Id,
                        TeacherId = (await context.Teachers.Include(t => t.User).FirstOrDefaultAsync(s => s.User.Email == "nedkokableshkov@schoolmath.eu"))!.Id,
                    },
                    new TimetableEntry
                    {
                        DayOfWeek = "Friday",
                        SubjectId = (await context.Subjects.FirstOrDefaultAsync(s => s.Name == "Програмиране за вградени системи"))!.Id,
                        LessonId = (await context.Lessons.FirstOrDefaultAsync(s => s.From == new TimeSpan(8, 20, 0)))!.Id,
                        ClassId = (await context.Classes.FirstOrDefaultAsync(s => s.Name == "12e"))!.Id,
                        TeacherId = (await context.Teachers.Include(t => t.User).FirstOrDefaultAsync(s => s.User.Email == "zhivkoradev@schoolmath.eu"))!.Id,
                    },
                    new TimetableEntry
                    {
                        DayOfWeek = "Friday",
                        SubjectId = (await context.Subjects.FirstOrDefaultAsync(s => s.Name == "Програмиране за вградени системи"))!.Id,
                        LessonId = (await context.Lessons.FirstOrDefaultAsync(s => s.From == new TimeSpan(9, 10, 0)))!.Id,
                        ClassId = (await context.Classes.FirstOrDefaultAsync(s => s.Name == "12e"))!.Id,
                        TeacherId = (await context.Teachers.Include(t => t.User).FirstOrDefaultAsync(s => s.User.Email == "zhivkoradev@schoolmath.eu"))!.Id,
                    },
                    new TimetableEntry
                    {
                        DayOfWeek = "Friday",
                        SubjectId = (await context.Subjects.FirstOrDefaultAsync(s => s.Name == "Програмиране за вградени системи"))!.Id,
                        LessonId = (await context.Lessons.FirstOrDefaultAsync(s => s.From == new TimeSpan(10, 10, 0)))!.Id,
                        ClassId = (await context.Classes.FirstOrDefaultAsync(s => s.Name == "12e"))!.Id,
                        TeacherId = (await context.Teachers.Include(t => t.User).FirstOrDefaultAsync(s => s.User.Email == "yanislavkartelov@schoolmath.eu"))!.Id,
                    },
                    new TimetableEntry
                    {
                        DayOfWeek = "Friday",
                        SubjectId = (await context.Subjects.FirstOrDefaultAsync(s => s.Name == "Математика"))!.Id,
                        LessonId = (await context.Lessons.FirstOrDefaultAsync(s => s.From == new TimeSpan(11, 0, 0)))!.Id,
                        ClassId = (await context.Classes.FirstOrDefaultAsync(s => s.Name == "12e"))!.Id,
                        TeacherId = (await context.Teachers.Include(t => t.User).FirstOrDefaultAsync(s => s.User.Email == "dafinkaandreeva@schoolmath.eu"))!.Id,
                    },
                    new TimetableEntry
                    {
                        DayOfWeek = "Friday",
                        SubjectId = (await context.Subjects.FirstOrDefaultAsync(s => s.Name == "Немски език"))!.Id,
                        LessonId = (await context.Lessons.FirstOrDefaultAsync(s => s.From == new TimeSpan(11, 45, 0)))!.Id,
                        ClassId = (await context.Classes.FirstOrDefaultAsync(s => s.Name == "12e"))!.Id,
                        TeacherId = (await context.Teachers.Include(t => t.User).FirstOrDefaultAsync(s => s.User.Email == "miroslavaiovkova@schoolmath.eu"))!.Id,
                    },
                    new TimetableEntry
                    {
                        DayOfWeek = "Friday",
                        SubjectId = (await context.Subjects.FirstOrDefaultAsync(s => s.Name == "Мрежови протоколи и технологии"))!.Id,
                        LessonId = (await context.Lessons.FirstOrDefaultAsync(s => s.From == new TimeSpan(12, 30, 0)))!.Id,
                        ClassId = (await context.Classes.FirstOrDefaultAsync(s => s.Name == "12e"))!.Id,
                        TeacherId = (await context.Teachers.Include(t => t.User).FirstOrDefaultAsync(s => s.User.Email == "denitsagrozeva@schoolmath.eu"))!.Id,
                    },
                };

                await context.Timetable.AddRangeAsync(entries.Select(e => e.ToTimetableEntity()));
                await context.SaveChangesAsync();
            }
        }
    }
}