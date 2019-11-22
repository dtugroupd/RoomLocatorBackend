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
        public bool? Vote { get; set; }
        public string UserId { get; set; }
    }
}