using System;
using System.Collections.Generic;
using System.Text;

/// <summary>
/// 	<author>Amal Qasim, s132957, Gaurav Dangs:134692</author>
/// </summary>


namespace RoomLocator.Domain.InputModels
{
    public class SensorInputModel
    {
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
