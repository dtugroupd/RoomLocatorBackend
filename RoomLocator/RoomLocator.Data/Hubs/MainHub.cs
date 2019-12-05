using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using RoomLocator.Data.Services;
using Shared.Extentions;

namespace RoomLocator.Data.Hubs
{
    /// <summary>
    ///     <author>Anders Wiberg Olsen, s165241</author>
    /// </summary>
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class MainHub : Hub
    {
        private readonly ILogger<MainHub> _logger;
        private readonly UserService _userService;

        public MainHub(ILogger<MainHub> logger, UserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        public override async Task OnConnectedAsync()
        {
            var connectionId = Context.ConnectionId;
            var studentId = Context.User.StudentId();
            var user = await _userService.GetByStudentId(studentId);

            await Groups.AddToGroupAsync(connectionId, studentId);

            if (user.IsGeneralAdmin)
            {
                await Groups.AddToGroupAsync(connectionId, "admin");
            }
            
            _logger.LogInformation("User \"{StudentId}\" connection to the websocket with connectionId {ConnectionId}'", studentId, connectionId);

            await base.OnConnectedAsync();
        }
    }
}
