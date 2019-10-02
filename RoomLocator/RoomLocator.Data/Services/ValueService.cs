using System.Collections.Generic;
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
    public class ValueService : BaseService
    {
        public ValueService(RoomLocatorContext context, IMapper mapper) : base(context, mapper) { }
        
        public async Task<ValueViewModel> Get(string id)
        {
            var value = await _context.Values
                .ProjectTo<ValueViewModel>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (value == null) throw NotFoundException.NotExistsWithProperty<Value>(x => x.Id, id);

            return value;
        }

        public Task<List<ValueViewModel>> Get()
        {
            return _context.Values
                .ProjectTo<ValueViewModel>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<ValueViewModel> Create(ValueInputModel valueToCreate)
        {
            var value = await _context.Values.FirstOrDefaultAsync(x => x.Text == valueToCreate.Text);
            
            if (value != null) throw DuplicateException.DuplicateEntry<Value>();

            var createdValue = await _context.Values.AddAsync(_mapper.Map<Value>(valueToCreate));
            await _context.SaveChangesAsync();

            return _mapper.Map<ValueViewModel>(createdValue.Entity);
        }

        public async Task<ValueViewModel> Update(string id, ValueInputModel value)
        {
            var valueToUpdate = await _context.Values.FirstOrDefaultAsync(x => x.Id == id);
            valueToUpdate.Text = value.Text;
            await _context.SaveChangesAsync();

            return _mapper.Map<ValueViewModel>(valueToUpdate);
        }

        public async Task Delete(string id)
        {
            var value = await _context.Values
                .FirstOrDefaultAsync(x => x.Id == id);

            if (value == null) return;

            _context.Values.Remove(value);
            await _context.SaveChangesAsync();
        }
    }
}
