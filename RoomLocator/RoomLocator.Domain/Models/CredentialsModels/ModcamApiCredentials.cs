namespace RoomLocator.Domain.Models.CredentialsModels
{
    /// <summary>
    /// 	<author>Amal Qasim, s132957</author>
    /// </summary>
    public abstract class ModcamApiCredentials
    {
        public string Key { get; set; }
        public string ClientId { get; set; }
    }
}
