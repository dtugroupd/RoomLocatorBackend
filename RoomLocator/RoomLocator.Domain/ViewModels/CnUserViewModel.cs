namespace RoomLocator.Domain.ViewModels
{
    public class CnUserViewModel
    {
        public string UserName { get; set; }
        public string UserId { get; set; }
        public string GivenName { get; set; }
        public string FamilyName { get; set; }
        public string Email { get; set; }

        public string FullName => $"{GivenName} {FamilyName}";
        public string ProfileImage { get; set; }
    }
}