using System;

namespace RoomLocator.Domain.ViewModels
{
    public class EventViewModel
    {
        public string Id { get; set; }
        public DateTime Date { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public double DurationInHours { get; set; }
        public bool DurationApproximated { get; set; }
        public string Speakers { get; set; }
    }
}