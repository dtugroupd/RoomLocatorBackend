using RoomLocator.Domain.Enums;
using RoomLocator.Domain.Models;
using System.Collections.Generic;

namespace RoomLocator.Domain.ViewModels
{
    public class MazeMapSectionViewModel
    {
        public int Id { get; set; }
        public int ZLevel { get; set; }
        public LibrarySectionType Type { get; set; }
        public SurveyViewModel Survey { get; set; }
        public IEnumerable<CoordinatesViewModel> Coordinates { get; set; }
    }
}
