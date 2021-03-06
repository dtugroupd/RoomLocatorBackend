﻿using System;

namespace RoomLocator.Domain.ViewModels
{
    /// <summary>
    ///     <author>Andreas Gøricke, s153804</author>
    ///     <author>Thomas Lien Christensen, s165242</author>
    /// </summary>
    public class EventViewModel
    {
        public string Id { get; set; }
        public DateTime Date { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public int ZLevel { get; set; }
        public string LocationId { get; set; }
        public string LocationName { get; set; }
        public double DurationInHours { get; set; }
        public bool DurationApproximated { get; set; }
        public string Speakers { get; set; }
    }
}