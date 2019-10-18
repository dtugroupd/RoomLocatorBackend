using System;
using System.Collections.Generic;
using System.Text;

namespace RoomLocator.Domain.ViewModels
{
    public class SurveyCreateViewModel
    {
        public int SectionId { get; set; }
        public IEnumerable<QuestionViewModel> Questions { get; set; }

    }
}
