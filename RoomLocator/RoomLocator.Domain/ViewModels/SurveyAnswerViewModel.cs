using System;
using System.Collections.Generic;

namespace RoomLocator.Domain.ViewModels
{
    /// <summary>
    ///     <author>Thomas Lien Christensen, s165242</author>
    ///     <author>Hadi Horani, s144885</author>
    /// </summary>
    public class SurveyAnswerViewModel
    {
        public string Id { get; set; }
        public string SurveyId { get; set; }
        public string Comment { get; set; }
        public DateTime TimeStamp { get; set; }
        public IEnumerable<QuestionAnswerViewModel> QuestionAnswers { get; set; }
    }
}
