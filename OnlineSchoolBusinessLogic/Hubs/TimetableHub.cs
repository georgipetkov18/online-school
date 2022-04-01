using Microsoft.AspNetCore.SignalR;

namespace OnlineSchoolBusinessLogic.Hubs
{
    public class TimetableHub : Hub<ITimetableHub>
    {
        public void SendInfoHandler()
        {
            //TimetableEntriesInfo lastInfo = new TimetableEntriesInfo();
            var timer = new Timer(async state =>
            {
                //var info = await this.timetableService.GetEntriesInfoAsync(User);
                //if (info.Next is null)
                //{

                //}
                //else if (info.Current is null)
                //{

                //}
                //else
                //{
                //    if (lastInfo.Current!.Lesson.From != info.Current!.Lesson.From && lastInfo.Next!.Lesson.From != info.Next!.Lesson.From)
                //    {
                //        await this.hub.Clients.All.SendAsync("getTimetableInfo", info);
                //    }
                //}
                //await this.hub.Clients.All.SendAsync("getTimetableInfo", "Working...");
                //await this.hub.Clients.All.SendAsync("Test", "Working...");

            }, new AutoResetEvent(false), 1000, 10000);
                Clients.Caller.SendInfo("test connection");
        }
    }

    public interface ITimetableHub
    {
        Task SendInfo(string message);
    }
}
