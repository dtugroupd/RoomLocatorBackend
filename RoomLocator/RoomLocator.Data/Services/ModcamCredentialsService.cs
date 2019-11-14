using System;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RoomLocator.Domain.ViewModels;

namespace RoomLocator.Data.Services
{
    /// <summary>
    /// 	<author>Amal Qasim, s132957</author>
    /// </summary>
    public class ModcamCredentialsService 
    {
        public async Task<ApiCredentialsViewModel> LoadFile() 
        {
            var stringJson = File.ReadAllText(@"api_credentials.json");
            var convertJson = JsonConvert.DeserializeObject<ApiCredentialsViewModel>(stringJson);
         
            return convertJson;
            
        }

    }
    
}