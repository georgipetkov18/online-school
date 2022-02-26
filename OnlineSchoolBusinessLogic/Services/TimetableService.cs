using OnlineSchoolBusinessLogic.Interfaces;
using OnlineSchoolBusinessLogic.Models;

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

        public async Task<IEnumerable<TimetableEntry>> GetCurrentDayEntriesAsync() =>
            await this.timetableRepository.GetCurrentDayEntriesAsync();

        public async Task<TimetableEntry?> GetCurrentEntryAsync() =>
            await this.timetableRepository.GetCurrentEntryAsync();

        public async Task<IEnumerable<TimetableEntry>> GetEntriesByDayOfWeekAsync(string dayOfWeek) =>
            await this.timetableRepository.GetEntriesByDayOfWeekAsync(dayOfWeek);

        public async Task<TimetableEntry?> GetNextEntryAsync() =>
            await this.timetableRepository.GetNextEntryAsync();

        public async Task<IEnumerable<TimetableEntry>> GetTimetableAsync() =>
            await this.timetableRepository.GetTimetableAsync();
    }
}
