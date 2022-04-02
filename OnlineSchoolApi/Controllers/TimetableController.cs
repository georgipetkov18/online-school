using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineSchoolApi.InputModels;
using OnlineSchoolBusinessLogic.Common;
using OnlineSchoolBusinessLogic.Interfaces;

namespace OnlineSchoolApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class TimetableController : ControllerBase
    {
        private readonly ITimetableService timetableService;

        public TimetableController(ITimetableService timetableService)
        {
            this.timetableService = timetableService;
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
