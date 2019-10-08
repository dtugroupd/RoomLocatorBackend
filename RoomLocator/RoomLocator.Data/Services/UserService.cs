using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
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
            var users = await _context.Users.ToListAsync();

//            var userViewModels = new List<UserViewModel>();
//            foreach (var user in users)
//            {
//                userViewModels.Add(new UserViewModel() { StudentId = user.StudentId });
//            }
            var userViewModels = _mapper.Map<UserViewModel[]>(users);

            return userViewModels;
        }

        public async Task<UserViewModel> Create(UserInputModel input)
        {
            var user = _mapper.Map<User>(input);

//            var userExists = await _context.Users
//                .Where(x => x.StudentId == input.StudentId)
//                .AnyAsync();
            var userExists = await _context.Users
                .AnyAsync(x => x.StudentId == input.StudentId);

            if (userExists)
            {
                throw DuplicateException.DuplicateEntry<User>();
            }

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return _mapper.Map<UserViewModel>(user);
        }
    }
}