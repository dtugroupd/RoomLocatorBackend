﻿using System;
using System.Collections.Generic;

namespace RoomLocator.Domain.Models
{
    /// <summary>
    ///     <author>Thomas Lien Christensen, s165242</author>
    ///     <author>Hadi Horani, s144885</author>
    /// </summary>
    public class SurveyAnswer
    {
        public string Id { get; set; }
        public string SurveyId { get; set; }
        public string Comment { get; set; }
        public DateTime TimeStamp { get; set; }
        public virtual IEnumerable<QuestionAnswer> QuestionAnswers { get; set; }
        public virtual Survey Survey { get; set; }
    }
}
