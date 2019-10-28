using System;
using System.Collections.Generic;
using System.Text;

namespace RoomLocator.Domain.ViewModels
{
    public class SurveyAnswerSubmitViewModel
    {
        public int SurveyId { get; set; }
        public IEnumerable<QuestionAnswerSubmitViewModel> QuestionAnswers { get; set; }
    }
}
