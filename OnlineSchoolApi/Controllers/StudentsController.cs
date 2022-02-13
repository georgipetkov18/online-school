using Microsoft.AspNetCore.Mvc;
using OnlineSchoolApi.InputModels;
using OnlineSchoolApi.ResponseModels;
using OnlineSchoolBusinessLogic.Interfaces;
using OnlineSchoolData.CustomExceptions;

namespace OnlineSchoolApi.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentService studentService;

        public StudentsController(IStudentService studentService)
        {
            this.studentService = studentService;
        }

        [HttpPost]
        public async Task<IActionResult> Register(StudentInputModel studentInputModel)
        {
            if (!ModelState.IsValid)
            {
                return this.BadRequest(ModelState);
            }

            try
            {
                var createdStudent = await this.studentService.AddStudentAsync(studentInputModel.ToStudent());
                return this.CreatedAtAction(nameof(Get), new { studentId = createdStudent.Id }, createdStudent);
            }
            catch (InvalidIdException ex)
            {
                ModelState.AddModelError("Id", ex.Message);
                return this.BadRequest(ModelState);
            }
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
