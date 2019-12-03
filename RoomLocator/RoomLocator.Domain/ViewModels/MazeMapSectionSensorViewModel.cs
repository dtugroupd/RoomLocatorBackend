using System;
using System.Collections.Generic;
using System.Text;
using RoomLocator.Domain.Models;

namespace RoomLocator.Domain.ViewModels
{
    public class MazeMapSectionSensorViewModel
    {

        public MazeMapSectionViewModel Section { get; set; } 

        public IEnumerable<SensorViewModel> Sensors { get; set; }

   
    }
}
