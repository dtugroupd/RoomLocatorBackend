using System.ComponentModel.DataAnnotations;

namespace RoomLocator.Domain
{
    public class User
    {
        public string Id { get; set; }
        [Required] public string StudentId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Required] public string UserType { get; set; }
    }
}