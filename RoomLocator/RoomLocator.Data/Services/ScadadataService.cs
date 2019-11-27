using System;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RoomLocator.Data.Config;
using RoomLocator.Domain;
using RoomLocator.Domain.InputModels;
using RoomLocator.Domain.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Internal;


namespace RoomLocator.Data.Services
{
    /// <summary>
    ///    <author>Andreas Gøricke, s153804</author>
    /// </summary>
    public class ScadadataService
    {
        private const double SeatsWeight = 0.6; 
        private const double TemperatureWeight = 0.15; 
        private const double SoundWeight = 0.15; 
        private const double LightWeight = 0.1;
        private const double SeatsAvailableMin = 40;
        
        //Defines optimal values for each known variable.
        private const double TemperatureOptimal = 22; //22 C
        private const double SoundOptimal = 150; //25 Db
        private const double LightOptimal = 360; //360 ??
        
        //Defines max variance
        private const double TemperatureMaxVariance = 18; //+-18C
        private const double SoundMaxVariance = 300; //+-50 Db
        private const double LightMaxVariance = 300; //+-300 ??
        
        
        //Since all models are subjected to a linear model, these constants define specific limits for each known variable.
        //Eg. a 40% change in temperature (about 14 degrees/30 degrees) would be "really bad", but a 40% reduction
        //in seats available would not be considered "really bad".
        private const double TemperatureQ1Limit = 0.9;
        private const double TemperatureQ2Limit = 0.8;
        private const double TemperatureQ3Limit = 0.7;
        private const double TemperatureQ4Limit = 0.6;
        private const double SoundQ1Limit = 0.8;
        private const double SoundQ2Limit = 0.7;
        private const double SoundQ3Limit = 0.5;
        private const double SoundQ4Limit = 0.3;
        private const double LightQ1Limit = 0.8;
        private const double LightQ2Limit = 0.6;
        private const double LightQ3Limit = 0.3;
        private const double LightQ4Limit = 0.1;
        private const double SeatsAvailableQ1Limit = 0.85;
        private const double SeatsAvailableQ2Limit = 0.7;
        private const double SeatsAvailableQ3Limit = 0.5;
        private const double SeatsAvailableQ4Limit = 0.3;
        private const double StatusQ1Limit = 0.6;
        private const double StatusQ2Limit = 0.35;
        


        private readonly IHttpClientFactory _clientFactory;

        public ScadadataService(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        private async Task<List<ScadadataViewModel>> GetSensorData()
        {
            const string url = "https://scadadataapi.azurewebsites.net/api/values";
            var request = new HttpRequestMessage(HttpMethod.Get, url);
            var client = _clientFactory.CreateClient("dtu-cas");
            var response = await client.SendAsync(request);
            return JsonConvert.DeserializeObject<List<ScadadataViewModel>>(await
                response.Content.ReadAsStringAsync());
        }
        
        public async Task<ScadadataInfoModel> GetListOfScores()
        {
            var scadadataViewModels = await GetSensorData();
            var list = new List<ScadadataScoresModel>
            {
                new ScadadataScoresModel("Temperature",
                    FindAverageDeviation(scadadataViewModels.Where(item => item.Type == "Temperature").ToList(),
                        TemperatureOptimal, TemperatureMaxVariance, 
                        TemperatureQ1Limit, TemperatureQ2Limit, TemperatureQ3Limit, TemperatureQ4Limit)), 
                new ScadadataScoresModel("Sound",
                    FindAverageDeviation(scadadataViewModels.Where(item => item.Type == "Sound").ToList(),
                        SoundOptimal, SoundMaxVariance,
                        SoundQ1Limit, SoundQ2Limit, SoundQ3Limit,SoundQ4Limit)),
                new ScadadataScoresModel("Light",
                    FindAverageDeviation(scadadataViewModels.Where(item => item.Type == "Light").ToList(),
                        LightOptimal, LightMaxVariance, 
                        LightQ1Limit, LightQ2Limit, LightQ3Limit, LightQ4Limit)),
                new ScadadataScoresModel("Seats Available",
                    FindAverageDeviation(scadadataViewModels.Where(item => item.Type == "available seats").ToList(),
                        scadadataViewModels.Where(item => item.Type == "max available seats").ToList()[0].Value, 
                        scadadataViewModels.Where(item => item.Type == "max available seats").ToList()[0].Value-SeatsAvailableMin, 
                        SeatsAvailableQ1Limit, SeatsAvailableQ2Limit, SeatsAvailableQ3Limit, SeatsAvailableQ4Limit))
            };
            var weighted = await GetWeightedScore();
            string status = (weighted > StatusQ1Limit ? "good" : (weighted > StatusQ2Limit ? "okay" : "bad"));
            return new ScadadataInfoModel(status, list);


        }
        private async Task<double> GetWeightedScore()
        {
            var scadadataViewModels = await GetSensorData();
            return (1-FindAverageDeviation(scadadataViewModels.Where(item => item.Type == "Temperature").ToList(), 
                        TemperatureOptimal, TemperatureMaxVariance))*TemperatureWeight
                   +(1-FindAverageDeviation(scadadataViewModels.Where(item => item.Type == "Sound").ToList(), 
                         SoundOptimal, SoundMaxVariance))*SoundWeight
                   +(1-FindAverageDeviation(scadadataViewModels.Where(item => item.Type == "Light").ToList(), 
                         LightOptimal, LightMaxVariance))*LightWeight
                   +(1-FindAverageDeviation(scadadataViewModels.Where(item => item.Type == "available seats").ToList(),
                         scadadataViewModels.Where(item => item.Type == "max available seats").ToList()[0].Value, 
                         scadadataViewModels.Where(item => item.Type == "max available seats").ToList()[0].Value))*SeatsWeight;
        }

        private static string FindAverageDeviation(IEnumerable<ScadadataViewModel> list, double optimal, double maxVariance, 
            double q1, double q2, double q3, double q4)
        {
            var avg = 1-list.Select(item => GetDeviation(item.Value, optimal, maxVariance)).ToList().Average();
            return avg > q1 ? "very good" : (avg > q2 ? "good" : (avg > q3 ? "okay" : (avg > q4 ? "bad" : "very bad")));
        }
        
        private static double FindAverageDeviation(IEnumerable<ScadadataViewModel> list, double optimal, double maxVariance)
        {
            return  (list.Select(item => GetDeviation(item.Value, optimal, maxVariance)).ToList()).Average();
        }
        
        private static double GetDeviation(double measurement, double optimal, double maxVariance)
        {
            var deviation = (Math.Abs(optimal - measurement)) / maxVariance ;
            return deviation > 1.0 ? 1.0 : deviation;
        }


        
    }
}