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

namespace RoomLocator.Data.Services
{
    public class ValueService : BaseService
    {
        public ValueService(RoomLocatorContext context, IMapper mapper) : base(context, mapper) { }
        
        public Task<ValueViewModel> Get(string id)
        {
            return GetModel<Value, ValueViewModel>(_context.Values, x => x.Id == id);
        }

        public async Task<IEnumerable<ValueViewModel>> Get()
        {
            return _mapper.Map<ValueViewModel[]>(await _context.Values.ToListAsync());
        }

        public Task<ValueViewModel> Create(ValueInputModel valueToCreate)
        {
            return GetCreateModel<ValueInputModel, Value, ValueViewModel>(
                _context.Values, valueToCreate);
        }

        public async Task<ValueViewModel> Update(string id, ValueInputModel value)
        {
            var valueToUpdate = await _context.Values.FirstOrDefaultAsync(x => x.Id == id);
            valueToUpdate.Text = value.Text;
            await _context.SaveChangesAsync();

            return _mapper.Map<ValueViewModel>(valueToUpdate);
            
            // TODO: Refactor to use below method instead
//            return UpdateModel<ValueViewModel, Value, ValueViewModel>(_context.Values, value);
        }

        public Task Delete(string id)
        {
            return DeleteModel<Value>(_context.Values, x => x.Id == id);
        }
    }
}
