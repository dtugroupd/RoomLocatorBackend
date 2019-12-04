using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using RoomLocator.Data.Services;
using Shared.Extentions;

namespace RoomLocator.Data.Hubs
{
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
            await Groups.AddToGroupAsync(connectionId, $"user/{studentId}");
            
            _logger.LogInformation("User \"{StudentId}\" connection to the websocket with connectionId {ConnectionId}'", studentId, connectionId);

            await base.OnConnectedAsync();
        }
    }
}
