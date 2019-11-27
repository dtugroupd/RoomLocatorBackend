using RoomLocator.Domain.Enums;
using RoomLocator.Domain.Models;
using System.Collections.Generic;

namespace RoomLocator.Domain.ViewModels
{
    /// <summary>
    ///     <author>Thomas Lien Christensen, s165242</author>
    /// </summary>
    public class SectionViewModel
    {
        public string Id { get; set; }
        public int ZLevel { get; set; }
        public SectionType Type { get; set; }
        public SurveyViewModel Survey { get; set; }
        public IEnumerable<CoordinatesViewModel> Coordinates { get; set; }
    }
}
