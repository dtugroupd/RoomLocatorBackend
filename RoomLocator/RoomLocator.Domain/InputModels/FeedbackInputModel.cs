using System;
using System.Collections.Generic;
using System.Text;

namespace RoomLocator.Domain.InputModels
{
    /// <summary>
    ///     <author>Hamed Kadkhodaie, s083485</author>
    /// </summary>
    public class FeedbackInputModel
    {
        public bool upvote { get; set; }
        public bool downvote { get; set; }
        
    }
}