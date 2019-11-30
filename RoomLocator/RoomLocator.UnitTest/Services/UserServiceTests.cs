using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RoomLocator.Data.Config;
using RoomLocator.Data.Services;
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

            UserViewModel createdUser = null;

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
    }
}
