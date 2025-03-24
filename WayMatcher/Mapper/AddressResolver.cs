using AutoMapper;
using WayMatcherBL.DtoModels;
using WayMatcherBL.Interfaces;
using WayMatcherBL.LogicModels;
using WayMatcherBL.Models;

namespace WayMatcherBL.Mapper
{
    public class AddressResolver : IValueResolver<User, UserDto, AddressDto>, IValueResolver<Stop, StopDto, AddressDto>
    {
        private readonly IDatabaseService _databaseService;

        public AddressResolver(IDatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

        public AddressDto Resolve(User source, UserDto destination, AddressDto destMember, ResolutionContext context)
        {
            return _databaseService.GetAddress(new AddressDto { AddressId = source.Address.AddressId });
        }

        public AddressDto Resolve(Stop source, StopDto destination, AddressDto destMember, ResolutionContext context)
        {
            return _databaseService.GetAddress(new AddressDto { AddressId = source.Address.AddressId });
        }
    }
}
