using System;
using System.Collections.Generic;
using System.Text;

namespace RoomLocator.Domain.Models
{
    public class Question
    {

        public int Id { get; set; }
        public virtual QuestionAnswer QuestionAnswer { get; set; }
        public int SurveyId { get; set; }
        public string Text { get; set; }
    }
}
