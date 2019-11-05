using RoomLocator.Data.Config;
using RoomLocator.Domain.Enums;
using RoomLocator.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RoomLocator.Api.Helpers
{
    /// <summary>
    ///     <author>Thomas Lien Christensen, s165242</author>
    /// </summary>
    public static class DatabaseSeedHelper
    {
        /// <summary>
        ///     <author>Anders Wiberg Olsen, s165241</author>
        /// </summary>
        /// <param name="context"></param>
        public static void SeedRoles(RoomLocatorContext context)
        {
            var roles = new[] {"admin", "researcher", "student"};
            var existingRoles = context.Roles.Select(x => x.Name.ToLower()).ToList();
            var missingRoleNames = new List<string>();

            foreach (var role in roles)
            {
                if (!existingRoles.Contains(role))
                {
                    missingRoleNames.Add(role);
                }
            }

            var missingRoles = missingRoleNames.Select(x => new Role {Name = x.ToLower()});

            context.Roles.AddRange(missingRoles);
            context.SaveChanges();
        }
        
        public static void SeedDatabase(RoomLocatorContext context)
        {   
            if (context.MazeMapSections.Count() < 5)
            {
                var sections = context.MazeMapSections;
                context.RemoveRange(sections);
                context.SaveChanges();
            }

            if (!context.MazeMapSections.Any())
            {
                var loremIpsum = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Duis ut diam quam nulla porttitor massa id neque aliquam.";
                var survey1 = new Survey { Title = "Lorem ipsum dolor sit amet", Description = loremIpsum, CreatedDate = DateTime.Now };
                var survey2 = new Survey { Title = "Dolor purus non enim praesent elementum", Description = loremIpsum, CreatedDate = DateTime.Now };

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

                var mazeMapSection3 = new MazeMapSection
                {
                    ZLevel = 2,
                    SurveyId = survey2.Id,
                    Type = LibrarySectionType.LOUNGE
                };

                var mazeMapSection4 = new MazeMapSection
                {
                    ZLevel = 2,
                    SurveyId = survey2.Id,
                    Type = LibrarySectionType.DATABAR
                };

                var mazeMapSection5 = new MazeMapSection
                {
                    ZLevel = 2,
                    SurveyId = survey2.Id,
                    Type = LibrarySectionType.GROUP_STUDY
                };

                var mazeMapSection6 = new MazeMapSection
                {
                    ZLevel = 2,
                    SurveyId = survey2.Id,
                    Type = LibrarySectionType.DATABAR
                };

                var mazeMapSection7 = new MazeMapSection
                {
                    ZLevel = 2,
                    SurveyId = survey2.Id,
                    Type = LibrarySectionType.GROUP_STUDY
                };

                var mazeMapSection8 = new MazeMapSection
                {
                    ZLevel = 2,
                    SurveyId = survey2.Id,
                    Type = LibrarySectionType.GROUP_STUDY
                };

                var mazeMapSection9 = new MazeMapSection
                {
                    ZLevel = 2,
                    SurveyId = survey2.Id,
                    Type = LibrarySectionType.GROUP_STUDY
                    
                };

                context.Add(mazeMapSection1);
                context.Add(mazeMapSection2);
                context.Add(mazeMapSection3);
                context.Add(mazeMapSection4);
                context.Add(mazeMapSection5);
                context.Add(mazeMapSection6);
                context.Add(mazeMapSection7);
                context.Add(mazeMapSection8);
                context.Add(mazeMapSection9);
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
                        Latitude = 55.787009187536114,
                        Index = 0
                    },
                    new Coordinates
                    {
                        MazeMapSectionId = mazeMapSection1.Id,
                        Longitude = 12.52317152962101,
                        Latitude = 55.78704845759867,
                        Index = 1
                    },
                    new Coordinates
                    {
                        MazeMapSectionId = mazeMapSection1.Id,
                        Longitude = 12.523106783718134,
                        Latitude = 55.78693157515275,
                        Index = 2

                    },
                    new Coordinates
                    {
                        MazeMapSectionId = mazeMapSection1.Id,
                        Longitude = 12.523328398202466,
                        Latitude = 55.78689276418302,
                        Index = 3
                    },
                    new Coordinates
                    {
                        MazeMapSectionId = mazeMapSection1.Id,
                        Longitude = 12.523394936706552,
                        Latitude = 55.787009187536114,
                        Index = 4
                    },
                    new Coordinates
                    {
                        MazeMapSectionId = mazeMapSection2.Id,
                        Longitude = 12.523259637399121,
                        Latitude = 55.786766267668895,
                        Index = 0
                    },
                    new Coordinates
                    {
                        MazeMapSectionId = mazeMapSection2.Id,
                        Longitude = 12.523328398202466,
                        Latitude = 55.78689276418302,
                        Index = 1
                    },
                    new Coordinates
                    {
                        MazeMapSectionId = mazeMapSection2.Id,
                        Longitude = 12.523106783718134,
                        Latitude = 55.78693157515275,
                        Index = 2
                    },
                    new Coordinates
                    {
                        MazeMapSectionId = mazeMapSection2.Id,
                        Longitude = 12.523038473405279,
                        Latitude = 55.78680352605781,
                        Index = 3
                    },
                    new Coordinates
                    {
                        MazeMapSectionId = mazeMapSection2.Id,
                        Longitude = 12.523259637399121,
                        Latitude = 55.786766267668895,
                        Index = 4
                    },

                    // 1st floor corner lounge
                    new Coordinates
                    {
                        MazeMapSectionId = mazeMapSection3.Id,
                        Longitude = 12.522992396101245,
                        Latitude = 55.78708067483939,
                        Index = 0
                    },
                    new Coordinates
                    {
                        MazeMapSectionId = mazeMapSection3.Id,
                        Longitude = 12.52296211062415,
                        Latitude = 55.787025664233454,
                        Index = 1
                    },
                    new Coordinates
                    {
                        MazeMapSectionId = mazeMapSection3.Id,
                        Longitude = 12.523103078712865,
                        Latitude = 55.78700090057603,
                        Index = 2
                    },
                    new Coordinates
                    {
                        MazeMapSectionId = mazeMapSection3.Id,
                        Longitude = 12.523133512544206,
                        Latitude = 55.787056448768766,
                        Index = 3
                    },
                    new Coordinates
                    {
                        MazeMapSectionId = mazeMapSection3.Id,
                        Longitude = 12.522992396101245,
                        Latitude = 55.78708067483939,
                        Index = 4
                    },

                    // 1st floor databar 1
                    new Coordinates
                    {
                        MazeMapSectionId = mazeMapSection4.Id,
                        Longitude = 12.523070611196516,
                        Latitude = 55.78688036166568,
                        Index = 0
                    },

                    new Coordinates
                    {
                        MazeMapSectionId = mazeMapSection4.Id,
                        Longitude = 12.523035424992003,
                        Latitude = 55.7868150048902,
                        Index = 1
                    },


                    new Coordinates
                    {
                        MazeMapSectionId = mazeMapSection4.Id,
                        Longitude = 12.523057303704121,
                        Latitude = 55.786811200586556,
                        Index = 2
                    },


                    new Coordinates
                    {
                        MazeMapSectionId = mazeMapSection4.Id,
                        Longitude = 12.523052804076087,
                        Latitude = 55.78680270866491,
                        Index = 3
                    },

                    new Coordinates
                    {
                        MazeMapSectionId = mazeMapSection4.Id,
                        Longitude = 12.523442220398778,
                        Latitude = 55.78673536458402,
                        Index = 4
                    },

                    new Coordinates
                    {
                        MazeMapSectionId = mazeMapSection4.Id,
                        Longitude = 12.523482255351013,
                        Latitude = 55.78680885135134,
                        Index = 5
                    },

                    new Coordinates
                    {
                        MazeMapSectionId = mazeMapSection4.Id,
                        Longitude = 12.523070611196516,
                        Latitude = 55.78688036166568,
                        Index = 6
                    },

                    // 1st floor group study 1
                    new Coordinates
                    {
                        MazeMapSectionId = mazeMapSection5.Id,
                        Longitude = 12.523482255351013,
                        Latitude = 55.78680885135134,
                        Index = 0
                    },

                    new Coordinates
                    {
                        MazeMapSectionId = mazeMapSection5.Id,
                        Longitude = 12.523447000092318,
                        Latitude = 55.786743509263715,
                        Index = 1
                    },

                    new Coordinates
                    {
                        MazeMapSectionId = mazeMapSection5.Id,
                        Longitude = 12.523641059141397,
                        Latitude = 55.78670979806952,
                        Index = 2
                    },

                    new Coordinates
                    {
                        MazeMapSectionId = mazeMapSection5.Id,
                        Longitude = 12.52374629327332,
                        Latitude = 55.786901383480824,
                        Index = 3
                    },

                    new Coordinates
                    {
                        MazeMapSectionId = mazeMapSection5.Id,
                        Longitude = 12.523572349344818,
                        Latitude = 55.78693143358913,
                        Index = 4
                    },

                    new Coordinates
                    {
                        MazeMapSectionId = mazeMapSection5.Id,
                        Longitude = 12.523502485736486,
                        Latitude = 55.78680528200479,
                        Index = 5
                    },

                    new Coordinates
                    {
                        MazeMapSectionId = mazeMapSection5.Id,
                        Longitude = 12.523482255351013,
                        Latitude = 55.78680885135134,
                        Index = 6
                    },

                    // 1st floor databar 2
                    new Coordinates
                    {
                        MazeMapSectionId = mazeMapSection6.Id,
                        Longitude = 12.523381586623486,
                        Latitude = 55.787013378777004,
                        Index = 0
                    },
                    new Coordinates
                    {
                        MazeMapSectionId = mazeMapSection6.Id,
                        Longitude = 12.523356818522757,
                        Latitude = 55.78696882184383,
                        Index = 1
                    },
                    new Coordinates
                    {
                        MazeMapSectionId = mazeMapSection6.Id,
                        Longitude = 12.523572349344818,
                        Latitude = 55.78693143358913,
                        Index = 2
                    },
                    new Coordinates
                    {
                        MazeMapSectionId = mazeMapSection6.Id,
                        Longitude = 12.52374629327332,
                        Latitude = 55.786901383480824,
                        Index = 3
                    },
                    new Coordinates
                    {
                        MazeMapSectionId = mazeMapSection6.Id,
                        Longitude = 12.52377028372436,
                        Latitude = 55.78694572525518,
                        Index = 4
                    },
                    new Coordinates
                    {
                        MazeMapSectionId = mazeMapSection6.Id,
                        Longitude = 12.523381586623486,
                        Latitude = 55.787013378777004,
                        Index = 5
                    },

                    // 1st floor group study 2
                    new Coordinates
                    {
                        MazeMapSectionId = mazeMapSection7.Id,
                        Longitude = 12.523356818522757,
                        Latitude = 55.78696882184383,
                        Index = 0
                    },
                    new Coordinates
                    {
                        MazeMapSectionId = mazeMapSection7.Id,
                        Longitude = 12.523339474902428,
                        Latitude = 55.78693753051286,
                        Index = 1
                    },
                    new Coordinates
                    {
                        MazeMapSectionId = mazeMapSection7.Id,
                        Longitude = 12.5235550453948,
                        Latitude = 55.78689997276197,
                        Index = 2
                    },
                    new Coordinates
                    {
                        MazeMapSectionId = mazeMapSection7.Id,
                        Longitude = 12.5235550453948,
                        Latitude = 55.78689997276197,
                        Index = 3
                    },
                    new Coordinates
                    {
                        MazeMapSectionId = mazeMapSection7.Id,
                        Longitude = 12.523572349344818,
                        Latitude = 55.78693143358913,
                        Index = 4
                    },
                    new Coordinates
                    {
                        MazeMapSectionId = mazeMapSection7.Id,
                        Longitude = 12.523356818522757,
                        Latitude = 55.78696882184383,
                        Index = 5
                    },

                    // 1st floor group study 3
                    new Coordinates
                    {
                        MazeMapSectionId = mazeMapSection8.Id,
                        Longitude = 12.523133512544206,
                        Latitude = 55.787056448768766,
                        Index = 0
                    },
                    new Coordinates
                    {
                        MazeMapSectionId = mazeMapSection8.Id,
                        Longitude = 12.523091692964897,
                        Latitude = 55.78698056999656,
                        Index = 1
                    },
                    new Coordinates
                    {
                        MazeMapSectionId = mazeMapSection8.Id,
                        Longitude = 12.523339474902428,
                        Latitude = 55.78693753051286,
                        Index = 2
                    },
                    new Coordinates
                    {
                        MazeMapSectionId = mazeMapSection8.Id,
                        Longitude = 12.523381586623486,
                        Latitude = 55.787013378777004,
                        Index = 3
                    },
                    new Coordinates
                    {
                        MazeMapSectionId = mazeMapSection8.Id,
                        Longitude = 12.523133512544206,
                        Latitude = 55.787056448768766,
                        Index = 4
                    },

                    // 1st floor group study 4
                    new Coordinates
                    {
                        MazeMapSectionId = mazeMapSection9.Id,
                        Longitude = 12.52296211062415,
                        Latitude = 55.787025664233454,
                        Index = 0
                    },
                    new Coordinates
                    {
                        MazeMapSectionId = mazeMapSection9.Id,
                        Longitude = 12.522862484572613,
                        Latitude = 55.786844942921476,
                        Index = 1
                    },
                    new Coordinates
                    {
                        MazeMapSectionId = mazeMapSection9.Id,
                        Longitude = 12.523035424992003,
                        Latitude = 55.7868150048902,
                        Index = 2
                    },
                    new Coordinates
                    {
                        MazeMapSectionId = mazeMapSection9.Id,
                        Longitude = 12.523122460328437,
                        Latitude = 55.786975109109136,
                        Index = 3
                    },
                    new Coordinates
                    {
                        MazeMapSectionId = mazeMapSection9.Id,
                        Longitude = 12.523091692964897,
                        Latitude = 55.78698056999656,
                        Index = 4
                    },
                    new Coordinates
                    {
                        MazeMapSectionId = mazeMapSection9.Id,
                        Longitude = 12.523103078712865,
                        Latitude = 55.78700090057603,
                        Index = 5
                    },
                    new Coordinates
                    {
                        MazeMapSectionId = mazeMapSection9.Id,
                        Longitude = 12.52296211062415,
                        Latitude = 55.787025664233454,
                        Index = 6
                    },

                };

                context.AddRange(coordinates);
                context.AddRange(questions);
                context.SaveChanges();
            }
        }
    }
}
