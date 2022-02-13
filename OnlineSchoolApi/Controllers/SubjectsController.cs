using Microsoft.AspNetCore.Mvc;
using OnlineSchoolApi.RequestModels;
using OnlineSchoolApi.ResponseModels;
using OnlineSchoolBusinessLogic.Services;
using OnlineSchoolData.CustomExceptions;

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
        catch (CustomException ex)
        {
            return this.BadRequest(new ErrorResponseModel
            {
                ErrorMessage = ex.Message
            });
        }
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var subjects = await this.subjectService.GetAllSubjectsAsync();
        if (subjects.Any())
        {
            return this.Ok(subjects);
        }
        return this.NoContent();
    }

    [HttpPost]
    public async Task<IActionResult> Add(SubjectInputModel subjectInputModel)
    {
        try
        {
            var createdSubject = await this.subjectService.AddSubjectAsync(subjectInputModel.ToSubject());
            return this.CreatedAtAction(nameof(Get), new { subjectId = createdSubject.Id }, createdSubject);
        }
        catch (CustomException ex)
        {
            return this.BadRequest(new ErrorResponseModel
            {
                ErrorMessage = ex.Message
            });
        }
    }

    [HttpPut("{subjectId}")]
    public async Task<IActionResult> Update(Guid subjectId, SubjectInputModel subjectInputModel)
    {
        try
        {
            var updatedSubject = await this.subjectService.UpdateSubjectAsync(subjectId, subjectInputModel.ToSubject());
            return this.Ok(updatedSubject);
        }
        catch (CustomException ex)
        {
            return this.BadRequest(new ErrorResponseModel
            {
                ErrorMessage = ex.Message
            });
        }
    }

    [HttpDelete("{subjectId}")]
    public async Task<IActionResult> Delete(Guid subjectId)
    {
        try
        {
            await this.subjectService.DeleteSubjectAsync(subjectId);
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

