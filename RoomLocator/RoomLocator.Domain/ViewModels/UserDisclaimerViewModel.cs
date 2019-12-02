namespace RoomLocator.Domain.ViewModels
{
    public class UserDisclaimerViewModel
    {
        public UserDisclaimerViewModel() { }

        public UserDisclaimerViewModel(bool accepted)
        {
            HasAcceptedDisclaimer = accepted;
        }

        public bool HasAcceptedDisclaimer { get; set; }
        public static UserDisclaimerViewModel NotAccepted() => new UserDisclaimerViewModel(false);
        public static UserDisclaimerViewModel Accepted() => new UserDisclaimerViewModel(true);
    }
}
