using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using RoomLocator.Domain.ViewModels;

namespace RoomLocator.Data.Hubs.Services
{
    /// <summary>
    ///     <author>Anders Wiberg Olsen, s165241</author>
    /// </summary>
    public class UserServiceHub
    {
        private readonly IHubContext<MainHub> _hub;
        private readonly ILogger<UserServiceHub> _logger;

        public UserServiceHub(IHubContext<MainHub> hub, ILogger<UserServiceHub> logger)
        {
            _hub = hub;
            _logger = logger;
        }

        public async Task UpdateUserRole(UserViewModel updatedUser, Task<IEnumerable<UserViewModel>> usersTask)
        {
            var updateListTask = UpdateUserList(usersTask);
            _logger.LogInformation("Updating user model for '{StudentId}'", updatedUser.StudentId);
            await _hub.Clients.Groups(updatedUser.StudentId).SendAsync("updated-user", updatedUser);
            await updateListTask;
        }

        public async Task UpdateUserList(Task<IEnumerable<UserViewModel>> usersTask)
        {
            var users = await usersTask;
            _logger.LogInformation("Updating user list with {UserCount} users.", users.Count());
            await _hub.Clients.All.SendAsync("admin-user-update", users);
        }

        public async Task DeleteUser(string studentId)
        {
            await _hub.Clients.Groups(studentId).SendAsync("deleted-user", studentId);
            await _hub.Clients.Groups("admin").SendAsync("deleted-user", studentId);
        }
    }
}
