using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RoomLocator.Domain.Models
{
    public class Survey
    {
        public int Id { get; set; }
        public string Comment { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }
        public DateTime CreatedDate { get; set; }
        public virtual IEnumerable<Question> Questions { get; set; }
        public virtual IEnumerable<MazeMapSection> MazeMapSections { get; set; }
        public virtual IEnumerable<SurveyAnswer> SurveyAnswers { get; set; }
    }
}
