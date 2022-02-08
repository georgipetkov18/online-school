using Microsoft.AspNetCore.Mvc;
using OnlineSchoolApi.ResponseModels;
using OnlineSchoolBusinessLogic.Services;
using OnlineSchoolData.CustomExceptions;

namespace OnlineSchoolApi.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class LessonsController : ControllerBase
    {
        private readonly ILessonService lessonService;

        public LessonsController(ILessonService lessonService)
        {
            this.lessonService = lessonService;
        }

        [HttpGet("{lessonId}")]
        public async Task<IActionResult> Get(Guid lessonId)
        {
            try
            {
                var lesson = await this.lessonService.GetLessonAsync(lessonId);
                return this.Ok(lesson);
            }
            catch (InvalidIdException ex)
            {
                return this.BadRequest(new ErrorResponseModel { ErrorMessage = ex.Message});
            }
            catch (ArgumentNullException)
            {
                return this.BadRequest();
            }
        }
    }
}
