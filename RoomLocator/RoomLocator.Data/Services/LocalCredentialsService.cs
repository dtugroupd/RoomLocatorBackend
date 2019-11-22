using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RoomLocator.Domain.Models.CredentialsModels;

namespace RoomLocator.Data.Services
{
    /// <summary>
    /// 	<author>Amal Qasim, s132957</author>
    ///     <author>Anders Wiberg Olsen, s165241</author>
    /// </summary>
    public class LocalCredentialsService 
    {
        private readonly CampusNetApiCredentials _cnCredentials;

        public LocalCredentialsService(IOptions<CampusNetApiCredentials> cnCredentialsOptions)
        {
            var cnCredentialsOptionsOptions = cnCredentialsOptions;
            if (cnCredentialsOptionsOptions?.Value == null) return;

            _cnCredentials = cnCredentialsOptionsOptions.Value;

            if (_cnCredentials.ApiToken == null || _cnCredentials.AppName == null)
            {
                _cnCredentials = null;
            }
        }

        public async Task<ModcamApiCredentials> LoadModcamCredentials() 
        {
            var stringJson = await File.ReadAllTextAsync(@"api_credentials.json");
            var convertJson = JsonConvert.DeserializeObject<ModcamApiCredentials>(stringJson);
         
            return convertJson;
        }

        public async Task<CampusNetApiCredentials> LoadCampusNetCredentials()
        {
            if (_cnCredentials != null) return _cnCredentials;
            var cnJson = await File.ReadAllTextAsync("cn_credentials.json");

            if (cnJson == null)
            {
                throw new Exception("File \"cn_credentials.json\" does not exist.");
            }
            
            return JsonConvert.DeserializeObject<CampusNetApiCredentials>(cnJson);
        }
    }
}
