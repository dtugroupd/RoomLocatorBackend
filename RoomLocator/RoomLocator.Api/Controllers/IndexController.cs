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
        private string _healthMessage = "This pod is healthy"; 
        /// <summary>
        /// Health check, used for checking if the current pod is healthy
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<string> Index()
        {
            return _healthMessage;
        }
        
        [HttpGet("api")]
        public ActionResult<string> ApiIndex()
        {
            return _healthMessage;
        }
    }
}
