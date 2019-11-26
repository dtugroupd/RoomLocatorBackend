
namespace RoomLocator.Domain.InputModels
{
    /// <summary>
    ///     <author>Andreas Gøricke, s153804</author>
    /// </summary>
    public class EventInputModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Date { get; set; }
        public double DurationInHours { get; set; }
        public bool DurationApproximated { get; set; }
        public string Speakers { get; set; }
    }
}