using System;
using System.Collections.Generic;
using System.Text;

namespace RoomLocator.Domain.ViewModels
{
    /// <summary>
    ///     <author>Amal Qasim, s132957</author>
    ///     <author>Hadi Horani, s144885</author>
    /// </summary>
    public class SensorViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public DateTime TimeStamp { get; set; }
        public string Unit { get; set; }
        public double Value { get; set; }
        public string Status { get; set; }
    }
}
