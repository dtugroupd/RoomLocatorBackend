using System;
using System.Collections.Generic;
using System.Text;

namespace RoomLocator.Domain.ViewModels
{
    public class SurveyViewModel
    {
        public int Id { get; set; }
        public IEnumerable<QuestionViewModel> Questions { get; set; }
        public IEnumerable<SurveyAnswerViewModel> SurveyAnswers { get; set; }
    }
}
