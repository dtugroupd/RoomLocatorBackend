/// <summary>
/// 	<author>Amal Qasim, s132957</author>
/// </summary>

namespace RoomLocator.Domain.ViewModels
{
    public class ApiCredentialsViewModel
    {
        public string Key { get; set; }
        public string ClientId { get; set; }

        public override string ToString()
        {
            return string.Format("Key:{0}, ClientId: {1} ", Key,ClientId);
        }
    }
}