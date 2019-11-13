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
    public class ScadadataService
    {
        private const double SeatsWeight = 0.6; 
        private const double TemperatureWeight = 0.15; 
        private const double SoundWeight = 0.15; 
        private const double LightWeight = 0.1;
        private const double TemperatureOptimal = 22; //22 C
        private const double SoundOptimal = 150; //25 Db
        private const double LightOptimal = 360; //360 ??
        private const double TemperatureMaxVariance = 18; //+-18C
        private const double SoundMaxVariance = 300; //+-50 Db
        private const double LightMaxVariance = 300; //+-300 ??



        private readonly IHttpClientFactory _clientFactory;

        public ScadadataService(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<IEnumerable<ScadadataViewModel>> GetSensorData()
        {
            const string url = "https://scadadataapi.azurewebsites.net/api/values";
            var request = new HttpRequestMessage(HttpMethod.Get, url);
            //var mc = new  ScadadataService();


            var client = _clientFactory.CreateClient("dtu-cas");
            var response = await client.SendAsync(request);

            return !response.IsSuccessStatusCode ? null : JsonConvert.DeserializeObject<List<ScadadataViewModel>>(await
                response.Content.ReadAsStringAsync());
        }
        
        public async Task<IEnumerable<ScadadataScoresModel>> GetListOfScores()
        {
            var sd = await GetSensorData();
            var scadadataViewModels = sd.ToList();
            var temperatures = scadadataViewModels.Where(item => item.Type == "Temperature").ToList();
            var lights = scadadataViewModels.Where(item => item.Type == "Light").ToList(); 
            var sounds = scadadataViewModels.Where(item => item.Type == "Sound").ToList(); 
            var availableSeats = scadadataViewModels.Where(item => item.Type == "available seats").ToList(); 
            var maxAvailableSeats = scadadataViewModels.Where(item => item.Type == "max available seats").ToList();
            var list = new List<ScadadataScoresModel>
            {
                new ScadadataScoresModel("Temperature",
                    1 - FindAverageDeviation(temperatures, TemperatureOptimal, TemperatureMaxVariance)),
                new ScadadataScoresModel("Sound",
                    1-FindAverageDeviation(sounds, SoundOptimal, SoundMaxVariance)),
                new ScadadataScoresModel("Light",
                    1-FindAverageDeviation(lights, LightOptimal, LightMaxVariance)),
                new ScadadataScoresModel("Seats Available",
                    1-FindAverageDeviation(availableSeats, maxAvailableSeats[0].Value, 
                        maxAvailableSeats[0].Value-40))
            };
            return list;


        }
        public async Task<double> GetWeightedScore()
        {
            var sd = await GetSensorData();
            var scadadataViewModels = sd.ToList();
            var temperatures = scadadataViewModels.Where(item => item.Type == "Temperature").ToList();
            var lights = scadadataViewModels.Where(item => item.Type == "Light").ToList(); 
            var sounds = scadadataViewModels.Where(item => item.Type == "Sound").ToList(); 
            var availableSeats = scadadataViewModels.Where(item => item.Type == "available seats").ToList(); 
            var maxAvailableSeats = scadadataViewModels.Where(item => item.Type == "max available seats").ToList();
            
            return (1-FindAverageDeviation(temperatures, TemperatureOptimal, TemperatureMaxVariance))*TemperatureWeight
                   +(1-FindAverageDeviation(sounds, SoundOptimal, SoundMaxVariance))*SoundWeight
                   +(1-FindAverageDeviation(lights, LightOptimal, LightMaxVariance))*LightWeight
                   +(1-FindAverageDeviation(availableSeats, maxAvailableSeats[0].Value, 
                         maxAvailableSeats[0].Value))*SeatsWeight;
        }

        private static double FindAverageDeviation(IEnumerable<ScadadataViewModel> list, double optimal, double maxVariance)
        {
            var deviations = list.Select(item => GetDeviation(item.Value, optimal, maxVariance)).ToList();
            return deviations.Average();
        }
        
        private static double GetDeviation(double measurement, double optimal, double maxVariance)
        {
            var deviation = (Math.Abs(optimal - measurement)) / maxVariance ;
            return deviation > 1.0 ? 1.0 : deviation;
        }


        
    }
}




     







      

    