using Microsoft.AspNetCore.Mvc;
using OnlineSchoolApi.RequestModels;
using OnlineSchoolApi.ResponseModels;
using OnlineSchoolBusinessLogic.Interfaces;
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
            catch (CustomException ex)
            {
                return this.BadRequest(new ErrorResponseModel
                { 
                    ErrorMessage = ex.Message
                });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Add(LessonInputModel lessonInputModel)
        {
            try
            {
                var createdLesson = await this.lessonService.AddLessonAsync(lessonInputModel.ToLesson());
                return this.CreatedAtAction(nameof(Get), new { lessonId = createdLesson.Id }, createdLesson);
            }
            catch (CustomException ex)
            {
                return this.BadRequest(new ErrorResponseModel
                {
                    ErrorMessage = ex.Message
                });
            }
        }

        [HttpPut("{lessonId}")]
        public async Task<IActionResult> Update(Guid lessonId, LessonInputModel lessonInputModel)
        {
            try
            {
                var updatedLesson = await this.lessonService.UpdateLessonAsync(lessonId, lessonInputModel.ToLesson());
                return this.Ok(updatedLesson);
            }
            catch (CustomException ex)
            {
                return this.BadRequest(new ErrorResponseModel
                {
                    ErrorMessage = ex.Message
                });
            }
        }

        [HttpDelete("{lessonId}")]
        public async Task<IActionResult> Delete(Guid lessonId)
        {
            try
            {
                await this.lessonService.DeleteLessonAsync(lessonId);
                return this.Ok();
            }
            catch (CustomException ex)
            {
                return this.BadRequest(new ErrorResponseModel
                {
                    ErrorMessage = ex.Message
                });
            }
        }
    }
}
