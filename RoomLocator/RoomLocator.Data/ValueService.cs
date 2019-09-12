using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using RoomLocator.Domain;
using RoomLocator.Domain.InputModels;
using RoomLocator.Domain.ViewModels;

namespace RoomLocator.Data
{
    public class ValueService
    {
        private readonly IMapper _mapper;

        public ValueService(IMapper mapper)
        {
            _mapper = mapper;
        }

        private static IEnumerable<Value> _values = new Value[]
        {
            Value.Create("First value"),
            Value.Create("Second value"),
            Value.Create("This is another value")
        };

        public bool TryGetValue(string id, out ValueViewModel value)
        {
            value = _mapper.Map<ValueViewModel>(_values.FirstOrDefault(x => x.Id == id));
            return value != null;
        }

        public IEnumerable<ValueViewModel> Get()
        {
            return _mapper.Map<ValueViewModel[]>(_values);
        }

        public ValueViewModel Create(ValueInputModel valueToCreate)
        {
            var value = Value.Create(valueToCreate.Text);

            return _mapper.Map<ValueViewModel>(value);
        }
    }
}
