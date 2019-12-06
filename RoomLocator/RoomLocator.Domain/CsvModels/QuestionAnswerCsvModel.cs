using System;
using System.Collections.Generic;
using System.Text;

namespace RoomLocator.Domain.CsvModels
{
    public class QuestionAnswerCsvModel
    {
        public string QuestionId { get; set; }
        public string Text { get; set; }
        public int Score { get; set; }
    }
}
