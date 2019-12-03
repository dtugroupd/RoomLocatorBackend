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
            var roles = new[] { "admin", "researcher", "student" };
            var existingRoles = context.Roles.Select(x => x.Name.ToLower()).ToList();
            var missingRoleNames = new List<string>();

            foreach (var role in roles)
            {
                if (!existingRoles.Contains(role))
                {
                    missingRoleNames.Add(role);
                }
            }

            var missingRoles = missingRoleNames.Select(x => new Role { Name = x.ToLower() });

            context.Roles.AddRange(missingRoles);
            context.SaveChanges();
        }
        
        /// <summary>
        ///     <author>Andreas Gøricke, s153804</author>
        /// </summary>
        public static void SeedSensors(RoomLocatorContext context)
        {
            if (context.Sensors.Any()) return;
            var sensors = new List<Sensor>
            {
                new Sensor
                {
                    Id = "B101_DI100_02_US01",
                    Type = SensorType.Ultrasound,
                    Provider = SensorProvider.Scadadata,
                    Latitude = 55.7869945007994,
                    Longitude = 12.5232421671629,
                    ZLevel = 1
                },
                new Sensor
                {
                    Id = "B101_DI100_02_TR01",
                    Type = SensorType.Temperature,
                    Provider = SensorProvider.Scadadata,
                    Latitude = 55.7869945007994,
                    Longitude = 12.5232421671629,
                    ZLevel = 1
                },
                new Sensor
                {
                    Id = "B101_DI100_02_HR01",
                    Type = SensorType.Humidity,
                    Provider = SensorProvider.Scadadata,
                    Latitude = 55.7869945007994,
                    Longitude = 12.5232421671629,
                    ZLevel = 1
                },
                new Sensor
                {
                    Id = "B101_DI100_02_SR01",
                    Type = SensorType.Sound,
                    Provider = SensorProvider.Scadadata,
                    Latitude = 55.7869945007994,
                    Longitude = 12.5232421671629,
                    ZLevel = 1
                },
                new Sensor
                {
                    Id = "B101_DI100_02_LX01",
                    Type = SensorType.Light,
                    Provider = SensorProvider.Scadadata,
                    Latitude = 55.7869945007994,
                    Longitude = 12.5232421671629,
                    ZLevel = 1
                },
                new Sensor
                {
                    Id = "B101_DI200_06_US01",
                    Type = SensorType.Ultrasound,
                    Provider = SensorProvider.Scadadata,
                    Latitude = 55.7867729072814,
                    Longitude = 12.523521560966,
                    ZLevel = 1
                },
                new Sensor
                {
                    Id = "B101_DI200_06_TR01",
                    Type = SensorType.Temperature,
                    Provider = SensorProvider.Scadadata,
                    Latitude = 55.7867729072814,
                    Longitude = 12.523521560966,
                    ZLevel = 1
                },
                new Sensor
                {
                    Id = "B101_DI200_06_HR01",
                    Type = SensorType.Humidity,
                    Provider = SensorProvider.Scadadata,
                    Latitude = 55.7867729072814,
                    Longitude = 12.523521560966,
                    ZLevel = 1
                },
                new Sensor
                {
                    Id = "B101_DI200_06_SR01",
                    Type = SensorType.Sound,
                    Provider = SensorProvider.Scadadata,
                    Latitude = 55.7867729072814,
                    Longitude = 12.523521560966,
                    ZLevel = 1
                },
                new Sensor
                {
                    Id = "B101_DI200_06_LX01",
                    Type = SensorType.Light,
                    Provider = SensorProvider.Scadadata,
                    Latitude = 55.7867729072814,
                    Longitude = 12.523521560966,
                    ZLevel = 1
                },
                new Sensor
                {
                    Id = "B101_DI000_07_US01",
                    Type = SensorType.Ultrasound,
                    Provider = SensorProvider.Scadadata,
                    Latitude = 55.7868413016794,
                    Longitude = 12.5231868449781,
                    ZLevel = 1
                },
                new Sensor
                {
                    Id = "B101_DI000_07_TR01",
                    Type = SensorType.Temperature,
                    Provider = SensorProvider.Scadadata,
                    Latitude = 55.7868413016794,
                    Longitude = 12.5231868449781,
                    ZLevel = 1
                },
                new Sensor
                {
                    Id = "B101_DI000_07_HR01",
                    Type = SensorType.Humidity,
                    Provider = SensorProvider.Scadadata,
                    Latitude = 55.7868413016794,
                    Longitude = 12.5231868449781,
                    ZLevel = 1
                },
                new Sensor
                {
                    Id = "B101_DI000_07_SR01",
                    Type = SensorType.Sound,
                    Provider = SensorProvider.Scadadata,
                    Latitude = 55.7868413016794,
                    Longitude = 12.5231868449781,
                    ZLevel = 1
                },
                new Sensor
                {
                    Id = "B101_DI000_07_LX01",
                    Type = SensorType.Light,
                    Provider = SensorProvider.Scadadata,
                    Latitude = 55.7868413016794,
                    Longitude = 12.5231868449781,
                    ZLevel = 1
                },
                new Sensor
                {
                    Id = "B101_DI000_00_OP01",
                    Type = SensorType.AvailableSeats,
                    Provider = SensorProvider.Scadadata,
                    Latitude = 55.7868413016794,
                    Longitude = 12.5231868449781,
                    ZLevel = 1
                },
                new Sensor
                {
                    Id = "B101_DI000_00_OP01_MAX",
                    Type = SensorType.MaxAvailableSeats,
                    Provider = SensorProvider.Scadadata,
                    Latitude = 55.7868413016794,
                    Longitude = 12.5231868449781,
                    ZLevel = 1
                }
            };
            context.Sensors.AddRange(sensors);
            context.SaveChanges();
        }
        
        public static void SeedLocations(RoomLocatorContext context)
        {
            // Should be removed when all sections are defined and created
            if (context.Locations.Count() < 2)
            {
                var locations = context.Locations;
                context.RemoveRange(locations);
                context.SaveChanges();
            }
            

            if (!context.Locations.Any())
            {
                // Library location
                var library = new Location
                {
                    Name = "Bibliotek",
                    Zoom = 19.1,
                    Longitude = 12.5233335,
                    Latitude = 55.7868826
                };

                var skylab = new Location
                {
                    Name = "Skylab",
                    Zoom = 19.7,
                    Longitude = 12.512870194188451,
                    Latitude = 55.781780056860384
                };

                context.Add(library);
                context.Add(skylab);
                context.SaveChanges();

                /* ######################
                 * Begin library sections 
                 * ###################### */

                // Test sections on ground floor
                var Section1 = new Section
                {
                    LocationId = library.Id,
                    ZLevel = 1,
                };

                var Section2 = new Section
                {
                    LocationId = library.Id,
                    ZLevel = 1,
                };

                // All sections on first floor
                var Section3 = new Section
                {
                    LocationId = library.Id,
                    ZLevel = 2,
                    Type = SectionType.LOUNGE
                };

                var Section4 = new Section
                {
                    LocationId = library.Id,
                    ZLevel = 2,
                    Type = SectionType.DATABAR
                };

                var Section5 = new Section
                {
                    LocationId = library.Id,
                    ZLevel = 2,
                    Type = SectionType.GROUP_STUDY
                };

                var Section6 = new Section
                {
                    LocationId = library.Id,
                    ZLevel = 2,
                    Type = SectionType.DATABAR
                };

                var Section7 = new Section
                {
                    LocationId = library.Id,
                    ZLevel = 2,
                    Type = SectionType.GROUP_STUDY
                };

                var Section8 = new Section
                {
                    LocationId = library.Id,
                    ZLevel = 2,
                    Type = SectionType.GROUP_STUDY
                };

                var Section9 = new Section
                {
                    LocationId = library.Id,
                    ZLevel = 2,
                    Type = SectionType.GROUP_STUDY

                };

                context.Add(Section1);
                context.Add(Section2);
                context.Add(Section3);
                context.Add(Section4);
                context.Add(Section5);
                context.Add(Section6);
                context.Add(Section7);
                context.Add(Section8);
                context.Add(Section9);
                context.SaveChanges();

                var coordinates = new List<Coordinates>
                {
                    // Test section 1
                    new Coordinates
                    {
                        SectionId = Section1.Id,
                        Longitude = 12.523394936706552,
                        Latitude = 55.787009187536114,
                        Index = 0
                    },
                    new Coordinates
                    {
                        SectionId = Section1.Id,
                        Longitude = 12.52317152962101,
                        Latitude = 55.78704845759867,
                        Index = 1
                    },
                    new Coordinates
                    {
                        SectionId = Section1.Id,
                        Longitude = 12.523106783718134,
                        Latitude = 55.78693157515275,
                        Index = 2

                    },
                    new Coordinates
                    {
                        SectionId = Section1.Id,
                        Longitude = 12.523328398202466,
                        Latitude = 55.78689276418302,
                        Index = 3
                    },
                    new Coordinates
                    {
                        SectionId = Section1.Id,
                        Longitude = 12.523394936706552,
                        Latitude = 55.787009187536114,
                        Index = 4
                    },

                    // Test section 2
                    new Coordinates
                    {
                        SectionId = Section2.Id,
                        Longitude = 12.523259637399121,
                        Latitude = 55.786766267668895,
                        Index = 0
                    },
                    new Coordinates
                    {
                        SectionId = Section2.Id,
                        Longitude = 12.523328398202466,
                        Latitude = 55.78689276418302,
                        Index = 1
                    },
                    new Coordinates
                    {
                        SectionId = Section2.Id,
                        Longitude = 12.523106783718134,
                        Latitude = 55.78693157515275,
                        Index = 2
                    },
                    new Coordinates
                    {
                        SectionId = Section2.Id,
                        Longitude = 12.523038473405279,
                        Latitude = 55.78680352605781,
                        Index = 3
                    },
                    new Coordinates
                    {
                        SectionId = Section2.Id,
                        Longitude = 12.523259637399121,
                        Latitude = 55.786766267668895,
                        Index = 4
                    },

                    // 1st floor corner lounge
                    new Coordinates
                    {
                        SectionId = Section3.Id,
                        Longitude = 12.522992396101245,
                        Latitude = 55.78708067483939,
                        Index = 0
                    },
                    new Coordinates
                    {
                        SectionId = Section3.Id,
                        Longitude = 12.52296211062415,
                        Latitude = 55.787025664233454,
                        Index = 1
                    },
                    new Coordinates
                    {
                        SectionId = Section3.Id,
                        Longitude = 12.523103078712865,
                        Latitude = 55.78700090057603,
                        Index = 2
                    },
                    new Coordinates
                    {
                        SectionId = Section3.Id,
                        Longitude = 12.523133512544206,
                        Latitude = 55.787056448768766,
                        Index = 3
                    },
                    new Coordinates
                    {
                        SectionId = Section3.Id,
                        Longitude = 12.522992396101245,
                        Latitude = 55.78708067483939,
                        Index = 4
                    },

                    // 1st floor databar 1
                    new Coordinates
                    {
                        SectionId = Section4.Id,
                        Longitude = 12.523070611196516,
                        Latitude = 55.78688036166568,
                        Index = 0
                    },

                    new Coordinates
                    {
                        SectionId = Section4.Id,
                        Longitude = 12.523035424992003,
                        Latitude = 55.7868150048902,
                        Index = 1
                    },


                    new Coordinates
                    {
                        SectionId = Section4.Id,
                        Longitude = 12.523057303704121,
                        Latitude = 55.786811200586556,
                        Index = 2
                    },


                    new Coordinates
                    {
                        SectionId = Section4.Id,
                        Longitude = 12.523052804076087,
                        Latitude = 55.78680270866491,
                        Index = 3
                    },

                    new Coordinates
                    {
                        SectionId = Section4.Id,
                        Longitude = 12.523442220398778,
                        Latitude = 55.78673536458402,
                        Index = 4
                    },

                    new Coordinates
                    {
                        SectionId = Section4.Id,
                        Longitude = 12.523482255351013,
                        Latitude = 55.78680885135134,
                        Index = 5
                    },

                    new Coordinates
                    {
                        SectionId = Section4.Id,
                        Longitude = 12.523070611196516,
                        Latitude = 55.78688036166568,
                        Index = 6
                    },

                    // 1st floor group study 1
                    new Coordinates
                    {
                        SectionId = Section5.Id,
                        Longitude = 12.523482255351013,
                        Latitude = 55.78680885135134,
                        Index = 0
                    },

                    new Coordinates
                    {
                        SectionId = Section5.Id,
                        Longitude = 12.523447000092318,
                        Latitude = 55.786743509263715,
                        Index = 1
                    },

                    new Coordinates
                    {
                        SectionId = Section5.Id,
                        Longitude = 12.523641059141397,
                        Latitude = 55.78670979806952,
                        Index = 2
                    },

                    new Coordinates
                    {
                        SectionId = Section5.Id,
                        Longitude = 12.52374629327332,
                        Latitude = 55.786901383480824,
                        Index = 3
                    },

                    new Coordinates
                    {
                        SectionId = Section5.Id,
                        Longitude = 12.523572349344818,
                        Latitude = 55.78693143358913,
                        Index = 4
                    },

                    new Coordinates
                    {
                        SectionId = Section5.Id,
                        Longitude = 12.523502485736486,
                        Latitude = 55.78680528200479,
                        Index = 5
                    },

                    new Coordinates
                    {
                        SectionId = Section5.Id,
                        Longitude = 12.523482255351013,
                        Latitude = 55.78680885135134,
                        Index = 6
                    },

                    // 1st floor databar 2
                    new Coordinates
                    {
                        SectionId = Section6.Id,
                        Longitude = 12.523381586623486,
                        Latitude = 55.787013378777004,
                        Index = 0
                    },
                    new Coordinates
                    {
                        SectionId = Section6.Id,
                        Longitude = 12.523356818522757,
                        Latitude = 55.78696882184383,
                        Index = 1
                    },
                    new Coordinates
                    {
                        SectionId = Section6.Id,
                        Longitude = 12.523572349344818,
                        Latitude = 55.78693143358913,
                        Index = 2
                    },
                    new Coordinates
                    {
                        SectionId = Section6.Id,
                        Longitude = 12.52374629327332,
                        Latitude = 55.786901383480824,
                        Index = 3
                    },
                    new Coordinates
                    {
                        SectionId = Section6.Id,
                        Longitude = 12.52377028372436,
                        Latitude = 55.78694572525518,
                        Index = 4
                    },
                    new Coordinates
                    {
                        SectionId = Section6.Id,
                        Longitude = 12.523381586623486,
                        Latitude = 55.787013378777004,
                        Index = 5
                    },

                    // 1st floor group study 2
                    new Coordinates
                    {
                        SectionId = Section7.Id,
                        Longitude = 12.523356818522757,
                        Latitude = 55.78696882184383,
                        Index = 0
                    },
                    new Coordinates
                    {
                        SectionId = Section7.Id,
                        Longitude = 12.523339474902428,
                        Latitude = 55.78693753051286,
                        Index = 1
                    },
                    new Coordinates
                    {
                        SectionId = Section7.Id,
                        Longitude = 12.5235550453948,
                        Latitude = 55.78689997276197,
                        Index = 2
                    },
                    new Coordinates
                    {
                        SectionId = Section7.Id,
                        Longitude = 12.5235550453948,
                        Latitude = 55.78689997276197,
                        Index = 3
                    },
                    new Coordinates
                    {
                        SectionId = Section7.Id,
                        Longitude = 12.523572349344818,
                        Latitude = 55.78693143358913,
                        Index = 4
                    },
                    new Coordinates
                    {
                        SectionId = Section7.Id,
                        Longitude = 12.523356818522757,
                        Latitude = 55.78696882184383,
                        Index = 5
                    },

                    // 1st floor group study 3
                    new Coordinates
                    {
                        SectionId = Section8.Id,
                        Longitude = 12.523133512544206,
                        Latitude = 55.787056448768766,
                        Index = 0
                    },
                    new Coordinates
                    {
                        SectionId = Section8.Id,
                        Longitude = 12.523091692964897,
                        Latitude = 55.78698056999656,
                        Index = 1
                    },
                    new Coordinates
                    {
                        SectionId = Section8.Id,
                        Longitude = 12.523339474902428,
                        Latitude = 55.78693753051286,
                        Index = 2
                    },
                    new Coordinates
                    {
                        SectionId = Section8.Id,
                        Longitude = 12.523381586623486,
                        Latitude = 55.787013378777004,
                        Index = 3
                    },
                    new Coordinates
                    {
                        SectionId = Section8.Id,
                        Longitude = 12.523133512544206,
                        Latitude = 55.787056448768766,
                        Index = 4
                    },

                    // 1st floor group study 4
                    new Coordinates
                    {
                        SectionId = Section9.Id,
                        Longitude = 12.52296211062415,
                        Latitude = 55.787025664233454,
                        Index = 0
                    },
                    new Coordinates
                    {
                        SectionId = Section9.Id,
                        Longitude = 12.522862484572613,
                        Latitude = 55.786844942921476,
                        Index = 1
                    },
                    new Coordinates
                    {
                        SectionId = Section9.Id,
                        Longitude = 12.523035424992003,
                        Latitude = 55.7868150048902,
                        Index = 2
                    },
                    new Coordinates
                    {
                        SectionId = Section9.Id,
                        Longitude = 12.523122460328437,
                        Latitude = 55.786975109109136,
                        Index = 3
                    },
                    new Coordinates
                    {
                        SectionId = Section9.Id,
                        Longitude = 12.523091692964897,
                        Latitude = 55.78698056999656,
                        Index = 4
                    },
                    new Coordinates
                    {
                        SectionId = Section9.Id,
                        Longitude = 12.523103078712865,
                        Latitude = 55.78700090057603,
                        Index = 5
                    },
                    new Coordinates
                    {
                        SectionId = Section9.Id,
                        Longitude = 12.52296211062415,
                        Latitude = 55.787025664233454,
                        Index = 6
                    },

                };

                context.AddRange(coordinates);
                context.SaveChanges();
            }
        }

        public static void SeedDemoSurveys(RoomLocatorContext context)
        {
            if (!context.Surveys.Any())
            {
                var loremIpsum = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Duis ut diam quam nulla porttitor massa id neque aliquam.";
                var survey1 = new Survey { Title = "Lorem ipsum dolor sit amet", Description = loremIpsum, CreatedDate = DateTime.Now };
                var survey2 = new Survey { Title = "Dolor purus non enim praesent elementum", Description = loremIpsum, CreatedDate = DateTime.Now };

                context.Add(survey1);
                context.Add(survey2);
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

                var Sections = context.Sections.ToList();

                // Even distribution between the two test surveys
                var i = 0;
                foreach (var section in Sections)
                {
                    if (i < Sections.Count() / 2)
                    {
                        section.SurveyId = survey1.Id;
                    }
                    else
                    {
                        section.SurveyId = survey2.Id;
                    }

                    i++;
                }

                context.AddRange(questions);
                context.UpdateRange(Sections);
                context.SaveChanges();
            }
        }
    }
}
