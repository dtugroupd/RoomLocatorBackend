using Microsoft.AspNetCore.Mvc;
using RoomLocator.Domain.ViewModels;

namespace RoomLocator.Api.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class MazeMapController : ControllerBase
    {
        [HttpGet]
        public ActionResult<MazeMapCoordinatesViewModel> GetStaticPoint()
        {
            return Ok(new MazeMapCoordinatesViewModel { Latitude = 55.78498471097425, Longitude = 12.52026114549633 });
        }

    }
}