using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RoomLocator.Data.Config;
using RoomLocator.Data.Services;
using RoomLocator.Domain;
using RoomLocator.Domain.Models;
using Xunit;

namespace RoomLocator.UnitTest.Services
{
    public class UserServiceTests : BaseTest
    {
        [Fact]
        public async Task Create_CreatesTheUser_AssignsStudentRole()
        {
            var studentId = "s123456";
            var expectedRole = "student";

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
    }
}
