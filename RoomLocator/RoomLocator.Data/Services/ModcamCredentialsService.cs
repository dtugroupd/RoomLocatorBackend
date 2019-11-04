using System.IO;
using AutoMapper;
using Newtonsoft.Json;
using RoomLocator.Data.Config;
using RoomLocator.Domain.ViewModels;

namespace RoomLocator.Data.Services
{
   
    public class ModcamCredentialsService: BaseService
    { 
        public ModcamCredentialsService(RoomLocatorContext context, IMapper mapper) : base(context, mapper) { }

        private readonly string _path = "RoomLocator/RoomLocator.Api/api_credentials.json";
        
        public ApiCredentialsViewModel LoadFile()
        {
            ApiCredentialsViewModel apiCred =
                JsonConvert.DeserializeObject<ApiCredentialsViewModel>(File.ReadAllText(_path));
            using (StreamReader file = File.OpenText(_path))
            {
                JsonSerializer serializer = new JsonSerializer();
                ApiCredentialsViewModel apiCred2 = (ApiCredentialsViewModel) serializer.Deserialize(
                    file, typeof(ApiCredentialsViewModel));
            }
            return apiCred;


        }

    }
    
}