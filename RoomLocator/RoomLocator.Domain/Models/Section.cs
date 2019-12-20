using RoomLocator.Domain.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RoomLocator.Domain.Models
{
    /// <summary>
    ///     <author>Thomas Lien Christensen, s165242</author>
    /// </summary>
    public class Section
    {
        public string Id { get; set; }
        [Required] public string LocationId { get; set; }
        public string SurveyId { get; set; }
        public int ZLevel { get; set; }
        public SectionType Type { get; set; } 
        public virtual Survey Survey { get; set; }
        public virtual Location Location { get; set; }
        public virtual IEnumerable<Coordinates> Coordinates { get; set; }
    }
}
