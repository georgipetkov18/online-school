using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineSchoolBusinessLogic.Common;
using OnlineSchoolBusinessLogic.Interfaces;
using OnlineSchoolData.CustomExceptions;

namespace OnlineSchoolApi.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentsService studentService;

        public StudentsController(IStudentsService studentService)
        {
            this.studentService = studentService;
        }

        [HttpGet("{studentId}")]
        public async Task<IActionResult> Get(Guid studentId)
        {
            if (!ModelState.IsValid)
            {
                return this.BadRequest(ModelState);
            }

            try
            {
                var student = await this.studentService.GetStudentAsync(studentId);
                return this.Ok(student);
            }
            catch (InvalidIdException ex)
            {
                ModelState.AddModelError("Id", ex.Message);
                return this.BadRequest(ModelState);
            }
        }

        [HttpDelete("{studentId}")]
        [Authorize(Policy = Policies.RequireAdministratorRole)]
        public async Task<IActionResult> Delete(Guid studentId)
        {
            if (!ModelState.IsValid)
            {
                return this.BadRequest(ModelState);
            }

            try
            {
                await this.studentService.DeleteStudentAsync(studentId);
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
