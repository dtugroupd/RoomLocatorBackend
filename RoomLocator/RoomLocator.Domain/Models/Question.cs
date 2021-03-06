﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RoomLocator.Domain.Models
{
    /// <summary>
    ///     <author>Thomas Lien Christensen, s165242</author>
    /// </summary>
    public class Question
    {
        public string Id { get; set; }
        [Required] public string SurveyId { get; set; }
        public string Text { get; set; }
        public virtual Survey Survey { get; set; }
        public virtual IEnumerable<QuestionAnswer> QuestionAnswers { get; set; }
    }
}
