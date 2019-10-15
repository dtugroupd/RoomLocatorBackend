using System;
using System.Collections.Generic;
using System.Text;

namespace RoomLocator.Domain.Models
{
    public class SurveyAnswer
    {
        public int Id { get; set; }
        public int SurveyId { get; set; }
        public IEnumerable<QuestionAnswer> QuestionAnswers { get; set; }
        public virtual Survey Survey { get; set; }
    }
}
