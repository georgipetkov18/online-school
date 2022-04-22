using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineSchoolBusinessLogic;
using OnlineSchoolBusinessLogic.Common;
using OnlineSchoolBusinessLogic.InputModels;
using OnlineSchoolBusinessLogic.Interfaces;
using OnlineSchoolData.CustomExceptions;

namespace OnlineSchoolApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClassesController : ControllerBase
    {
        private readonly IClassService classService;

        public ClassesController(IClassService classService)
        {
            this.classService = classService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] FilterInputModel filterInputModel)
        {
            var classes = await this.classService.GetAllClassesAsync(filterInputModel.Filter);
            return Ok(classes);
        }

        [HttpGet("[action]/{classId}")]
        public async Task<IActionResult> Get(Guid classId)
        {
            if (!ModelState.IsValid)
            {
                return this.BadRequest(ModelState);
            }

            try
            {
                var _class = await this.classService.GetClassAsync(classId);
                return this.Ok(_class);
            }
            catch (InvalidIdException ex)
            {
                ModelState.AddModelError("Id", ex.Message);
                return this.BadRequest(ModelState);
            }
        }

        [HttpPost("[action]")]
        [Authorize(Policy = Policies.RequireAdministratorRole)]
        public async Task<IActionResult> Add(ClassInputModel classInputModel)
        {
            if (!ModelState.IsValid)
            {
                return this.BadRequest(ModelState);
            }

            try
            {
                var createdClass = await this.classService.AddClassAsync(classInputModel.ToClass());
                return this.CreatedAtAction(nameof(Get), new { classId = createdClass.Id }, createdClass);
            }
            catch (InvalidIdException ex)
            {
                ModelState.AddModelError("Id", ex.Message);
                return this.BadRequest(ModelState);
            }
        }

        [HttpPut("[action]/{classId}")]
        [Authorize(Policy = Policies.RequireAdministratorRole)]
        public async Task<IActionResult> Update(Guid classId, ClassInputModel classInputModel)
        {
            if (!ModelState.IsValid)
            {
                return this.BadRequest(ModelState);
            }

            try
            {
                var updatedClass = await this.classService.UpdateClassAsync(classId, classInputModel.ToClass());
                return this.Ok(updatedClass);
            }
            catch (InvalidIdException ex)
            {
                ModelState.AddModelError("Id", ex.Message);
                return this.BadRequest(ModelState);
            }
        }

        [HttpDelete("[action]/{classId}")]
        [Authorize(Policy = Policies.RequireAdministratorRole)]
        public async Task<IActionResult> Delete(Guid classId)
        {
            if (!ModelState.IsValid)
            {
                return this.BadRequest(ModelState);
            }
            try
            {
                await this.classService.DeleteClassAsync(classId);
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
