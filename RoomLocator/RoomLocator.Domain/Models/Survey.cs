using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RoomLocator.Domain.Models
{
    public class Survey
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public virtual IEnumerable<Question> Questions { get; set; }
        public virtual IEnumerable<MazeMapSection> MazeMapSections { get; set; }
        public virtual IEnumerable<SurveyAnswer> SurveyAnswers { get; set; }
    }
}
