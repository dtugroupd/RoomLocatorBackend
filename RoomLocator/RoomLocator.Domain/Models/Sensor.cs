using System.Collections.Generic;
using RoomLocator.Domain.Enums;

namespace RoomLocator.Domain.Models
{
    /// <summary>
    ///     <author>Andreas Gøricke, s153804</author>
    /// </summary>
    public class Sensor
    {
        public string Id { get; set; }
        public int ZLevel { get; set; }
        public SensorType Type { get; set; } 
        public SensorProvider Provider { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
    }
}