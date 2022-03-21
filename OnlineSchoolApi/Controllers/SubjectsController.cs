using Microsoft.AspNetCore.Mvc;
using OnlineSchoolApi.InputModels;
using OnlineSchoolBusinessLogic.Interfaces;
using OnlineSchoolData.CustomExceptions;

namespace OnlineSchoolApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SubjectsController : ControllerBase
{
    private readonly ISubjectsService subjectService;

    public SubjectsController(ISubjectsService subjectService)
    {
        this.subjectService = subjectService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var subjects = await this.subjectService.GetAllSubjectsAsync();
        return Ok(subjects);
    }

    [HttpGet("[action]/{subjectId}")]
    public async Task<IActionResult> Get(Guid subjectId)
    {
        if (!ModelState.IsValid)
        {
            return this.BadRequest(ModelState);
        }

        try
        {
            var subject = await this.subjectService.GetSubjectAsync(subjectId);
            return this.Ok(subject);
        }
        catch (InvalidIdException ex)
        {
            ModelState.AddModelError("Id", ex.Message);
            return this.BadRequest(ModelState);
        }
    }

    [HttpGet("[action]")]
    public async Task<IActionResult> Get()
    {
        if (!ModelState.IsValid)
        {
            return this.BadRequest(ModelState);
        }

        var subjects = await this.subjectService.GetAllSubjectsAsync();
        if (subjects.Any())
        {
            return this.Ok(subjects);
        }
        return this.NoContent();
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> Add(SubjectInputModel subjectInputModel)
    {
        if (!ModelState.IsValid)
        {
            return this.BadRequest(ModelState);
        }

        try
        {
            var createdSubject = await this.subjectService.AddSubjectAsync(subjectInputModel.ToSubject());
            return this.CreatedAtAction(nameof(Get), new { subjectId = createdSubject.Id }, createdSubject);
        }
        catch (InvalidIdException ex)
        {
            ModelState.AddModelError("Id", ex.Message);
            return this.BadRequest(ModelState);
        }
    }

    [HttpPut("[action]/{subjectId}")]
    public async Task<IActionResult> Update(Guid subjectId, SubjectInputModel subjectInputModel)
    {
        if (!ModelState.IsValid)
        {
            return this.BadRequest(ModelState);
        }

        try
        {
            var updatedSubject = await this.subjectService.UpdateSubjectAsync(subjectId, subjectInputModel.ToSubject());
            return this.Ok(updatedSubject);
        }
        catch (InvalidIdException ex)
        {
            ModelState.AddModelError("Id", ex.Message);
            return this.BadRequest(ModelState);
        }
    }

    [HttpDelete("[action]/{subjectId}")]
    public async Task<IActionResult> Delete(Guid subjectId)
    {
        if (!ModelState.IsValid)
        {
            return this.BadRequest(ModelState);
        }

        try
        {
            await this.subjectService.DeleteSubjectAsync(subjectId);
            return this.Ok();
        }
        catch (InvalidIdException ex)
        {
            ModelState.AddModelError("Id", ex.Message);
            return this.BadRequest(ModelState);
        }
    }
}

