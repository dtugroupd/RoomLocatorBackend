using System;
using System.Collections.Generic;

namespace RoomLocator.Domain.ViewModels
{
    /// <summary>
    ///     <author>Thomas Lien Christensen, s165242</author>
    /// </summary>
    public class SurveyAnswerViewModel
    {
        public int Id { get; set; }
        public int SurveyId { get; set; }
        public DateTime TimeStamp { get; set; }
        public IEnumerable<QuestionAnswerViewModel> QuestionAnswers { get; set; }
    }
}
