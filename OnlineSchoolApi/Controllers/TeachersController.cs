using Microsoft.AspNetCore.Mvc;
using OnlineSchoolApi.InputModels;
using OnlineSchoolBusinessLogic.Interfaces;

namespace OnlineSchoolApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TeachersController : ControllerBase
    {
        private readonly ITeachersService teachersService;

        public TeachersController(ITeachersService teachersService)
        {
            this.teachersService = teachersService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] FilterInputModel filterInputModel)
        {
            var teachers = await this.teachersService.GetAllTeachersAsync(filterInputModel.Filter);
            return Ok(teachers);
        }
    }
}
