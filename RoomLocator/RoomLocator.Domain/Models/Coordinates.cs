namespace RoomLocator.Domain.Models
{
    /// <summary>
    ///     <author>Thomas Lien Christensen, s165242</author>
    /// </summary>
    public class Coordinates
    {
        public string Id { get; set; }
        public string SectionId { get; set; }
        public int Index { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public virtual Section MazeMapSection { get; set; }
    }
}
