using System;
using System.Collections.Generic;
using System.Text;

namespace RoomLocator.Domain.Models
{
    public class Feedback
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public bool? Vote { get; set; }
        public DateTime TimeStamp { get; set; } = DateTime.Now;
    }
}
