using System;
using System.Collections.Generic;
using System.Text;

namespace RoomLocator.Domain.Models
{
    public class Feedback
    {
        public string Id { get; set; }
        public bool Upvote { get; set; }
        public bool Downvote { get; set; }
    }
}
