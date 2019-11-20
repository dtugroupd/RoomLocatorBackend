using System;
using System.Collections.Generic;
using System.Linq;
using RoomLocator.Domain;
using RoomLocator.Domain.Models;
using RoomLocator.Domain.ViewModels;
using Xunit;

namespace RoomLocator.UnitTest.Mappings
{
    public class UserMappingRoleTests : BaseTest
    {
        [Fact]
        public void MapUserToUserViewModel_MapsRoles()
        {
            var studentRole = new Role
            {
                Name = "student"
            };
            var adminRole = new Role
            {
                Name = "admin"
            };

            var user = new User
            {
                Id = Guid.NewGuid().ToString(),
                StudentId = "s123456",
                UserRoles = new List<UserRole>
                {
                    new UserRole
                    {
                        Role = studentRole
                    },
                    new UserRole()
                    {
                        Role = adminRole
                    }
                }
            };

            var mappedUser = Mapper.Map<UserViewModel>(user);
            
            Assert.Equal(2, mappedUser.Roles.Count());

            Assert.Contains(studentRole.Name, mappedUser.Roles);
            Assert.Contains(adminRole.Name, mappedUser.Roles);
        }
    }
}
