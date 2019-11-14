namespace RoomLocator.Domain.ViewModels
{
    /// <summary>
    ///    <author>Andreas Gøricke, s153804</author>
    /// </summary>
    public class ScadadataViewModel
    {
        public string PointId { get; set; }
        public string Timestamp { get; set; }
        public string Type { get; set; }
        public string Unit { get; set; }
        public float Value { get; set; }
        public string Status { get; set; }
    }
}