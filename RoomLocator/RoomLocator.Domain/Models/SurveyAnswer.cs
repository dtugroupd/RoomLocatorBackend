using System;
using System.Collections.Generic;

namespace RoomLocator.Domain.Models
{
    /// <summary>
    ///     <author>Thomas Lien Christensen, s165242</author>
    /// </summary>
    public class SurveyAnswer
    {
        public int Id { get; set; }
        public int SurveyId { get; set; }
        public DateTime TimeStamp { get; set; }
        public IEnumerable<QuestionAnswer> QuestionAnswers { get; set; }
        public virtual Survey Survey { get; set; }
    }
}
