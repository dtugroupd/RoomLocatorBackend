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
            string stringJson = File.ReadAllText(@"api_credentials.json");
            var convJson = JsonConvert.DeserializeObject<ApiCredentialsViewModel>(stringJson);
         
            return convJson;


        }

    }
    
}