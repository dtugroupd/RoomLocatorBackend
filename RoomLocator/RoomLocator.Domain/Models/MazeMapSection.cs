using RoomLocator.Domain.Enums;
using System.Collections.Generic;

namespace RoomLocator.Domain.Models
{
    /// <summary>
    ///     <author>Thomas Lien Christensen, s165242</author>
    /// </summary>
    public class MazeMapSection
    {
        public int Id { get; set; }
        public int ZLevel { get; set; }
        public int? SurveyId { get; set; }
        public LibrarySectionType Type { get; set; } 
        public virtual Survey Survey { get; set; }
        public virtual IEnumerable<Coordinates> Coordinates { get; set; }
    }
}
