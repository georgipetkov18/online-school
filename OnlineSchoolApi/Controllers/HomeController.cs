using Microsoft.AspNetCore.Mvc;

namespace OnlineSchoolApi.Controllers;

[ApiController]
public class HomeController : ControllerBase
{
    [HttpGet("api/")]
    public IActionResult Home()
    {
        return this.Ok(new { test = "working" });
    }
}

