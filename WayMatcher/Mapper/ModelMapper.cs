﻿using AutoMapper;
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
                cfg.CreateMap<Event, EventDto>();
                cfg.CreateMap<EventDto, Event>();
                cfg.CreateMap<Schedule, ScheduleDto>();
                cfg.CreateMap<ScheduleDto, Schedule>();
                cfg.CreateMap<User, UserDto>();
                cfg.CreateMap<UserDto, User>();
                cfg.CreateMap<Vehicle, VehicleDto>();
                cfg.CreateMap<VehicleDto, Vehicle>();
            });
            _mapper = config.CreateMapper();
        }

        public AddressDto ConvertAddressToDto(Address address)
        {
            return _mapper.Map<AddressDto>(address);
        }

        public Address ConvertAddressDtoToEntity(AddressDto addressDto)
        {
            return _mapper.Map<Address>(addressDto);
        }

        public EventDto ConvertEventToDto(Event eventItem)
        {
            return _mapper.Map<EventDto>(eventItem);
        }

        public Event ConvertEventDtoToEntity(EventDto eventDto)
        {
            return _mapper.Map<Event>(eventDto);
        }

        public ScheduleDto ConvertScheduleToDto(Schedule schedule)
        {
            return _mapper.Map<ScheduleDto>(schedule);
        }

        public Schedule ConvertScheduleDtoToEntity(ScheduleDto scheduleDto)
        {
            return _mapper.Map<Schedule>(scheduleDto);
        }

        public UserDto ConvertUserToDto(User user)
        {
            return _mapper.Map<UserDto>(user);
        }

        public User ConvertUserDtoToEntity(UserDto userDto)
        {
            return _mapper.Map<User>(userDto);
        }

        public VehicleDto ConvertVehicleToDto(Vehicle vehicle)
        {
            return _mapper.Map<VehicleDto>(vehicle);
        }

        public Vehicle ConvertVehicleDtoToEntity(VehicleDto vehicleDto)
        {
            return _mapper.Map<Vehicle>(vehicleDto);
        }
    }
}
