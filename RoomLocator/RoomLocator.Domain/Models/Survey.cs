using System.Collections.Generic;

namespace RoomLocator.Domain.Models
{
    public class Survey
    {
        public int Id { get; set; }
        public virtual IEnumerable<Question> Questions { get; set; }
        public virtual IEnumerable<MazeMapSection> MazeMapSections { get; set; }
        public virtual IEnumerable<SurveyAnswer> SurveyAnswers { get; set; }
    }
}
