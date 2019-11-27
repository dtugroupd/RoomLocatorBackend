using System.ComponentModel.DataAnnotations;

namespace RoomLocator.Domain.InputModels
{
    public class CnAuthInputModel
    {
        public LoginModel LoginModel { get; set; }
    }

    public class LoginModel
    {
        [Required] public string Username { get; set; }
        [Required] public string Password { get; set; }
    }
}
