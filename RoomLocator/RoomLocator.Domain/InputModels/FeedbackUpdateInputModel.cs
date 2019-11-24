using System;
using System.Collections.Generic;
using System.Text;

namespace RoomLocator.Domain.InputModels
{
    /// <summary>
    ///     <author>Hamed Kadkhodaie, s083485</author>
    /// </summary>
    public class FeedbackUpdateInputModel
    {
        public string Id { get; set; }
        public bool? Vote { get; set; }
    }
}