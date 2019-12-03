﻿using System;
using System.ComponentModel.DataAnnotations;

namespace RoomLocator.Domain.InputModels
{
    /// <summary>
    ///     <author>Thomas Lien Christensen, s165242</author>
    /// </summary>
    public class EventUpdateInputModel
    {
        [Required] public string Id { get; set; }
        [Required] public string Title { get; set; }
        [Required] public DateTime Date { get; set; }
        [Required] public bool DurationApproximated { get; set; }
        public string Description { get; set; }
        public double DurationInHours { get; set; }
        public string Speakers { get; set; }
    }
}