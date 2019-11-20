using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using RoomLocator.Data.Config;
using RoomLocator.Domain;
using RoomLocator.Domain.InputModels;
using RoomLocator.Domain.Models;
using RoomLocator.Domain.ViewModels;
using Shared;

namespace RoomLocator.Data.Services
{
    /// <summary>
    ///     <author>Anders Wiberg Olsen, s165241</author>
    /// </summary>
    public class UserService : BaseService
    {
        public UserService(RoomLocatorContext context, IMapper mapper) : base(context, mapper) { }

        public async Task<IEnumerable<UserViewModel>> Get()
        {
            var users = await _context.Users
                .ProjectTo<UserViewModel>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return users;
        }

        public async Task<UserViewModel> Get(string id)
        {
            var user = await _context.Users
                .ProjectTo<UserViewModel>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(x => x.Id == id);

            return await AssignRoles(user);
        }

        public async Task<UserViewModel> GetByStudentId(string studentId)
        {
            var user = await _context.Users
                .Include(x => x.UserRoles)
                    .ThenInclude(x => x.Role)
                .ProjectTo<UserViewModel>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(x => x.StudentId == studentId);

            if (user == null) return null;

            return await AssignRoles(user);
        }

        private async Task<UserViewModel> AssignRoles(UserViewModel user)
        {
            user.Roles = await _context.UserRoles
                .Include(x => x.Role)
                .Where(x => x.UserId == user.Id)
                .Select(x => x.Role.Name)
                .ToListAsync();

            if (user.Roles.Contains("admin"))
            {
                user.Roles = await _context.Roles.Select(x => x.Name).ToListAsync();
            }

            return user;
        }

        public async Task<UserViewModel> Create(string studentId)
        {
            var user = new User {StudentId = studentId};

            var userExists = await _context.Users
                .AnyAsync(x => x.StudentId == studentId);

            if (userExists)
            {
                throw DuplicateException.DuplicateEntry<User>();
            }

            await _context.Users.AddAsync(user);

            var studentRoleId = await _context.Roles
                .Where(x => x.Name == "student")
                .Select(x => x.Id)
                .FirstOrDefaultAsync();

            var studentUserRole = new UserRole
            {
                UserId = user.Id,
                RoleId = studentRoleId
            };

            await _context.UserRoles.AddAsync(studentUserRole);
            await _context.SaveChangesAsync();

            return await Get(user.Id);
        }

        public async Task<UserViewModel> UpdateRole(string studentId, string roleName)
        {
            var user = await GetByStudentId(studentId);

            var studentRoleId = await _context.Roles
                .Where(x => x.Name == roleName)
                .Select(x => x.Id)
                .FirstOrDefaultAsync();

            var studentUserRole = new UserRole
            {
                UserId = user.Id,
                RoleId = studentRoleId
            };

            await _context.UserRoles.AddAsync(studentUserRole);
            await _context.SaveChangesAsync();

            return await Get(user.Id);
        }

        public async Task Delete(string studentId)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(x => x.StudentId == studentId);

            if (user == null) return;

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }
    }
}