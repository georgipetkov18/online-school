using Microsoft.AspNetCore.Mvc;
using OnlineSchoolApi.InputModels;
using OnlineSchoolApi.ResponseModels;
using OnlineSchoolBusinessLogic.Interfaces;
using OnlineSchoolData.CustomExceptions;

namespace OnlineSchoolApi.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class LessonsController : ControllerBase
    {
        private readonly ILessonsService lessonService;

        public LessonsController(ILessonsService lessonService)
        {
            this.lessonService = lessonService;
        }

        [HttpGet("{lessonId}")]
        public async Task<IActionResult> Get(Guid lessonId)
        {
            if (!ModelState.IsValid)
            {
                return this.BadRequest(ModelState);
            }

            try
            {
                var lesson = await this.lessonService.GetLessonAsync(lessonId);
                return this.Ok(lesson);
            }

            catch (InvalidIdException ex)
            {
                ModelState.AddModelError("Id", ex.Message);
                return this.BadRequest(ModelState);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Add(LessonInputModel lessonInputModel)
        {
            if (!ModelState.IsValid)
            {
                return this.BadRequest(ModelState);
            }

            var createdLesson = await this.lessonService.AddLessonAsync(lessonInputModel.ToLesson());
            return this.CreatedAtAction(nameof(Get), new { lessonId = createdLesson.Id }, createdLesson);

        }

        [HttpPut("{lessonId}")]
        public async Task<IActionResult> Update(Guid lessonId, LessonInputModel lessonInputModel)
        {
            if (!ModelState.IsValid)
            {
                return this.BadRequest(ModelState);
            }

            try
            {
                var updatedLesson = await this.lessonService.UpdateLessonAsync(lessonId, lessonInputModel.ToLesson());
                return this.Ok(updatedLesson);
            }
            catch (InvalidIdException ex)
            {
                ModelState.AddModelError("Id", ex.Message);
                return this.BadRequest(ModelState);
            }
        }

        [HttpDelete("{lessonId}")]
        public async Task<IActionResult> Delete(Guid lessonId)
        {
            if (!ModelState.IsValid)
            {
                return this.BadRequest(ModelState);
            }

            try
            {
                await this.lessonService.DeleteLessonAsync(lessonId);
                return this.Ok();
            }
            catch (InvalidIdException ex)
            {
                ModelState.AddModelError("Id", ex.Message);
                return this.BadRequest(ModelState);
            }
        }
    }
}
