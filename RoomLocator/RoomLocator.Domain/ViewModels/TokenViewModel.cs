using System.Collections.Generic;

namespace RoomLocator.Domain.ViewModels
{
    public class TokenViewModel
    {
        public string Token { get; set; }
        public UserViewModel User { get; set; }
        public IEnumerable<string> Roles { get; set; }
    }
}
