using System.Collections.Generic;

namespace RoomLocator.Domain.ViewModels
{
    public class UserViewModel
    {
        public string Id { get; set; }
        public string StudentId { get; set; }
        public IEnumerable<string> Roles { get; set; }
    }
}
