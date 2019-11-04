using AutoMapper;
using RoomLocator.Data.Config;

namespace RoomLocator.Data.Services
{
    public class ModcamService: BaseService
    {
        public ModcamService(RoomLocatorContext context, IMapper mapper) : base(context, mapper) { }

        
    }
}