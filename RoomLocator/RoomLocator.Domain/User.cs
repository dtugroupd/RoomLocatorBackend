using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using RoomLocator.Domain.Models;

namespace RoomLocator.Domain
{
    public class User
    {
        public string Id { get; set; }
        [Required] public string StudentId { get; set; }
        public virtual IList<UserRole> UserRoles { get; set; }
    }
}