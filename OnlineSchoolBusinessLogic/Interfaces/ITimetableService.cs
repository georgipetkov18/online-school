using OnlineSchoolBusinessLogic.Models;
using System.Security.Claims;

namespace OnlineSchoolBusinessLogic.Interfaces
{
    public interface ITimetableService
    {
        Task<IEnumerable<TimetableEntry>> GetEntriesByDayOfWeekAsync(ClaimsPrincipal user, string dayOfWeek);

        Task<IEnumerable<TimetableEntry>> GetCurrentDayEntriesAsync(ClaimsPrincipal user);

        Task<IEnumerable<IGrouping<string, TimetableEntry>>> GetTimetableAsync(ClaimsPrincipal user);

        Task<TimetableEntry?> GetNextEntryAsync(ClaimsPrincipal user);

        Task<TimetableEntry?> GetCurrentEntryAsync(ClaimsPrincipal user);

        Task AddTimetableEntryAsync(TimetableEntry timetableEntry);
    }
}
