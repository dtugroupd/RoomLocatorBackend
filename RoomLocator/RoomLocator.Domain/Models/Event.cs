﻿using System;
using System.ComponentModel.Design;

namespace RoomLocator.Domain.Models
{
    /// <summary>
    ///     <author>Andreas Gøricke, s153804</author>
    /// </summary>
    public class Event
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