using System;
using System.IO;
using System.Net;
using System.Numerics;
using AutoMapper;
using Newtonsoft.Json;
using RoomLocator.Data.Config;
using RoomLocator.Domain.ViewModels;
/// <summary>
/// 	<author>Amal Qasim, s132957</author>
/// </summary>

namespace RoomLocator.Data.Services
{
    public class ModcamCredentialsService 
    {
        public ApiCredentialsViewModel LoadFile() 
        {
            var stringJson = File.ReadAllText(@"api_credentials.json");
            var convertJson = JsonConvert.DeserializeObject<ApiCredentialsViewModel>(stringJson);
         
            return convertJson;
            
        }

    }
    
}