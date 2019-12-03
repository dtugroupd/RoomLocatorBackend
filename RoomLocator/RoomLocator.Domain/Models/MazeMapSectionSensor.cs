using RoomLocator.Domain.Enums;
using System.Collections.Generic;

namespace RoomLocator.Domain.Models
{
    /// <summary>
    ///     <author>Andreas Gøricke, s153804</author>
    /// </summary>
    public class MazeMapSectionSensor
    {
        public MazeMapSectionSensor()
        {
        }

        public MazeMapSectionSensor(MazeMapSection section, IEnumerable<Sensor> sensors)
        {
            Section = section;
            Sensors = sensors;
        }
        
        public virtual MazeMapSection Section { get; set; }
        
        public virtual IEnumerable<Sensor> Sensors { get; set; }
        
    }
}
