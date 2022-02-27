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

        public async Task<TimetableEntry?> GetNextEntryAsync(ClaimsPrincipal user)
        {
            var userId = GetId(user);
            return await this.timetableRepository.GetNextEntryAsync(userId);
        }

        public async Task<IEnumerable<TimetableEntry>> GetTimetableAsync(ClaimsPrincipal user) 
        {
            var userId = GetId(user);
            return await this.timetableRepository.GetTimetableAsync(userId);
        }

        private Guid GetId(ClaimsPrincipal user)
        {
            var userId = user.Claims.FirstOrDefault(c => c.Type == "Id")?.Value!;

            return Guid.Parse(userId);
        }
    }
}
