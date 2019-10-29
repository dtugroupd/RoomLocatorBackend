using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RoomLocator.Domain.ViewModels
{
    public class SurveyCreateViewModel
    {
        public int SectionId { get; set; }

        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        public IEnumerable<QuestionCreateViewModel> Questions { get; set; }
    }
}
