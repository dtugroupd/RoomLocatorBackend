﻿using RoomLocator.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RoomLocator.Domain.ViewModels
{
    public class LocationViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public double Zoom { get; set; }
        public IEnumerable<SectionViewModel> Sections { get; set; }
        public IEnumerable<EventViewModel> Events { get; set; }
        public IEnumerable<Coordinates> Coordinates { get; set; }
    }
}
