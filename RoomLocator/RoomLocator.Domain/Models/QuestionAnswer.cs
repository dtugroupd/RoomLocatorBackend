using System;
using System.Collections.Generic;
using System.Text;

namespace RoomLocator.Domain.Models
{
    public class QuestionAnswer
    {
        public int Id { get; set; }
        public int QuestionId { get; set; }
        public int SurveyAnswerId { get; set; }
        public string Text { get; set; }
        public int Score { get; set; }
    }
}
