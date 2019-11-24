using System;
using System.Collections.Generic;

namespace RoomLocator.Domain.ViewModels
{
    /// <summary>
    ///     <author> Hamed Kadkhodaie, s083485</author>     
    /// </summary>
    public class FeedbackViewModel
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public bool? Vote { get; set; }
        public DateTime TimeStamp { get; set; }

    }
}