using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using RoomLocator.Domain.ViewModels;

namespace RoomLocator.Data.Hubs.Services
{
    public class EventServiceHub
    {
        private readonly IHubContext<MainHub> _hub;
        private readonly ILogger<EventServiceHub> _logger;

        public EventServiceHub(IHubContext<MainHub> hub, ILogger<EventServiceHub> logger)
        {
            _hub = hub;
            _logger = logger;
        }

        public async Task UpdateEvents()
        {
            _logger.LogError("Updating everyone's event lists");
            await _hub.Clients.All.SendAsync("events-changed");
        }

        public async Task CreateEvent(EventViewModel @event)
        {
            await UpdateEvents();
            await _hub.Clients.All.SendAsync("new-event", @event);
        }
    }
}
