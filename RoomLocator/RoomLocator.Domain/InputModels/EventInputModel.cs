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
        [Required] public string Description { get; set; }
        [Required] public string Date { get; set; }
        [Required] public double DurationInHours { get; set; }
        [Required] public bool DurationApproximated { get; set; }
        public string Speakers { get; set; }
    }
}