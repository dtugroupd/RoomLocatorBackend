﻿using System;
using System.Collections.Generic;

namespace RoomLocator.Domain.ViewModels
{
    public class SurveyViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public IEnumerable<QuestionViewModel> Questions { get; set; }
        public IEnumerable<SurveyAnswerViewModel> SurveyAnswers { get; set; }
    }
}
