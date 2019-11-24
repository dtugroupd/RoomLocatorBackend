using System;
using System.Collections.Generic;
using System.Text;

namespace RoomLocator.Domain.Models
{
    /// <summary>
    ///     <author>Hamed kadkhodaie, s083485</author>
    ///     <author>Thomas Lien Christensen, s165242</author>
    /// </summary>
    public class Feedback
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public bool? Vote { get; set; }
        public DateTime TimeStamp { get; set; } = DateTime.Now;
        public virtual User User { get; set; }
    }
}
