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
using RoomLocator.Domain.ViewModels;
using Shared;

namespace RoomLocator.Data.Services
{
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

            return user;
        }

        public async Task<UserViewModel> GetByStudentId(string studentId)
        {
            var user = await _context.Users
                .ProjectTo<UserViewModel>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(x => x.StudentId == studentId);

            return user;
        }

        public async Task<UserViewModel> Create(string studentId, UserInputModel input)
        {
            var user = _mapper.Map<User>(input);
            user.StudentId = studentId;
            user.UserType = input.UserType;

            var userExists = await _context.Users
                .AnyAsync(x => x.StudentId == studentId);

            if (userExists)
            {
                throw DuplicateException.DuplicateEntry<User>();
            }

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return _mapper.Map<UserViewModel>(user);
        }

        public async Task<UserViewModel> Update(string studentId, UserInputModel input)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.StudentId == studentId);

            user.FirstName = input.FirstName;
            user.LastName = input.LastName;
            user.UserType = input.UserType;

            await _context.SaveChangesAsync();

            return _mapper.Map<UserViewModel>(user);
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