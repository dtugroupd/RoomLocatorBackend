using System.IO;
using Newtonsoft.Json;
using RoomLocator.Domain.ViewModels;

namespace RoomLocator.Api.Controllers
{
    public class ApiCredentialscontroller
    {
        private static string path = "RoomLocator/RoomLocator.Api/api_credentials.json";
        
        private ApiCredentialsViewModel apiCred = JsonConvert.DeserializeObject<ApiCredentialsViewModel>(
            File.ReadAllText(@path));

        static void loadJson()
        {
            using (StreamReader file = File.OpenText(@path)){
                        JsonSerializer serializer = new JsonSerializer();
                        ApiCredentialsViewModel apiCred2 = (ApiCredentialsViewModel) serializer.Deserialize(
                            file, typeof(ApiCredentialsViewModel)); }
        }
        
    }
}