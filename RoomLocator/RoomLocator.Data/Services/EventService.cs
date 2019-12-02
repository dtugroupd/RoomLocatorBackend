using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using RoomLocator.Data.Config;
using RoomLocator.Domain.InputModels;
using RoomLocator.Domain.Models;
using RoomLocator.Domain.ViewModels;
using Shared;

namespace RoomLocator.Data.Services
{
    /// <summary>
    ///     <author>Andreas Gøricke, s153804</author>
    ///     <author>Anders Wiberg Olsen, s165241</author>
    /// </summary>
    public class EventService : BaseService
    {
        public EventService(RoomLocatorContext context, IMapper mapper) : base(context, mapper) { }
        
        public async Task<EventViewModel> Get(string id)
        {
            return await _context.Events.ProjectTo<EventViewModel>(_mapper.ConfigurationProvider).FirstOrDefaultAsync(x => x.Id == id);
        }
        
        public async Task<EventViewModel> CreateEvent(EventInputModel inputModel)
        {
            var eventToCreate = _mapper.Map<Event>(inputModel);

            await _context.AddAsync(eventToCreate);
            await _context.SaveChangesAsync();

            return _mapper.Map<EventViewModel>(eventToCreate);
        }

        public async Task<EventViewModel> UpdateEvent(EventUpdateInputModel inputModel)
        {
            var currentEvent = await _context.Events.FirstOrDefaultAsync(x => x.Id == inputModel.Id);
            if (currentEvent == null) throw NotFoundException.NotExistsWithProperty<Event>(x => x.Id, inputModel.Id);

            _mapper.Map(inputModel, currentEvent);
            //currentEvent = _mapper.Map<Event>(inputModel);
            await _context.SaveChangesAsync();

            return _mapper.Map<EventViewModel>(currentEvent);
        }
        
        public async Task<IEnumerable<EventViewModel>> GetAll()
        {
            return await _context.Events.ProjectTo<EventViewModel>(_mapper.ConfigurationProvider).OrderBy(x => x.Date).ToListAsync();
        }
    }
}
