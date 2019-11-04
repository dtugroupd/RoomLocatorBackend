using System;
using System.Collections.Generic;
using System.Text;

namespace RoomLocator.Domain.CsvModels
{
    public class SurveyAnswerCsvModel
    {
        public int SurveyId { get; set; }
        public string Comment { get; set; }
        public DateTime TimeStamp { get; set; }
        public List<QuestionAnswerCsvModel> QuestionAnswers { get; set; }
    }
}
