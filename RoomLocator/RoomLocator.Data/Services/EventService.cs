using System;
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
    /// </summary>
    public class EventService : BaseService
    {
        public EventService(RoomLocatorContext context, IMapper mapper) : base(context, mapper)
        {
        }
        
        public async Task<EventViewModel> Get(string id)
        {
            return await _context.Events.ProjectTo<EventViewModel>(_mapper.ConfigurationProvider).FirstOrDefaultAsync(x => x.Id == id);
        }
        
        public async Task<EventViewModel> CreateEvent(EventInputModel viewModel)
        {
            if (viewModel == null)
                throw new InvalidRequestException("Invalid request", "Can not create event as event is null.");

            if (viewModel.Title == null)
                throw new InvalidRequestException("Invalid request", "Can not create event without a title.");
            
            if (viewModel.Date == null)
                throw new InvalidRequestException("Invalid request", "Can not create event without a date.");
            
            if (viewModel.Description == null)
                throw new InvalidRequestException("Invalid request", "Can not create event without a description.");
            
            
            var eventToCreate = new Event
            {
                Title = viewModel.Title,
                Description = viewModel.Description,
                Date = DateTime.Parse(viewModel.Date),
                Speakers = viewModel.Speakers,
                DurationInHours = viewModel.DurationInHours,
                DurationApproximated = viewModel.DurationApproximated
            };

            await _context.AddAsync(eventToCreate);
            await _context.SaveChangesAsync();

            return _mapper.Map<EventViewModel>(eventToCreate);
        }
        
        public async Task<IEnumerable<EventViewModel>> GetAll()
        {
            return await _context.Events.ProjectTo<EventViewModel>(_mapper.ConfigurationProvider).OrderBy(x => x.Date).ToListAsync();
        }
        
    }
}