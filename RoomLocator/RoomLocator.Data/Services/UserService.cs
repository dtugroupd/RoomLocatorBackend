using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using RoomLocator.Data.Config;
using RoomLocator.Domain;
using RoomLocator.Domain.Models;
using RoomLocator.Domain.ViewModels;
using Shared;
using System;

namespace RoomLocator.Data.Services
{
    /// <summary>
    ///     <author>Anders Wiberg Olsen, s165241</author>
    /// </summary>
    public class UserService : BaseService
    {
        protected DbContextOptions<RoomLocatorContext> Options;

        public UserService(RoomLocatorContext context, IMapper mapper) : base(context, mapper) { }

        public async Task<IEnumerable<UserViewModel>> Get()
        {
            var users = await _context.Users
                .Include(x => x.UserRoles)
                    .ThenInclude(x => x.Role)
                    .Where(x => !x.UserIsDeleted)
                .ProjectTo<UserViewModel>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return users;
        }

        public async Task<UserViewModel> Get(string id)
        {
            var user = await _context.Users
                .Include(x => x.UserRoles)
                    .ThenInclude(x => x.Role)
                .ProjectTo<UserViewModel>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(x => x.Id == id);

            return user;
        }

        public async Task<UserViewModel> GetByStudentId(string studentId)
        {
            var user = await _context.Users
                .Include(x => x.UserRoles)
                    .ThenInclude(x => x.Role)
                .ProjectTo<UserViewModel>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(x => x.StudentId == studentId);

            return user;
        }

        public async Task<UserViewModel> Create(string studentId, bool hasAcceptedDisclaimer)
        {
            var user = new User {StudentId = studentId};

            var userExists = await _context.Users
                .AnyAsync(x => x.StudentId == studentId);

            if (userExists)
            {
                throw DuplicateException.DuplicateEntry<User>();
            }
            
            if (!hasAcceptedDisclaimer) throw new InvalidRequestException("Disclaimer Not Accepted", "You have to accept the disclaimer in order to register for the service");

            await _context.Users.AddAsync(user);
            await _context.UserDisclaimers.AddAsync(new UserDisclaimer(user.Id, true));

            var userCount = await _context.Users.CountAsync();
            var roleName = userCount == 0 ? "admin" : "student";

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

        /// <summary>
        ///     <author>Hadi Horani, s144885</author>
        /// </summary>
        public async Task<UserViewModel> UpdateRole(string studentId, string roleName)
        {

            var userExists = await _context.Users
               .AnyAsync(x => x.StudentId == studentId);

            if (!userExists)
            {
                throw NotFoundException.NotExistsWithProperty<User>(x => x.StudentId, studentId);
            }

            var user = await GetByStudentId(studentId);

            var userRoles = await _context.UserRoles
                .Where(x => x.UserId == user.Id)
                .FirstOrDefaultAsync();

            if (userRoles != null)
            {
                _context.UserRoles.Remove(userRoles);
                await _context.SaveChangesAsync();
            }

            var roleId = await _context.Roles
                .Where(x => x.Name == roleName)
                .Select(x => x.Id)
                .FirstOrDefaultAsync();

            if (roleId == null)
            {
                throw NotFoundException.NotExistsWithProperty<Role>(x => x.Name, roleName);
            }

            var userRoleExists = await _context.UserRoles
                .Where(x => x.UserId == user.Id)
                .Where(x => x.RoleId == roleId)
                .AnyAsync();

            if (userRoleExists)
            {
                throw DuplicateException.DuplicateEntry<Role>();
            }

            var studentUserRole = new UserRole
            {
                UserId = user.Id,
                RoleId = roleId
            };

            await _context.UserRoles.AddAsync(studentUserRole);
            await _context.SaveChangesAsync();

            return await Get(user.Id);
        }

        /// <summary>
        ///     <author>Hadi Horani, s144885</author>
        /// </summary>
        public async Task<UserViewModel> DeleteUserInfo(string studentId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.StudentId == studentId);

            if (user == null)
            {
                throw NotFoundException.NotExistsWithProperty<User>(x => x.StudentId, user.StudentId);
            }

            user.ProfileImage = null;
            user.FirstName = null;
            user.LastName = null;
            user.Email = null;
            user.UserIsDeleted = true;
            user.StudentId = Guid.NewGuid().ToString();

            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            return await Get(user.Id);
        }

        public async Task<UserViewModel> GetOrCreate(CnUserViewModel model, bool? hasAcceptedDisclaimer)
        {
            var userToCreate = _mapper.Map<User>(model);

            var existingUser = await GetByStudentId(userToCreate.StudentId);

            if (existingUser != null) return existingUser;
            if (!hasAcceptedDisclaimer ?? false) throw new InvalidRequestException("Disclaimer Not Accepted", "You have to accept the disclaimer in order to register for the service");
            
            await _context.Users.AddAsync(userToCreate);
            await _context.UserDisclaimers.AddAsync(new UserDisclaimer(userToCreate.Id, true));
            
            var userCount = await _context.Users.CountAsync();
            var roleName = userCount == 0 ? "admin" : "student";

            var studentRoleId = await _context.Roles
                .Where(x => x.Name == roleName)
                .Select(x => x.Id)
                .FirstOrDefaultAsync();
            await _context.UserRoles.AddAsync(new UserRole {UserId = userToCreate.Id, RoleId = studentRoleId});
            await _context.SaveChangesAsync();

            return await GetByStudentId(model.UserName);
        }

        public async Task<UserDisclaimerViewModel> HasAcceptedDisclaimer(string studentId)
        {
            var user = await _context.Users
                .Include(x => x.UserDisclaimer)
                .Where(x => !x.UserIsDeleted)
                .Where(x => x.StudentId == studentId)
                .FirstOrDefaultAsync();
            
            if (user?.UserDisclaimer == null) return UserDisclaimerViewModel.NotAccepted();

            return new UserDisclaimerViewModel(user.UserDisclaimer.HasAcceptedDisclaimer);
        }

        public async Task<UserDisclaimerViewModel> AcceptDisclaimer(string studentId)
        {
            var user = await _context.Users
                .Include(x => x.UserDisclaimer)
                .Where(x => !x.UserIsDeleted)
                .Where(x => x.StudentId == studentId)
                .FirstOrDefaultAsync();
            
            if (user == null) return UserDisclaimerViewModel.NotAccepted();
            if (user?.UserDisclaimer.HasAcceptedDisclaimer ?? false) return UserDisclaimerViewModel.Accepted();

            await _context.UserDisclaimers.AddAsync(new UserDisclaimer(user.Id, true));
            await _context.SaveChangesAsync();
            
            return UserDisclaimerViewModel.Accepted();
        }
    }
}
