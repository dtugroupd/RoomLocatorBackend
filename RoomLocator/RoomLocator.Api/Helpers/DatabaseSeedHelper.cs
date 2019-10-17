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
                var survey1 = new Survey { };
                var survey2 = new Survey { };

                context.Add(survey1);
                context.Add(survey2);
                context.SaveChanges();

                var mazeMapSection1 = new MazeMapSection
                {
                    ZLevel = 1,
                    SurveyId = survey1.Id
                };

                var mazeMapSection2 = new MazeMapSection
                {
                    ZLevel = 1,
                    SurveyId = survey2.Id
                };

                context.Add(mazeMapSection1);
                context.Add(mazeMapSection2);
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

                var question4 = new Question
                {
                    SurveyId = survey2.Id,
                    Text = "How do you like the chairs?"
                };

                var question5 = new Question
                {
                    SurveyId = survey2.Id,
                    Text = "We recently installed new printers. If you have gotten a chance to use them, how did you like them?"
                };

                var question6 = new Question
                {
                    SurveyId = survey2.Id,
                    Text = "Do you frequently use the bonfire screen saver?"
                };

                var coordinate1 = new Coordinates
                {
                    MazeMapSectionId = mazeMapSection1.Id,
                    Longitude = 12.523394936706552,
                    Latitude = 55.787009187536114
                };

                var coordinate2 = new Coordinates
                {
                    MazeMapSectionId = mazeMapSection1.Id,
                    Longitude = 12.52317152962101,
                    Latitude = 55.78704845759867
                };

                var coordinate3 = new Coordinates
                {
                    MazeMapSectionId = mazeMapSection1.Id,
                    Longitude = 12.523106783718134,
                    Latitude = 55.78693157515275
                };

                var coordinate4 = new Coordinates
                {
                    MazeMapSectionId = mazeMapSection1.Id,
                    Longitude = 12.523328398202466,
                    Latitude = 55.78689276418302
                };

                var coordinate5 = new Coordinates
                {
                    MazeMapSectionId = mazeMapSection1.Id,
                    Longitude = 12.523394936706552,
                    Latitude = 55.787009187536114
                };

                var coordinate6 = new Coordinates
                {
                    MazeMapSectionId = mazeMapSection2.Id,
                    Longitude = 12.523259637399121,
                    Latitude = 55.786766267668895
                };

                var coordinate7 = new Coordinates
                {
                    MazeMapSectionId = mazeMapSection2.Id,
                    Longitude = 12.523328398202466,
                    Latitude = 55.78689276418302
                };

                var coordinate8 = new Coordinates
                {
                    MazeMapSectionId = mazeMapSection2.Id,
                    Longitude = 12.523106783718134,
                    Latitude = 55.78693157515275
                };

                var coordinate9 = new Coordinates
                {
                    MazeMapSectionId = mazeMapSection2.Id,
                    Longitude = 12.523038473405279,
                    Latitude = 55.78680352605781
                };

                var coordinate10 = new Coordinates
                {
                    MazeMapSectionId = mazeMapSection2.Id,
                    Longitude = 12.523259637399121,
                    Latitude = 55.786766267668895
                };

                context.Add(coordinate1);
                context.Add(coordinate2);
                context.Add(coordinate3);
                context.Add(coordinate4);
                context.Add(coordinate5);
                context.Add(coordinate6);
                context.Add(coordinate7);
                context.Add(coordinate8);
                context.Add(coordinate9);
                context.Add(coordinate10);
                context.Add(question1);
                context.Add(question2);
                context.Add(question3);
                context.Add(question4);
                context.Add(question5);
                context.Add(question6);
                context.SaveChanges();
            }
        }
    }
}
