using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineSchoolBusinessLogic;
using OnlineSchoolBusinessLogic.Common;
using OnlineSchoolBusinessLogic.InputModels;
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
    public async Task<IActionResult> GetAll([FromQuery] FilterInputModel filterInputModel)
    {
        var subjects = await this.subjectService.GetAllSubjectsAsync(filterInputModel.Filter ?? string.Empty);
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

    [HttpPost("[action]")]
    [Authorize(Policy = Policies.RequireAdministratorRole)]
    public async Task<IActionResult> Add(SubjectInputModel subjectInputModel)
    {
        if (!ModelState.IsValid)
        {
            return this.BadRequest(ModelState);
        }

        var createdSubject = await this.subjectService.AddSubjectAsync(subjectInputModel.ToSubject());
        return this.CreatedAtAction(nameof(Get), new { subjectId = createdSubject.Id }, createdSubject);
    }

    [HttpPut("[action]/{subjectId}")]
    [Authorize(Policy = Policies.RequireAdministratorRole)]
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
    [Authorize(Policy = Policies.RequireAdministratorRole)]
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

