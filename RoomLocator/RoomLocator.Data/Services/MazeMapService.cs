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
    public class MazeMapService : BaseService
    {
        public MazeMapService(RoomLocatorContext context, IMapper mapper) : base(context, mapper) { }

        public async Task<IEnumerable<MazeMapSectionViewModel>> GetSections()
        {
            var sections = await _context.MazeMapSections.ProjectTo<MazeMapSectionViewModel>(_mapper.ConfigurationProvider).ToListAsync();
            return sections;
        }
    }
}
