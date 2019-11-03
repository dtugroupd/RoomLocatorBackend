using System;
using System.Collections.Generic;
using System.Text;

namespace RoomLocator.Domain.InputModels
{
    /// <summary>
    ///     <author>Thomas Lien Christensen, s165242</author>
    /// </summary>
    public class SurveyAnswerInputModel
    {
        public int SurveyId { get; set; }
        public string Comment { get; set; }
        public IEnumerable<QuestionAnswerInputModel> QuestionAnswers { get; set; }
    }
}
