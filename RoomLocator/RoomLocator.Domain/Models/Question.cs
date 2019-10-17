using System.Collections.Generic;

namespace RoomLocator.Domain.Models
{
    public class Question
    {
        public int Id { get; set; }
        public virtual Survey Survey { get; set; }
        public virtual IEnumerable<QuestionAnswer> QuestionAnswers { get; set; }
        public int SurveyId { get; set; }
        public string Text { get; set; }
    }
}
