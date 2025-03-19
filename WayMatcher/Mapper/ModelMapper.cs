using AutoMapper;
using WayMatcherBL.Models;
using WayMatcherBL.LogicModels;
using WayMatcherBL.DtoModels;

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
                cfg.CreateMap<VehicleMapping, VehicleMappingDto>();
                cfg.CreateMap<VehicleMappingDto, VehicleMapping>();
                cfg.CreateMap<Stop, StopDto>();
                cfg.CreateMap<StopDto, Stop>();
                cfg.CreateMap<Invite, InviteDto>();
                cfg.CreateMap<InviteDto, Invite>();
                cfg.CreateMap<EventMember, EventMemberDto>();
                cfg.CreateMap<EventMemberDto, EventMember>();
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

        public VehicleMappingDto ConvertVehicleMappingToDto(VehicleMapping vehicle)
        {
            return _mapper.Map<VehicleMappingDto>(vehicle);
        }
        public VehicleMapping ConvertVehicleMappingDtoToEntity(VehicleMappingDto vehicleDto)
        {
            return _mapper.Map<VehicleMapping>(vehicleDto);
        }

        public StopDto ConvertStopToDto(Stop stop)
        {
            return _mapper.Map<StopDto>(stop);
        }
        public Stop ConvertStopDtoToEntity(StopDto stopDto)
        {
            return _mapper.Map<Stop>(stopDto);
        }

        public InviteDto ConvertInviteToDto(Invite invite)
        {
            return _mapper.Map<InviteDto>(invite);
        }
        public Invite ConvertInviteDtoToEntity(InviteDto inviteDto)
        {
            return _mapper.Map<Invite>(inviteDto);
        }

        public EventMemberDto ConvertEventMemberToDto(EventMember eventMember)
        {
            return _mapper.Map<EventMemberDto>(eventMember);
        }

        public EventMember ConvertEventMemberDtoToEntity(EventMemberDto eventMemberDto)
        {
            return _mapper.Map<EventMember>(eventMemberDto);
        }
    }
}
