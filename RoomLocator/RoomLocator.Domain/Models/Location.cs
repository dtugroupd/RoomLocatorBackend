using System;
using System.Collections.Generic;
using System.Text;

namespace RoomLocator.Domain.Models
{
    /// <summary>
    ///     <author>Thomas Lien Christensen, s165242</author>
    /// </summary>
    public class Location
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public double Zoom { get; set; }
        public virtual IEnumerable<Section> Sections { get; set; }
        public virtual IEnumerable<Feedback> Feedbacks { get; set; }
        public virtual IEnumerable<Event> Events { get; set; }
    }
}
