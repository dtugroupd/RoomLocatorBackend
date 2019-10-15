using System;
using System.Collections.Generic;
using System.Text;

namespace RoomLocator.Domain.Models
{
    public class MazeMapSection
    {

        public int Id { get; set; }
        public int ZLevel { get; set; }
        public virtual Survey Survey { get; set; }
        public double[][] Coordinates { get; set; }
    }
}
