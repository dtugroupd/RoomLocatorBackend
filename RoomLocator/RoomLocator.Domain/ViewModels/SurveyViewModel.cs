using System;
using System.Collections.Generic;

namespace RoomLocator.Domain.ViewModels
{
    /// <summary>
    ///     <author>Thomas Lien Christensen, s165242</author>
    ///     <author>Hadi Horani, s144885</author>
    /// </summary>
    public class SurveyViewModel
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public IEnumerable<QuestionViewModel> Questions { get; set; }
        public IEnumerable<SurveyAnswerViewModel> SurveyAnswers { get; set; }
    }
}
