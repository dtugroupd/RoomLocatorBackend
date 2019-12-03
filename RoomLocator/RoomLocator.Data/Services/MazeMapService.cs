using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using RoomLocator.Data.Config;
using RoomLocator.Domain.Models;
using RoomLocator.Domain.ViewModels;

namespace RoomLocator.Data.Services
{
    /// <summary>
    ///     <author>Thomas Lien Christensen, s165242</author>
    /// </summary>
    public class MazeMapService : BaseService
    {
        public MazeMapService(RoomLocatorContext context, IMapper mapper) : base(context, mapper) { }

        public async Task<IEnumerable<MazeMapSectionViewModel>> GetSections()
        {
            var sections = await _context.MazeMapSections.ProjectTo<MazeMapSectionViewModel>(_mapper.ConfigurationProvider)
                .ToListAsync();
            foreach(var s in sections)
            {
                s.Coordinates = s.Coordinates.OrderBy(c => c.Index);
            }
            return sections;
        }
        
        
        
        public async Task<IEnumerable<MazeMapSectionSensorViewModel>> GetSectionsWithSensors()
        {
            var sectionsWithSensors = new List<MazeMapSectionSensor>();

            var sections = await _context.MazeMapSections.ToListAsync();
            var sensors= await _context.Sensors.ToListAsync();
            foreach (var section in sections)
            {
                section.Coordinates = section.Coordinates.OrderBy(c => c.Index);
                sectionsWithSensors.Add(new MazeMapSectionSensor(section, GetSensorsInSection(section, sensors)));
            }
            
            return sectionsWithSensors.Select(sws => new MazeMapSectionSensorViewModel
            {
                Sensors = _mapper.Map<IEnumerable<SensorViewModel>>(sws.Sensors), 
                Section = _mapper.Map<MazeMapSectionViewModel>(sws.Section)
            });
        }

        private IEnumerable<Sensor> GetSensorsInSection(MazeMapSection section, List<Sensor> sensors)
        {
            return sensors.Where(sensor => IsInSection(section.Coordinates.ToList(), sensor));
        }

        private static bool IsInSection(List<Coordinates> section, Sensor sensor)
        {
            var coef = section.Skip(1).Select((point, i) =>
                    (sensor.Latitude - section[i].Latitude) * (point.Longitude - section[i].Longitude)
                    - (sensor.Longitude - section[i].Longitude) * (point.Latitude - section[i].Latitude))
                .ToList();

            if (coef.Any(p => p == 0))
                return true;

            for (int i = 1; i < coef.Count(); i++)
            {
                if (coef[i] * coef[i - 1] < 0)
                    return false;
            }
            return true;
        }

    
        
        
        
        
    }
}
