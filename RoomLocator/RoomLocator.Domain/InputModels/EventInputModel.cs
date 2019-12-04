using System;
using System.ComponentModel.DataAnnotations;

namespace RoomLocator.Domain.InputModels
{
    /// <summary>
    ///     <author>Andreas Gøricke, s153804</author>
    ///     <author>Anders Wiberg Olsen, s165241</author>
    /// </summary>
    public class EventInputModel
    {
        [Required] public string Title { get; set; }
        [Required] public DateTime Date { get; set; }
        [Required] public string LocationId { get; set; }
        [Required] public double Longitude { get; set; }
        [Required] public double Latitude { get; set; }
        [Required] public int ZLevel { get; set; }
        public bool DurationApproximated { get; set; }
        public double DurationInHours { get; set; }
        public string Description { get; set; }
        public string Speakers { get; set; }
    }
}