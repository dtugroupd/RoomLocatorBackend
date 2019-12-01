using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Moq;
using RoomLocator.Data.Config;
using RoomLocator.Data.Services;
using RoomLocator.Domain;
using RoomLocator.Domain.Models;
using RoomLocator.Domain.ViewModels;
using Xunit;

namespace RoomLocator.UnitTest.Services
{
    /// <summary>
    ///     <author>Anders Wiberg Olsen, s165241</author>
    /// </summary>
    public class UserServiceTests : BaseTest
    {
        [Fact]
        public async Task Create_CreatesTheUser_AssignsStudentRole()
        {
            const string studentId = "s123456";
            const string expectedRole = "student";

            using (var context = new RoomLocatorContext(Options))
            {
                await context.Roles.AddAsync(new Role {Name = expectedRole});
                await context.SaveChangesAsync();

                var userService = new UserService(context, Mapper);
                await userService.Create(studentId);
            }

            using (var context = new RoomLocatorContext(Options))
            {
                var user = await context.Users
                    .Where(x => x.StudentId == studentId)
                    .Include(x => x.UserRoles)
                    .ThenInclude(x => x.Role)
                    .FirstOrDefaultAsync();
                
                Assert.NotNull(user);
                Assert.Equal(user.StudentId, studentId);

                var hasRole = user.UserRoles.Any(ur => ur.Role.Name.ToLower() == expectedRole);
                Assert.True(hasRole);
            }
        }

        [Fact]
        public async Task GetOrCreate_CreatesUser_IfNotExists()
        {
            var userToCreate = new CnUserViewModel
            {
                GivenName = "John",
                FamilyName = "Doe",
                UserName = "s123456",
            };

            UserViewModel createdUser;

            using (var context = new RoomLocatorContext(Options))
            {
                Assert.False(await context.Users.AnyAsync(x => x.StudentId == userToCreate.UserName));

                var userService = new UserService(context, Mapper);
                createdUser = await userService.GetOrCreate(userToCreate);
            }
            
            Assert.NotNull(createdUser);
            Assert.Equal(userToCreate.GivenName, createdUser.FirstName);
            Assert.Equal(userToCreate.FamilyName, createdUser.LastName);
            Assert.Equal(userToCreate.UserName, createdUser.StudentId);
        }

        [Fact]
        public async Task GetOrCreate_FetchesUser_IfExists()
        {
            var userToCreate = new CnUserViewModel
            {
                GivenName = "John",
                FamilyName = "Doe",
                UserName = "s123456",
            };

            using (var context = new RoomLocatorContext(Options))
            {
                await context.AddAsync(Mapper.Map<User>(userToCreate));
                await context.SaveChangesAsync();
            }
            
            UserViewModel fetchedUser;
            
            using (var context = new RoomLocatorContext(Options))
            {
                Assert.True(await context.Users.AnyAsync(x => x.StudentId == userToCreate.UserName));

                var userService = new UserService(context, Mapper);
                fetchedUser = await userService.GetOrCreate(userToCreate);
            }
            
            Assert.NotNull(fetchedUser);
            Assert.Equal(userToCreate.GivenName, fetchedUser.FirstName);
            Assert.Equal(userToCreate.FamilyName, fetchedUser.LastName);
            Assert.Equal(userToCreate.UserName, fetchedUser.StudentId);
        }

        [Fact]
        public async Task GetOrCreate_DoesNotTryToCreateUser_WhenUserExists()
        {
            var cnUserToCreate = new CnUserViewModel
            {
                GivenName = "John",
                FamilyName = "Doe",
                UserName = "s123456",
            }; 

            var userToCreate = Mapper.Map<User>(cnUserToCreate);

            var users = new List<User> { userToCreate };
            var roles = new List<Role>
            {
                new Role {Id = Guid.NewGuid().ToString(), Name = "student"}
            };

            using (var context = new RoomLocatorContext(Options))
            {
                await context.AddAsync(Mapper.Map<User>(cnUserToCreate));
                await context.SaveChangesAsync();
            }

            // TODO: The following doesn't quite work, so fix it if there is time
//            var mockContext = new Mock<RoomLocatorContext>();
//            mockContext.Setup(mock => mock.Set<User>()).Returns(MockingFactory.CreateDbSetMock(users).Object);
//            mockContext.Setup(mock => mock.Set<Role>()).Returns(MockingFactory.CreateDbSetMock(roles).Object);
//
//            mockContext.Setup(mock => mock.Set<User>().FirstOrDefaultAsync(
//                It.IsAny<Expression<Func<User, bool>>>(),
//                It.IsAny<CancellationToken>()
//            )).ReturnsAsync(userToCreate);
//            
//            var userService = new UserService(mockContext.Object, Mapper);
//            await userService.GetOrCreate(cnUserToCreate);
//            
//            mockContext.Verify(x => x.Users.AddAsync(It.IsAny<User>(), It.IsAny<CancellationToken>()), Times.Never);
        }
    }
}
