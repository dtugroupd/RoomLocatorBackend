using System;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RoomLocator.Data.Config;
using RoomLocator.Domain;
using RoomLocator.Domain.InputModels;
using RoomLocator.Domain.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

/// <summary>
/// 	<author>Amal Qasim - s132957, Gaurav Dang s134692</author>
/// </summary>

namespace RoomLocator.Data.Services
{
    public class SensorService : BaseService
    {
        public SensorService(RoomLocatorContext context, IMapper mapper) : base(context, mapper) { }

        public async Task<IEnumerable<SensorViewModel>> Get()
        {
            var sensors = await _context.Sensors.ToListAsync();
            var sensorViewModels = _mapper.Map<SensorViewModel[]>(sensors);
            return sensorViewModels;
        }


        public async Task<SensorViewModel> Create(SensorInputModel input)
        {
            if (input == null)
            {
                throw new InvalidCastException("Input should not be null");
            }

            var sensors = _mapper.Map<Sensor>(input);
            await _context.Sensors.AddAsync(sensors);
            await _context.SaveChangesAsync();
            return _mapper.Map<SensorViewModel>(sensors);
        }

        public async Task<SensorViewModel> Update(string id, SensorInputModel input)
        {
            var sensors = await _context.Sensors.FirstOrDefaultAsync(x => x.Id == id);
            
            sensors.Name = input.Name;
            sensors.Type = input.Type;
            sensors.Longitude = input.Longitude;
            sensors.Latitude = input.Latitude;
            sensors.Unit = input.Unit;
            sensors.Value = input.Value;
            sensors.Status = input.Status;

         
            await _context.SaveChangesAsync();

            return _mapper.Map<SensorViewModel>(sensors);
        }

        public async Task Delete(string id)
        {
            var sensors = await _context.Sensors.FirstOrDefaultAsync(x => x.Id == id);

            if (sensors == null) return;

            _context.Sensors.Remove(sensors);
            await _context.SaveChangesAsync();
        }
    }
}
