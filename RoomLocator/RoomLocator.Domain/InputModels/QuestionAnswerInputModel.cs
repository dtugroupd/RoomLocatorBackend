using System;
using System.Collections.Generic;
using System.Text;

namespace RoomLocator.Domain.InputModels
{
    /// <summary>
    ///     <author>Thomas Lien Christensen, s165242</author>
    /// </summary>
    public class QuestionAnswerInputModel
    {
        public int QuestionId { get; set; }
        public int Score { get; set; }
        public string Text { get; set; }
    }
}
