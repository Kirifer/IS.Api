using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Is.Api.Controllers
{
    [ApiController]
    public class HomeController : ControllerBase
    {
        [HttpGet]
        [ApiExplorerSettings(IgnoreApi = true)]
        [Route("")]
        [Route("Home")]
        public IActionResult Index()
        {
            return Ok("Is Api 1.0");
        }
    }
}
