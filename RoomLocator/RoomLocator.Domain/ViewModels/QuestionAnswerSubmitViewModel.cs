using System;
using System.Collections.Generic;
using System.Text;

namespace RoomLocator.Domain.ViewModels
{
    public class QuestionAnswerSubmitViewModel
    {
        public int QuestionId { get; set; }
        public int Score { get; set; }
        public string Text { get; set; }
    }
}
