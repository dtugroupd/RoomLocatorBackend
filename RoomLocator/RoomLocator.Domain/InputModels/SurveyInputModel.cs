using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RoomLocator.Domain.InputModels
{
    /// <summary>
    ///     <author>Thomas Lien Christensen, s165242</author>
    /// </summary>
    public class SurveyInputModel
    {
        public int SectionId { get; set; }

        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        public IEnumerable<QuestionInputModel> Questions { get; set; }
    }
}
