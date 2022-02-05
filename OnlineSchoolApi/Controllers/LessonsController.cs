using Microsoft.AspNetCore.Mvc;
using OnlineSchoolBusinessLogic.Services;

namespace OnlineSchoolApi.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class LessonsController : ControllerBase
{
    private readonly ILessonService lessonService;

    public LessonsController(ILessonService lessonService)
    {
        this.lessonService = lessonService;
    }
}

