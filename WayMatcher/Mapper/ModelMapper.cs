using AutoMapper;
using WayMatcherBL.Models;
using WayMatcherBL.LogicModels;

namespace WayMatcherBL.Mapper
{
    public class ModelMapper : Profile
    {
        private readonly IMapper _mapper;

        public ModelMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Address, AddressDto>();
                cfg.CreateMap<AddressDto, Address>();
            });
            _mapper = config.CreateMapper();
        }

        public AddressDto ConvertToDto(Address address)
        {
            return _mapper.Map<AddressDto>(address);
        }

        public Address ConvertToEntity(AddressDto addressDto)
        {
            return _mapper.Map<Address>(addressDto);
        }
    }
}
