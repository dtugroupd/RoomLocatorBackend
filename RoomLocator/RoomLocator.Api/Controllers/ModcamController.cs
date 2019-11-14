using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RoomLocator.Data.Services;
using RoomLocator.Domain.ViewModels;

namespace RoomLocator.Api.Controllers
{
    /// <summary>
    /// 	<author>Amal Qasim, s132957</author>
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize]
    public class ModcamController :Controller
    {
        private readonly ModcamService _modcamService;

        public ModcamController(ModcamService modcamService)
        {
            _modcamService = modcamService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ModcamInstallationsViewModel>>> GetPeopleCounterInstallations()
        {
            var modcamInstallations = await _modcamService.GetPeopleCounterInstallations();
            return Ok(modcamInstallations);
        }
    }
}
