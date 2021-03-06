﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RoomLocator.Domain.Models
{
    /// <summary>
    ///     <author>Thomas Lien Christensen, s165242</author>
    ///     <author>Hadi Horani, s144885</author>
    /// </summary>
    public class Survey
    {
        public string Id { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }
        public DateTime CreatedDate { get; set; }
        public virtual IEnumerable<Question> Questions { get; set; }
        public virtual IEnumerable<Section> Sections { get; set; }
        public virtual IEnumerable<SurveyAnswer> SurveyAnswers { get; set; }
    }
}
