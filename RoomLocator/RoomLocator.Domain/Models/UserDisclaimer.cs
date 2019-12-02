using System.ComponentModel.DataAnnotations;

namespace RoomLocator.Domain.Models
{
    public class UserDisclaimer
    {
        public UserDisclaimer() { }
        public UserDisclaimer(string userId, bool accepted)
        {
            UserId = userId;
            HasAcceptedDisclaimer = accepted;
        }

        public string Id { get; set; }
        [Required] public string UserId { get; set; }
        public virtual User User { get; set; }
        [Required] public bool HasAcceptedDisclaimer { get; set; }
    }
}
