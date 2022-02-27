﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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

        [HttpGet]
        public async Task<IActionResult> Now()
        {
            try
            {
                var currentEntry = await this.timetableService.GetCurrentEntryAsync(User);
                return Ok(currentEntry?.ToTimetableResponse());
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new {message = ex.Message});
            }
        }

        [HttpGet]
        public async Task<IActionResult> Next()
        {
            try
            {
                var nextEntry = await this.timetableService.GetNextEntryAsync(User);
                return Ok(nextEntry?.ToTimetableResponse());
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
                var timetable = await this.timetableService.GetTimetableAsync(User);
                return Ok(timetable?.Select(e => e.ToTimetableResponse()));
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
                return Ok(timetable?.Select(e => e.ToTimetableResponse()));
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet]
        [Route("{dayOfWeek}")]
        public async Task<IActionResult> Day([DayOfWeek]string dayOfWeek)
        {
            try
            {
                var timetable = await this.timetableService.GetEntriesByDayOfWeekAsync(User, dayOfWeek);
                return Ok(timetable?.Select(e => e.ToTimetableResponse()));
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
