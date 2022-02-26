using Microsoft.EntityFrameworkCore;
using OnlineSchoolBusinessLogic.Interfaces;
using OnlineSchoolBusinessLogic.Models;
using OnlineSchoolData.Mappers;

namespace OnlineSchoolData.Repositories
{
    public class TimetableRepository : ITimetableRepository
    {
        private readonly ApplicationDbContext context;

        public TimetableRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task AddTimetableEntryAsync(TimetableEntry timetableEntry)
        {
            await this.context.Timetable.AddAsync(timetableEntry.ToTimetableEntity());
        }

        public async Task<IEnumerable<TimetableEntry>> GetCurrentDayEntriesAsync()
        {
            var currentDayOfWeek = DateTime.Now.DayOfWeek;

            return await this.context.Timetable
                .Where(t => t.Day == currentDayOfWeek)
                .Select(t => t.ToTimetableEntry())
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<TimetableEntry?> GetCurrentEntryAsync()
        {
            var now = DateTime.Now;
            var nowTimeSpan = new TimeSpan(now.Hour, now.Minute, now.Second);

            var currentDayEntries = await this.GetCurrentDayEntriesAsync();

            if (currentDayEntries.Count() < 1)
            {
                return null;
            }

            var currentEntry = currentDayEntries
                .OrderByDescending(x => x.Lesson.From)
                .FirstOrDefault(x => x.Lesson.From <= nowTimeSpan);

            return currentEntry;
        }

        public async Task<IEnumerable<TimetableEntry>> GetEntriesByDayOfWeekAsync(string dayOfWeek)
        {
            return await this.context.Timetable
                .Where(t => t.Day.ToString().ToLower() == dayOfWeek.ToLower())
                .Include(t => t.Subject)
                .Include(t => t.Lesson)
                .Include(t => t.Class)
                .Select(t => t.ToTimetableEntry())
                .ToListAsync();
        }

        public async Task<TimetableEntry?> GetNextEntryAsync()
        {
            var now = DateTime.Now;
            var nowTimeSpan = new TimeSpan(now.Hour, now.Minute, now.Second);

            var currentDayEntries = await this.GetCurrentDayEntriesAsync();

            if (currentDayEntries.Count() < 1)
            {
                return null;
            }

            var nextEntry = currentDayEntries
                .OrderBy(x => x.Lesson.From)
                .FirstOrDefault(x => x.Lesson.From > nowTimeSpan);

            return nextEntry;
        }

        public async Task<IEnumerable<TimetableEntry>> GetTimetableAsync()
        {
            return await this.context.Timetable
                .Include(t => t.Subject)
                .Include(t => t.Lesson)
                .Include(t => t.Class)
                .Select(t => t.ToTimetableEntry())
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
