using System.ComponentModel.DataAnnotations.Schema;

namespace RoomLocator.Domain.ViewModels
{
    public class UserViewModel
    {
        public string Id { get; set; }
        public string StudentId { get; set; }
    }
}