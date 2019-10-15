using RoomLocator.Data.Config;
using RoomLocator.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoomLocator.Api.Helpers
{
    public static class DatabaseSeedHelper
    {
        public static void SeedDatabase(RoomLocatorContext context)
        {

            if (!context.MazeMapSections.Any())
            {
                var survey1 = new Survey
                {
                };

                context.Add(survey1);
                context.SaveChanges();

                var mazeMapSection1 = new MazeMapSection
                {
                    ZLevel = 1,
                    SurveyId = survey1.Id
                };

                context.Add(mazeMapSection1);
                context.SaveChanges();

                var question1 = new Question
                {
                    SurveyId = survey1.Id,
                    Text = "How did you like the coffee machine?"
                };

                var question2 = new Question
                {
                    SurveyId = survey1.Id,
                    Text = "Is the temperature alright?"
                };

                var question3 = new Question
                {
                    SurveyId = survey1.Id,
                    Text = "Are the chairs comfortable?"
                };

                var coordinate1 = new Coordinates
                {
                    MazeMapSectionId = mazeMapSection1.Id,
                    Longitude = 12.4321,
                    Latitude = 55.4313
                };

                var coordinate2 = new Coordinates
                {
                    MazeMapSectionId = mazeMapSection1.Id,
                    Longitude = 12.4325,
                    Latitude = 55.4316
                };

                var coordinate3 = new Coordinates
                {
                    MazeMapSectionId = mazeMapSection1.Id,
                    Longitude = 12.4324,
                    Latitude = 55.4314
                };

                var coordinate4 = new Coordinates
                {
                    MazeMapSectionId = mazeMapSection1.Id,
                    Longitude = 12.4328,
                    Latitude = 55.4318
                };

                var coordinate5 = new Coordinates
                {
                    MazeMapSectionId = mazeMapSection1.Id,
                    Longitude = 12.4321,
                    Latitude = 55.4313
                };

                context.Add(coordinate1);
                context.Add(coordinate2);
                context.Add(coordinate3);
                context.Add(coordinate4);
                context.Add(coordinate5);
                context.Add(question1);
                context.Add(question2);
                context.Add(question3);
                context.SaveChanges();
            }
        }
    }
}
