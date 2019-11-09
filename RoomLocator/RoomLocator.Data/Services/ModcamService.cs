using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RoomLocator.Data.Config;
using RoomLocator.Domain.ViewModels;

/// <summary>
/// 	<author>Amal Qasim, s132957</author>
/// </summary>
namespace RoomLocator.Data.Services
{
    public class ModcamService
    {

        public HttpRequestMessage  RequestsForHttp()
        {
            var url = "https://eds.modcam.io/v1/peoplecounter/installations";
            var request = new HttpRequestMessage(HttpMethod.Get, url);
            var modcamCredentialsService = new  ModcamCredentialsService();
            
            request.Headers.Add("x-client-id", modcamCredentialsService.LoadFile().ClientId);
            request.Headers.Add("x-api-key", modcamCredentialsService.LoadFile().Key);

            return request;
        }


    }
}