using Microsoft.AspNetCore.Mvc;

namespace OnlineSchoolBackend.Controllers
{
    public class HomeController : ControllerBase
    {
        [Route("api/")]
        public IActionResult Home()
        {
            return this.Ok(new { test = "working" });
        }
    }
}
