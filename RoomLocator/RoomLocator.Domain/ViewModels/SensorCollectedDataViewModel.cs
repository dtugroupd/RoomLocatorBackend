using System.Collections.Generic;

namespace RoomLocator.Domain.ViewModels
{
    /// <summary>
    ///     <author>Amal Qasim, s132957</author>
    /// </summary>
    public class SensorCollectedDataViewModel
    {
        public IEnumerable<ApieceSensorData> SensorsData{ get; set; }
        
    }

    public class ApieceSensorData
    {
        public string SectionId { get; set; }
        public IEnumerable<string> People { get; set; }
    } 
}
