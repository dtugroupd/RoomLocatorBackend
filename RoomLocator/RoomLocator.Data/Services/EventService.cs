using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using RoomLocator.Data.Config;
using RoomLocator.Data.Hubs.Services;
using RoomLocator.Domain.InputModels;
using RoomLocator.Domain.Models;
using RoomLocator.Domain.ViewModels;
using Shared;
using Shared.Extentions;

namespace RoomLocator.Data.Services
{
    /// <summary>
    ///     <author>Andreas Gøricke, s153804</author>
    ///     <author>Anders Wiberg Olsen, s165241</author>
    /// </summary>
    public class EventService : BaseService
    {
        private readonly UserService _userService;
        private readonly TokenService _tokenService;
        private readonly EventServiceHub _eventServiceHub;

        public EventService(RoomLocatorContext context, IMapper mapper, UserService userService, TokenService tokenService, EventServiceHub eventServiceHub) : base(context, mapper)
        {
            _userService = userService;
            _tokenService = tokenService;
            _eventServiceHub = eventServiceHub;

        }
        public async Task<EventViewModel> Get(string id)
        {
            return await _context.Events.ProjectTo<EventViewModel>(_mapper.ConfigurationProvider).FirstOrDefaultAsync(x => x.Id == id);
        }
        
        public async Task<EventViewModel> CreateEvent(EventInputModel inputModel)
        {
            var eventToCreate = _mapper.Map<Event>(inputModel);

            await _context.AddAsync(eventToCreate);
            await _context.SaveChangesAsync();
            
            var @event = _mapper.Map<EventViewModel>(eventToCreate);
            @event.LocationName = await _context.Locations
                .Where(x => x.Id == inputModel.LocationId)
                .Select(x => x.Name)
                .FirstOrDefaultAsync();

            await _eventServiceHub.CreateEvent(@event);

            return @event;
        }

        public async Task<EventViewModel> UpdateEvent(EventUpdateInputModel inputModel)
        {
            var currentEvent = await _context
                .Events
                .Include(e => e.Location)
                .FirstOrDefaultAsync(x => x.Id == inputModel.Id);
            if (currentEvent == null) throw NotFoundException.NotExistsWithProperty<Event>(x => x.Id, inputModel.Id);

            _mapper.Map(inputModel, currentEvent);
            await _context.SaveChangesAsync();

            await _eventServiceHub.UpdateEvents();

            return _mapper.Map<EventViewModel>(currentEvent);
        }

        public async Task DeleteEvent(string id)
        {
            var eventToDelete = await _context.Events.FirstOrDefaultAsync(x => x.Id == id);

            await _userService.EnsureAdmin(_tokenService.User.StudentId(), eventToDelete.LocationId);
            _context.Remove(eventToDelete);

            await _context.SaveChangesAsync();
        }
        
        public async Task<IEnumerable<EventViewModel>> GetAll()
        {
            return await _context.Events.ProjectTo<EventViewModel>(_mapper.ConfigurationProvider).OrderBy(x => x.Date).ToListAsync();
        }
    }
}
