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

                var questions = new List<Question>
                {
                    new Question
                    {
                        SurveyId = survey1.Id,
                        Text = "How did you like the coffee machine?"
                    },
                    new Question
                    {
                        SurveyId = survey1.Id,
                        Text = "Is the temperature alright?"
                    },
                    new Question
                    {
                        SurveyId = survey1.Id,
                        Text = "Are the chairs comfortable?"
                    },
                    new Question
                    {
                        SurveyId = survey2.Id,
                        Text = "How do you like the chairs?"
                    },
                    new Question
                    {
                        SurveyId = survey2.Id,
                        Text = "We recently installed new printers. If you have gotten a chance to use them, how did you like them?"
                    },
                    new Question
                    {
                        SurveyId = survey2.Id,
                        Text = "Do you frequently use the bonfire screen saver?"
                    }
                };

                var coordinates = new List<Coordinates>
                {
                    new Coordinates
                    {
                        MazeMapSectionId = mazeMapSection1.Id,
                        Longitude = 12.523394936706552,
                        Latitude = 55.787009187536114
                    },
                    new Coordinates
                    {
                        MazeMapSectionId = mazeMapSection1.Id,
                        Longitude = 12.52317152962101,
                        Latitude = 55.78704845759867
                    },
                    new Coordinates
                    {
                        MazeMapSectionId = mazeMapSection1.Id,
                        Longitude = 12.523106783718134,
                        Latitude = 55.78693157515275
                    },
                    new Coordinates
                    {
                        MazeMapSectionId = mazeMapSection1.Id,
                        Longitude = 12.523328398202466,
                        Latitude = 55.78689276418302
                    },
                    new Coordinates
                    {
                        MazeMapSectionId = mazeMapSection1.Id,
                        Longitude = 12.523394936706552,
                        Latitude = 55.787009187536114
                    },
                    new Coordinates
                    {
                        MazeMapSectionId = mazeMapSection2.Id,
                        Longitude = 12.523259637399121,
                        Latitude = 55.786766267668895
                    },
                    new Coordinates
                    {
                        MazeMapSectionId = mazeMapSection2.Id,
                        Longitude = 12.523328398202466,
                        Latitude = 55.78689276418302
                    },
                    new Coordinates
                    {
                        MazeMapSectionId = mazeMapSection2.Id,
                        Longitude = 12.523106783718134,
                        Latitude = 55.78693157515275
                    },
                    new Coordinates
                    {
                        MazeMapSectionId = mazeMapSection2.Id,
                        Longitude = 12.523038473405279,
                        Latitude = 55.78680352605781
                    },
                    new Coordinates
                    {
                        MazeMapSectionId = mazeMapSection2.Id,
                        Longitude = 12.523259637399121,
                        Latitude = 55.786766267668895
                    }
                };

                context.AddRange(coordinates);
                context.AddRange(questions);
                context.SaveChanges();
            }
        }
    }
}
