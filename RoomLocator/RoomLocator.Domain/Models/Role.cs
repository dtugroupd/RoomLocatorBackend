using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RoomLocator.Domain.Models
{
    public class Role
    {
        public string Id { get; set; }
        [Required] public string Name { get; set; }
        public virtual IList<UserRole> UserRoles { get; set; }
    }
}
