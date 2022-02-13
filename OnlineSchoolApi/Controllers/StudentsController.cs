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
            try
            {
                var createdStudent = await this.studentService.AddStudentAsync(studentInputModel.ToStudent());
                return this.CreatedAtAction(nameof(Get), new { studentId = createdStudent.Id }, createdStudent);
            }
            catch (CustomException ex)
            {
                return this.BadRequest(new ErrorResponseModel
                {
                    ErrorMessage = ex.Message
                });
            }
        }

        [HttpGet("{studentId}")]
        public async Task<IActionResult> Get(Guid studentId)
        {
            try
            {
                var student = await this.studentService.GetStudentAsync(studentId);
                return this.Ok(student);
            }
            catch (CustomException ex)
            {
                return this.BadRequest(new ErrorResponseModel
                {
                    ErrorMessage = ex.Message
                });
            }
        }

        [HttpDelete("{studentId}")]
        public async Task<IActionResult> Delete(Guid studentId)
        {
            try
            {
                await this.studentService.DeleteStudentAsync(studentId);
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
