using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineSchoolBusinessLogic.Interfaces;

namespace OnlineSchoolApi.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class TimetableController : ControllerBase
    {
        private readonly ITimetableService timetableService;

        public TimetableController(ITimetableService timetableService)
        {
            this.timetableService = timetableService;
        }

        [Authorize]
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
    }
}
