﻿using System.Collections.Generic;

namespace RoomLocator.Domain.Models
{
    public class MazeMapSection
    {
        public int Id { get; set; }
        public int ZLevel { get; set; }
        public int? SurveyId { get; set; }
        public virtual Survey Survey { get; set; }
        public virtual IEnumerable<Coordinates> Coordinates { get; set; }
    }
}