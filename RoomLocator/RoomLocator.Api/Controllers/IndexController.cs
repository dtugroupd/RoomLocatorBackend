using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace RoomLocator.Api.Controllers
{
    /// <summary>
    /// Root the domain
    /// </summary>
    [Route("/")]
    public class IndexController : ControllerBase
    {
        /// <summary>
        /// Health check, used for checking if the current pod is healthy
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<string> Index()
        {
            return Ok("This pod is healthy");
        }
    }
}
