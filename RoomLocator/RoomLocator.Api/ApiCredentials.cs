
using System.IO;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RoomLocator.Domain.ViewModels;
using Shared;

namespace RoomLocator.Api.Controllers
{
   
    public class ApiCredentials 
    {
        private static string path = "RoomLocator/RoomLocator.Api/api_credentials.json";


        public ApiCredentials()
        {
         
            loadFile();
        }
        
        static void loadFile()
        {
            ApiCredentialsViewModel apiCred =
                JsonConvert.DeserializeObject<ApiCredentialsViewModel>(File.ReadAllText(@path));
            using (StreamReader file = File.OpenText(@path))
            {
                JsonSerializer serializer = new JsonSerializer();
                ApiCredentialsViewModel apiCred2 = (ApiCredentialsViewModel) serializer.Deserialize(
                    file, typeof(ApiCredentialsViewModel));
            }


        }

    }
    
}