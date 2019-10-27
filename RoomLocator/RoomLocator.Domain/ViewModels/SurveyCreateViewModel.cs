using System;
using System.Collections.Generic;
using System.Text;

namespace RoomLocator.Domain.ViewModels
{
    public class SurveyCreateViewModel
    {
        public int SectionId { get; set; }
        public string Title { get; set; }
        public IEnumerable<QuestionCreateViewModel> Questions { get; set; }
        public DateTime DateTimeOfSurveyCreation { get; set; }

    }
}
