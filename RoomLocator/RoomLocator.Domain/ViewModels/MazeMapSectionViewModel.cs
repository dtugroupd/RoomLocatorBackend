using RoomLocator.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RoomLocator.Domain.ViewModels
{
    public class MazeMapSectionViewModel
    {
        public int Id { get; set; }
        public int ZLevel { get; set; }
        public SurveyViewModel Survey { get; set; }
        public IEnumerable<CoordinatesViewModel> Coordinates { get; set; }
    }
}
