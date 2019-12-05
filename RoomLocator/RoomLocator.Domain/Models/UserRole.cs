using System.ComponentModel.DataAnnotations;

namespace RoomLocator.Domain.Models
{
    public class UserRole
    {
        public string Id { get; set; }
        [Required] public string UserId { get; set; }
        [Required] public string RoleId { get; set; }
        public string LocationId { get; set; }
        public virtual User User { get; set; }
        public virtual Role Role { get; set; }
        public virtual Location Location { get; set; }
    }
}