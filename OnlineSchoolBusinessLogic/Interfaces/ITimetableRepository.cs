using OnlineSchoolBusinessLogic.Models;

namespace OnlineSchoolBusinessLogic.Interfaces
{
    public interface ITimetableRepository
    {
        Task<IEnumerable<TimetableEntry>> GetEntriesByDayOfWeekAsync(string dayOfWeek);

        Task<IEnumerable<TimetableEntry>> GetCurrentDayEntriesAsync();

        Task<IEnumerable<TimetableEntry>> GetTimetableAsync();

        Task<TimetableEntry?> GetNextEntryAsync();

        Task<TimetableEntry?> GetCurrentEntryAsync();

        Task AddTimetableEntryAsync(TimetableEntry timetableEntry);
    }
}
