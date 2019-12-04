using RoomLocator.Domain.Models;

namespace RoomLocator.Domain.ViewModels
{
    public class RoleViewModel
    {
        public RoleViewModel() { }

        public RoleViewModel(UserRole userRole)
        {
            Name = userRole.Role?.Name;
            LocationId = userRole.LocationId;
            LocationName = userRole.Location?.Name;
        }

        public string Name { get; set; }
        public string LocationName { get; set; }
        public string LocationId { get; set; }
    }
}
