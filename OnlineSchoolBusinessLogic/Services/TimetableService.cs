using OnlineSchoolBusinessLogic.Interfaces;
using OnlineSchoolBusinessLogic.Models;
using System.Security.Claims;

namespace OnlineSchoolBusinessLogic.Services
{
    public class TimetableService : ITimetableService
    {
        private readonly ITimetableRepository timetableRepository;

        public TimetableService(ITimetableRepository timetableRepository)
        {
            this.timetableRepository = timetableRepository;
        }

        public async Task AddTimetable(IEnumerable<TimetableEntry> timetable)
        {
            foreach (var entry in timetable)
            {
                await timetableRepository.AddTimetableEntryAsync(entry);
            }
        }

        public async Task AddTimetableEntryAsync(TimetableEntry timetableEntry) =>
            await this.timetableRepository.AddTimetableEntryAsync(timetableEntry);

        public async Task<IEnumerable<TimetableEntry>> GetCurrentDayEntriesAsync(ClaimsPrincipal user)
        {
            var userId = GetId(user);
            return await this.timetableRepository.GetCurrentDayEntriesAsync(userId);
        }

        public async Task<TimetableEntry?> GetCurrentEntryAsync(ClaimsPrincipal user)
        {
            var userId = GetId(user);
            return await this.timetableRepository.GetCurrentEntryAsync(userId);
        }

        public async Task<IEnumerable<TimetableEntry>> GetEntriesByDayOfWeekAsync(ClaimsPrincipal user, string dayOfWeek)
        {
            var userId = GetId(user);
            return await this.timetableRepository.GetEntriesByDayOfWeekAsync(userId, dayOfWeek);
        }

        public async Task<TimetableEntriesInfo> GetEntriesInfoAsync(ClaimsPrincipal user)
        {
            var userId = GetId(user);
            var current = await this.timetableRepository.GetCurrentEntryAsync(userId);
            var next = await this.timetableRepository.GetNextEntryAsync(userId);
            var now = DateTime.Now;
            var sendInfoAfter = next?.Lesson.From - new TimeSpan(now.Hour, now.Minute, now.Second);
            return new TimetableEntriesInfo 
            { 
                //SendInfoAfter = sendInfoAfter is null ? new TimeSpan() : sendInfoAfter.Value,
                SendInfoAfter = TimeSpan.FromSeconds(5),
                Current = current, 
                Next = next 
            };
        }

        public async Task<TimetableEntry?> GetNextEntryAsync(ClaimsPrincipal user)
        {
            var userId = GetId(user);
            return await this.timetableRepository.GetNextEntryAsync(userId);
        }

        public async Task<IEnumerable<IGrouping<string, TimetableEntry>>> GetTimetableAsync(ClaimsPrincipal user)
        {
            var userId = GetId(user);
            var timetable = await this.timetableRepository.GetTimetableAsync(userId);
            return timetable
                .GroupBy(e => e.DayOfWeek);
        }

        private Guid GetId(ClaimsPrincipal user)
        {
            var userId = user.Claims.FirstOrDefault(c => c.Type == "Id")?.Value!;

            return Guid.Parse(userId);
        }
    }
}
