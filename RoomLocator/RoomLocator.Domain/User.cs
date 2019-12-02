using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using RoomLocator.Domain.Models;

namespace RoomLocator.Domain
{
    /// <summary>
    /// <author>Anders Wiberg Olsen, s165241</author>
    /// </summary>
    public class User
    {
        public string Id { get; set; }
        [Required] public string StudentId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string ProfileImage { get; set; }
        public virtual IList<UserRole> UserRoles { get; set; }
        public bool UserIsDeleted { get; set; }
        public virtual IEnumerable<Feedback> Feedbacks { get; set; }
        public virtual UserDisclaimer UserDisclaimer { get; set; }
    }
}
