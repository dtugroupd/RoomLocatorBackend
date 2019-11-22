using System.Collections.Generic;

namespace RoomLocator.Domain.ViewModels
{
    public class UserViewModel
    {
        public string Id { get; set; }
        public string StudentId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName => $"{FirstName ?? ""} {LastName ?? ""}".Trim();
        public string ProfileImage { get; set; }
        public IEnumerable<string> Roles { get; set; }
    }
}
