using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using OnlineSchoolBusinessLogic.Interfaces;
using OnlineSchoolBusinessLogic.Models;

namespace OnlineSchoolBusinessLogic.Hubs
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class TimetableHub : Hub<ITimetableHub>
    {
        private readonly ITimetableService timetableService;
        private readonly ILogger<ITimetableService> logger;

        public TimetableHub(ITimetableService timetableService, ILogger<ITimetableService> logger)
        {
            this.timetableService = timetableService;
            this.logger = logger;
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
                    try
                    {
                        if (info.Next is not null)
                        {
                            var delay = GetDelay(info.Next);
                            await Clients.Caller.WaitingForLesson(info.Next);
                            await Task.Delay(delay);
                            await OnLessonBegan();
                        }
                        else
                        {
                            await OnLastLessonEnded();
                        }
                    }
                    catch (ArgumentNullException)
                    {
                        await Clients.Caller.NoLessons();
                    }
                }
                else
                {
                    await Clients.Caller.LessonBegan(info);
                    var now = DateTime.Now;
                    var nowSpan = new TimeSpan(now.Hour, now.Minute, now.Second);
                    var lessonEndsSpan = info.Current.Lesson.From.Add(TimeSpan.FromMinutes(info.Current.Lesson.DurationInMinutes));
                    await Task.Delay(lessonEndsSpan - nowSpan);
                    await OnLessonEnded(info);
                }
            }
        }

        private async Task OnLessonEnded(TimetableEntriesInfo info)
        {
            if (info.Next is not null)
            {
                await Clients.Caller.LessonEnded(info.Next);
                var now = DateTime.Now;
                var delay = info.Next.Lesson.From - new TimeSpan(now.Hour, now.Minute, now.Second);
                await Task.Delay(delay);
                await OnLessonBegan();
            }
            else
            {
                await OnLastLessonEnded();
            }
            
        }

        private async Task OnLastLessonEnded()
        {
            var delay = await GetFutureDayDelayAsync();
            await Clients.Caller.LastLessonEnded();
            await Task.Delay(delay);
            await OnLessonBegan();
        }

        private TimeSpan GetDelay(TimetableEntry nextEntry)
        {
            var now = DateTime.Now;
            var nowSpan = new TimeSpan(now.Hour, now.Minute, now.Second);
            return nextEntry.Lesson.From - nowSpan;
        }

        private async Task<TimeSpan> GetFutureDayDelayAsync()
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
            if (firstEntry is null)
            {
                throw new ArgumentNullException(nameof(firstEntry), "There are no lessons");
            }
            var delay = firstEntry.Lesson.From.Add(TimeSpan.FromDays(daysToAdd)) - new TimeSpan(now.Hour, now.Minute, now.Second);
            return delay;
        }
    }

    public interface ITimetableHub
    {
        Task LessonBegan(TimetableEntriesInfo info);
        Task LessonEnded(TimetableEntry next);
        Task LastLessonEnded();
        Task WaitingForLesson(TimetableEntry next);
        Task NoLessons();
    }
}
