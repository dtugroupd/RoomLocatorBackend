using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using RoomLocator.Data.Config;
using RoomLocator.Domain.ViewModels;

namespace RoomLocator.Data.Services
{
    /// <summary>
    ///     <author>Thomas Lien Christensen, s165242</author>
    /// </summary>
    public class LocationService : BaseService
    {
        public LocationService(RoomLocatorContext context, IMapper mapper) : base(context, mapper) { }

        public async Task<IEnumerable<LocationSimpleViewModel>> GetLocations()
        {
            var locations = await _context.Locations.ProjectTo<LocationSimpleViewModel>(_mapper.ConfigurationProvider).ToListAsync();

            return locations;
        }

        public async Task<LocationViewModel> GetLocation(string id)
        {
            var location = await _context.Locations.ProjectTo<LocationViewModel>(_mapper.ConfigurationProvider).FirstOrDefaultAsync(x => x.Id == id);
            return location;
        }
    }
}
