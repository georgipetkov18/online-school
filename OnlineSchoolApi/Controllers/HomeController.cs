using Microsoft.AspNetCore.Mvc;

namespace OnlineSchoolApi.Controllers
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
