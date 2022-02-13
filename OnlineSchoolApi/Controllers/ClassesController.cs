using Microsoft.AspNetCore.Mvc;
using OnlineSchoolApi.InputModels;
using OnlineSchoolApi.ResponseModels;
using OnlineSchoolBusinessLogic.Interfaces;
using OnlineSchoolData.CustomExceptions;

namespace OnlineSchoolApi.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class ClassesController : ControllerBase
    {
        private readonly IClassService classService;

        public ClassesController(IClassService classService)
        {
            this.classService = classService;
        }

        [HttpGet("{classId}")]
        public async Task<IActionResult> Get(Guid classId)
        {
            try
            {
                var _class = await this.classService.GetClassAsync(classId);
                return this.Ok(_class);
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
        public async Task<IActionResult> Add(ClassInputModel classInputModel)
        {
            try
            {
                var createdClass = await this.classService.AddClassAsync(classInputModel.ToClass());
                return this.CreatedAtAction(nameof(Get), new { classId = createdClass.Id }, createdClass);
            }
            catch (CustomException ex)
            {
                return this.BadRequest(new ErrorResponseModel
                {
                    ErrorMessage = ex.Message
                });
            }
        }

        [HttpPut("{classId}")]
        public async Task<IActionResult> Update(Guid classId, ClassInputModel classInputModel)
        {
            try
            {
                var updatedClass = await this.classService.UpdateClassAsync(classId, classInputModel.ToClass());
                return this.Ok(updatedClass);
            }
            catch (CustomException ex)
            {
                return this.BadRequest(new ErrorResponseModel
                {
                    ErrorMessage = ex.Message
                });
            }
        }

        [HttpDelete("{classId}")]
        public async Task<IActionResult> Delete(Guid classId)
        {
            try
            {
                await this.classService.DeleteClassAsync(classId);
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
