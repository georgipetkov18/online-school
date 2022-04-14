using OnlineSchoolBusinessLogic.Models;

namespace OnlineSchoolBusinessLogic.Interfaces
{
    public interface ITimetableRepository
    {
        Task<IEnumerable<TimetableEntry>> GetEntriesByDayOfWeekAsync(Guid userId, string dayOfWeek);
        Task<IEnumerable<TimetableEntry>> GetTimetableAsync(Guid userId);
        Task<IEnumerable<TimetableEntry>> GetTimetableByClassIdAsync(Guid classId);
        Task<IEnumerable<TimetableEntry>> GetCurrentDayEntriesAsync(Guid userId);
        Task<TimetableEntry?> GetNextEntryAsync(Guid userId);
        Task<TimetableEntry?> GetCurrentEntryAsync(Guid userId);
        Task AddTimetableEntryAsync(TimetableEntry timetableEntry);
        Task<TimetableEntry> UpdateTimetableEntryAsync(Guid timetableEntryid, TimetableEntry timetableEntry);
        Task DeleteTimetableEntryAsync(Guid timetableEntryid);
    }
}
