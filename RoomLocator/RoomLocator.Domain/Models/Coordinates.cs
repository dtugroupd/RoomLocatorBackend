namespace RoomLocator.Domain.Models
{
    public class Coordinates
    {
        public int Id { get; set; }
        public int MazeMapSectionId { get; set; }
        public int Index { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public virtual MazeMapSection MazeMapSection { get; set; }
    }
}
