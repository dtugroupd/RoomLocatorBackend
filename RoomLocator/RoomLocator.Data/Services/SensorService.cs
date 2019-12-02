using System;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RoomLocator.Data.Config;
using RoomLocator.Domain;
using RoomLocator.Domain.InputModels;
using RoomLocator.Domain.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RoomLocator.Data.Services
{
    /// <summary>
    /// 	<author>Amal Qasim - s132957, Gaurav Dang s134692</author>
    /// </summary>
    public class SensorService : BaseService
    {
        public SensorService(RoomLocatorContext context, IMapper mapper) : base(context, mapper) { }

        public async Task<IEnumerable<SensorViewModel>> Get()
        {
            var sensors = await _context.Sensors.ToListAsync();
            var sensorViewModels = _mapper.Map<SensorViewModel[]>(sensors);
            return sensorViewModels;
        }
        
    }
}
