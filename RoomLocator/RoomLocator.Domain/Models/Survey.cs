using System;
using System.Collections.Generic;
using System.Text;

namespace RoomLocator.Domain.Models
{
    public class Survey
    {
        public int Id { get; set; }
        public int SectionId { get; set; }
        public IEnumerable<Question> Questions { get; set; }
        public virtual MazeMapSection MazeMapSection { get; set; }
    }
}
