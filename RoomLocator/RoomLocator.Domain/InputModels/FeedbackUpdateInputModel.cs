using System;
using System.Collections.Generic;
using System.Text;

namespace RoomLocator.Domain.InputModels
{
    /// <summary>
    ///     <author>Hamed kadkhodaie, s083485</author>
    ///     <author>Thomas Lien Christensen, s165242</author>
    /// </summary>
    public class FeedbackUpdateInputModel
    {
        public string Id { get; set; }
        public bool? Vote { get; set; }
    }
}