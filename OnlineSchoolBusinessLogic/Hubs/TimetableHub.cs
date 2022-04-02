using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using OnlineSchoolBusinessLogic.Interfaces;
using OnlineSchoolBusinessLogic.Models;

namespace OnlineSchoolBusinessLogic.Hubs
{
    [Authorize]
    public class TimetableHub : Hub<ITimetableHub>
    {
        private readonly ITimetableService timetableService;

        public TimetableHub(ITimetableService timetableService)
        {
            this.timetableService = timetableService;
        }

        public async Task GetData()
        {
            await OnLessonBegan();
        }

        private async Task OnLessonBegan()
        {
            var info = await this.timetableService.GetEntriesInfoAsync(Context.User);
            if (info is not null)
            {
                if (info.Current is null)
                {
                    var delay = await GetDelayAsync();
                    await Task.Delay(delay).ContinueWith(async state => { await OnLessonBegan(); });
                }
                else
                {
                    await Clients.Caller.LessonBegan(info);
                    await Task.Delay(TimeSpan.FromMinutes(info.Current.Lesson.DurationInMinutes))
                        .ContinueWith(async state => { await OnLessonEnded(info); });
                }
            }
        }

        private async Task OnLessonEnded(TimetableEntriesInfo info)
        {
            if (info.Next is null)
            {
                await Clients.Caller.LastLessonEnded();
                await OnLessonBegan();
            }
            else
            {
                await Clients.Caller.LessonEnded();
                var now = DateTime.Now;
                var delay = info.Next.Lesson.From - new TimeSpan(now.Hour, now.Minute, now.Second);
                await Task.Delay(delay).ContinueWith(async state => { await OnLessonBegan(); });
            }
        }

        private async Task<TimeSpan> GetDelayAsync()
        {
            var now = DateTime.Now;
            var daysToAdd = 1;

            if (now.DayOfWeek == DayOfWeek.Friday)
            {
                daysToAdd = 3;
            }
            else if (now.DayOfWeek == DayOfWeek.Saturday)
            {
                daysToAdd = 2;
            }

            var day = now.AddDays(daysToAdd).DayOfWeek;
            var entries = await this.timetableService.GetEntriesByDayOfWeekAsync(Context.User, day.ToString());
            var firstEntry = entries.OrderBy(x => x.Lesson.From).FirstOrDefault();
            var delay = firstEntry.Lesson.From.Add(TimeSpan.FromDays(daysToAdd)) - new TimeSpan(now.Hour, now.Minute, now.Second);
            return delay;
        }
    }

    public interface ITimetableHub
    {
        Task LessonBegan(TimetableEntriesInfo info);
        Task LessonEnded();
        Task LastLessonEnded();
    }
}
