using Microsoft.AspNetCore.Mvc;
using OnlineSchoolApi.RequestModels;
using OnlineSchoolApi.ResponseModels;
using OnlineSchoolBusinessLogic.Services;

namespace OnlineSchoolApi.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class SubjectsController : ControllerBase
{
    private readonly ISubjectService subjectService;

    public SubjectsController(ISubjectService subjectService)
    {
        this.subjectService = subjectService;
    }

    [HttpGet("{subjectId}")]
    public async Task<IActionResult> Get(Guid subjectId)
    {
        try
        {
            var subject = await this.subjectService.GetSubjectAsync(subjectId);
            return this.Ok(subject);
        }
        catch (ArgumentNullException)
        {
            return this.BadRequest(new ErrorResponseModel { ErrorMessage = $"Invalid Id: {subjectId}" });
        }
        catch (Exception)
        {
            return new StatusCodeResult(500);
        }
    }

    [HttpPost]
    public async Task<IActionResult> Add(SubjectInputModel subjectInputModel)
    {
        try
        {
            var createdSubject = await this.subjectService.AddSubjectAsync(subjectInputModel.ToSubject());
            return this.CreatedAtAction(nameof(Get), new { subjectId = createdSubject.Id}, createdSubject);
        }
        catch (ArgumentNullException)
        {
            return this.BadRequest(new ErrorResponseModel { ErrorMessage = "Invalid data was provided" });
        }
        catch (Exception)
        {
            return new StatusCodeResult(500);
        }
    }

}

