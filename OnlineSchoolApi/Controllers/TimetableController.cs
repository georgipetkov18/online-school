using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using OnlineSchoolApi.InputModels;
using OnlineSchoolBusinessLogic.Common;
using OnlineSchoolBusinessLogic.Hubs;
using OnlineSchoolBusinessLogic.Interfaces;
using OnlineSchoolBusinessLogic.Models;

namespace OnlineSchoolApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class TimetableController : ControllerBase
    {
        private readonly ITimetableService timetableService;
        private readonly IHubContext<TimetableHub> hub;

        public TimetableController(ITimetableService timetableService, IHubContext<TimetableHub> hub)
        {
            this.timetableService = timetableService;
            this.hub = hub;
        }

        [HttpPost]
        public async Task<IActionResult> Add(TimetableInputModel timetableInputModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await this.timetableService.AddTimetable(timetableInputModel.Entries.Select(e => e.ToTimetableEntry()));
                return Ok();
            }
            catch (ArgumentException ex)
            {
                ModelState.AddModelError("Input", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Now()
        {
            try
            {
                var currentEntry = await this.timetableService.GetCurrentEntryAsync(User);
                return Ok(currentEntry?.ToTimetableEntryResponse());
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> Next()
        {
            try
            {
                var nextEntry = await this.timetableService.GetNextEntryAsync(User);
                return Ok(nextEntry?.ToTimetableEntryResponse());
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> Info()
        {
            try
            {
                TimetableEntriesInfo lastInfo = new TimetableEntriesInfo();
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
                    await this.hub.Clients.All.SendAsync("getTimetableInfo", "Working...");

                }, new AutoResetEvent(false), 1000, 5 * 60 * 1000);
                return Ok();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> Full()
        {
            try
            {
                var timetableGroup = await this.timetableService.GetTimetableAsync(User);
                return Ok(timetableGroup.ToTimetableResponse());
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> CurrentDay()
        {
            try
            {
                var timetable = await this.timetableService.GetCurrentDayEntriesAsync(User);
                return Ok(timetable?.Select(e => e.ToTimetableEntryResponse()));
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet]
        [Route("{dayOfWeek}")]
        public async Task<IActionResult> Day([DayOfWeek] string dayOfWeek)
        {
            try
            {
                var timetable = await this.timetableService.GetEntriesByDayOfWeekAsync(User, dayOfWeek);
                return Ok(timetable?.Select(e => e.ToTimetableEntryResponse()));
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
