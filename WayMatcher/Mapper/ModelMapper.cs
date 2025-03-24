using AutoMapper;
using WayMatcherBL.Models;
using WayMatcherBL.LogicModels;
using WayMatcherBL.DtoModels;

namespace WayMatcherBL.Mapper
{
    public class ModelMapper : Profile
    {
        private readonly IMapper _mapper;
        private readonly IDatabaseService _databaseService;

        public ModelMapper(IDatabaseService databaseService)
        {
            _databaseService = databaseService;

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Address, AddressDto>();
                cfg.CreateMap<AddressDto, Address>();
                cfg.CreateMap<Event, EventDto>();
                cfg.CreateMap<VwPilotEvent, EventDto>();
                cfg.CreateMap<VwPassengerEvent, EventDto>();
                cfg.CreateMap<EventDto, Event>();
                cfg.CreateMap<Schedule, ScheduleDto>();
                cfg.CreateMap<ScheduleDto, Schedule>();
                cfg.CreateMap<User, UserDto>()
                    .ForMember(dest => dest.Address, opt => opt.MapFrom(src => _databaseService.GetAddress(new AddressDto { AddressId = src.Address.AddressId })));
                cfg.CreateMap<UserDto, User>()
                    .ForMember(dest => dest.AddressId, opt => opt.MapFrom(src => src.Address.AddressId));
                cfg.CreateMap<Vehicle, VehicleDto>();
                cfg.CreateMap<VehicleDto, Vehicle>();
                cfg.CreateMap<VehicleMapping, VehicleMappingDto>();
                cfg.CreateMap<VehicleMappingDto, VehicleMapping>();
                cfg.CreateMap<Stop, StopDto>()
                    .ForMember(dest => dest.Address, opt => opt.MapFrom(src => _databaseService.GetAddress(new AddressDto { AddressId = src.Address.AddressId })));
                cfg.CreateMap<StopDto, Stop>()
                    .ForMember(dest => dest.AddressId, opt => opt.MapFrom(src => src.Address.AddressId));
                cfg.CreateMap<Invite, InviteDto>();
                cfg.CreateMap<InviteDto, Invite>();
                cfg.CreateMap<EventMember, EventMemberDto>();
                cfg.CreateMap<EventMemberDto, EventMember>();
                cfg.CreateMap<ChatMessage, ChatMessageDto>();
                cfg.CreateMap<ChatMessageDto, ChatMessage>();
                cfg.CreateMap<Rating, RatingDto>();
                cfg.CreateMap<RatingDto, Rating>();
                cfg.CreateMap<Notification, NotificationDto>();
                cfg.CreateMap<NotificationDto, Notification>();
                cfg.CreateMap<AuditDto, Audit>();
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
        public EventDto ConvertVwPilotEventToDto(VwPilotEvent vwPilot)
        {
            return _mapper.Map<EventDto>(vwPilot);
        }
        public EventDto ConvertVwPassengerEventToDto(VwPassengerEvent vwPassenger)
        {
            return _mapper.Map<EventDto>(vwPassenger);
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

        public ChatMessageDto ConvertChatMessageToDto(ChatMessage chatMessage)
        {
            return _mapper.Map<ChatMessageDto>(chatMessage);
        }
        public ChatMessage ConvertChatMessageDtoToEntity(ChatMessageDto chatMessageDto)
        {
            return _mapper.Map<ChatMessage>(chatMessageDto);
        }

        public RatingDto ConvertRatingToDto(Rating rating)
        {
            return _mapper.Map<RatingDto>(rating);
        }
        public Rating ConvertRatingDtoToEntity(RatingDto ratingDto)
        {
            return _mapper.Map<Rating>(ratingDto);
        }
        public NotificationDto ConvertNotificationToDto(Notification notification)
        {
            return _mapper.Map<NotificationDto>(notification);
        }
        public Notification ConvertNotificationDtoToEntity(NotificationDto notificationDto)
        {
            return _mapper.Map<Notification>(notificationDto);
        }
        public Audit ConvertAuditDtoToEntity(AuditDto auditDto)
        {
            return _mapper.Map<Audit>(auditDto);
        }
    }
}
