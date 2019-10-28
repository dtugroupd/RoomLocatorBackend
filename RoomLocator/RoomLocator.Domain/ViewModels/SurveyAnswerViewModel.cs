using System;
using System.Collections.Generic;

namespace RoomLocator.Domain.ViewModels
{
    public class SurveyAnswerViewModel
    {
        public int Id { get; set; }
        public int SurveyId { get; set; }
        public DateTime TimeStamp { get; set; }
        public IEnumerable<QuestionAnswerViewModel> QuestionAnswers { get; set; }
    }
}
