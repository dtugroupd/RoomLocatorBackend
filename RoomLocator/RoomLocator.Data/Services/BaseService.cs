using AutoMapper;
using RoomLocator.Data.Config;

namespace RoomLocator.Data.Services
{
    public abstract class BaseService
    {
        protected readonly RoomLocatorContext _context;
        protected readonly IMapper _mapper;

        protected BaseService(RoomLocatorContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
    }
}
