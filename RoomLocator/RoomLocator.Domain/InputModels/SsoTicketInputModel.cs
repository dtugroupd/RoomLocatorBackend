using System.ComponentModel.DataAnnotations;

namespace RoomLocator.Domain.InputModels
{
    public class SsoTicketInputModel
    {
        public string Service { get; set; } = "http://localhost:4200";
        [Required] public string Ticket { get; set; }
    }
}