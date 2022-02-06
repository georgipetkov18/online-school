using Microsoft.AspNetCore.Mvc;
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
}

