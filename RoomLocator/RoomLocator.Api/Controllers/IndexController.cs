using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace RoomLocator.Api.Controllers
{
    [Route("/")]
    public class IndexController : ControllerBase
    {
        [HttpGet]
        public ActionResult<string> Index()
        {
            return Ok("This pod is healthy");
        }
    }
}
