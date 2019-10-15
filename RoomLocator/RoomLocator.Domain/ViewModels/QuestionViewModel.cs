using System.Collections.Generic;

namespace RoomLocator.Domain.ViewModels
{
    public class QuestionViewModel
    {
        public int Id { get; set; }
        public int SurveyId { get; set; }
        public IEnumerable<QuestionAnswerViewModel> QuestionAnswers { get; set; }
        public string Text { get; set; }
    }
}
