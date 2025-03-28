using AutoMapper;
using WayMatcherBL.DtoModels;
using WayMatcherBL.LogicModels;
using WayMatcherBL.Models;

namespace WayMatcherBL.Mapper
{
    /// <summary>
    /// Provides mapping configurations and methods for converting between entity and DTO models.
    /// </summary>
    public class ModelMapper : Profile
    {
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="ModelMapper"/> class.
        /// </summary>
        public ModelMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Address, AddressDto>();
                cfg.CreateMap<AddressDto, Address>();
                cfg.CreateMap<Event, EventDto>();
                cfg.CreateMap<VwPilotEvent, EventDto>();
                cfg.CreateMap<VwPassengerEvent, EventDto>();
                cfg.CreateMap<FN_GetEventsByMemberUserIdResult, EventDto>();
                cfg.CreateMap<EventDto, Event>();
                cfg.CreateMap<Schedule, ScheduleDto>();
                cfg.CreateMap<ScheduleDto, Schedule>();
                cfg.CreateMap<User, UserDto>()
                    .ForMember(dest => dest.Address, opt => opt.MapFrom(src => new AddressDto { AddressId = src.Address.AddressId }));
                cfg.CreateMap<UserDto, User>()
                    .ForMember(dest => dest.AddressId, opt => opt.MapFrom(src => src.Address.AddressId));
                cfg.CreateMap<Vehicle, VehicleDto>();
                cfg.CreateMap<VehicleDto, Vehicle>();
                cfg.CreateMap<VehicleMapping, VehicleMappingDto>();
                cfg.CreateMap<VehicleMappingDto, VehicleMapping>();
                cfg.CreateMap<Stop, StopDto>()
                    .ForMember(dest => dest.Address, opt => opt.MapFrom(src => new AddressDto { AddressId = src.Address.AddressId }));
                cfg.CreateMap<StopDto, Stop>()
                    .ForMember(dest => dest.AddressId, opt => opt.MapFrom(src => src.Address.AddressId));
                cfg.CreateMap<Invite, InviteDto>();
                cfg.CreateMap<InviteDto, Invite>();
                cfg.CreateMap<EventMember, EventMemberDto>()
                    .ForMember(dest => dest.User, opt => opt.MapFrom(src => new UserDto { UserId = src.User.UserId }));
                cfg.CreateMap<EventMemberDto, EventMember>()
                    .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.User.UserId));
                cfg.CreateMap<ChatMessage, ChatMessageDto>();
                cfg.CreateMap<ChatMessageDto, ChatMessage>();
                cfg.CreateMap<Rating, RatingDto>();
                cfg.CreateMap<RatingDto, Rating>();
                cfg.CreateMap<Notification, NotificationDto>();
                cfg.CreateMap<NotificationDto, Notification>();
                cfg.CreateMap<Status, StatusDto>();
                cfg.CreateMap<StatusDto, Status>();
            });
            _mapper = config.CreateMapper();
        }

        /// <summary>
        /// Converts an <see cref="Address"/> entity to an <see cref="AddressDto"/>.
        /// </summary>
        /// <param name="address">The address entity to convert.</param>
        /// <returns>The converted address DTO.</returns>
        public AddressDto ConvertAddressToDto(Address address)
        {
            return _mapper.Map<AddressDto>(address);
        }

        /// <summary>
        /// Converts an <see cref="AddressDto"/> to an <see cref="Address"/> entity.
        /// </summary>
        /// <param name="addressDto">The address DTO to convert.</param>
        /// <returns>The converted address entity.</returns>
        public Address ConvertAddressDtoToEntity(AddressDto addressDto)
        {
            return _mapper.Map<Address>(addressDto);
        }

        /// <summary>
        /// Converts an <see cref="Event"/> entity to an <see cref="EventDto"/>.
        /// </summary>
        /// <param name="eventItem">The event entity to convert.</param>
        /// <returns>The converted event DTO.</returns>
        public EventDto ConvertEventToDto(Event eventItem)
        {
            return _mapper.Map<EventDto>(eventItem);
        }

        /// <summary>
        /// Converts a <see cref="VwPilotEvent"/> entity to an <see cref="EventDto"/>.
        /// </summary>
        /// <param name="vwPilot">The pilot event entity to convert.</param>
        /// <returns>The converted event DTO.</returns>
        public EventDto ConvertVwPilotEventToDto(VwPilotEvent vwPilot)
        {
            return _mapper.Map<EventDto>(vwPilot);
        }

        /// <summary>
        /// Converts a <see cref="VwPassengerEvent"/> entity to an <see cref="EventDto"/>.
        /// </summary>
        /// <param name="vwPassenger">The passenger event entity to convert.</param>
        /// <returns>The converted event DTO.</returns>
        public EventDto ConvertVwPassengerEventToDto(VwPassengerEvent vwPassenger)
        {
            return _mapper.Map<EventDto>(vwPassenger);
        }

        /// <summary>
        /// Converts a <see cref="VwPassengerEvent"/> entity to an <see cref="EventDto"/>.
        /// </summary>
        /// <param name="vwPassenger">The passenger event entity to convert.</param>
        /// <returns>The converted event DTO.</returns>
        public EventDto ConvertFNUserEventToDto(FN_GetEventsByMemberUserIdResult FNUserEvent)
        {
            return _mapper.Map<EventDto>(FNUserEvent);
        }

        /// <summary>
        /// Converts an <see cref="EventDto"/> to an <see cref="Event"/> entity.
        /// </summary>
        /// <param name="eventDto">The event DTO to convert.</param>
        /// <returns>The converted event entity.</returns>
        public Event ConvertEventDtoToEntity(EventDto eventDto)
        {
            return _mapper.Map<Event>(eventDto);
        }

        /// <summary>
        /// Converts a <see cref="Schedule"/> entity to a <see cref="ScheduleDto"/>.
        /// </summary>
        /// <param name="schedule">The schedule entity to convert.</param>
        /// <returns>The converted schedule DTO.</returns>
        public ScheduleDto ConvertScheduleToDto(Schedule schedule)
        {
            return _mapper.Map<ScheduleDto>(schedule);
        }

        /// <summary>
        /// Converts a <see cref="ScheduleDto"/> to a <see cref="Schedule"/> entity.
        /// </summary>
        /// <param name="scheduleDto">The schedule DTO to convert.</param>
        /// <returns>The converted schedule entity.</returns>
        public Schedule ConvertScheduleDtoToEntity(ScheduleDto scheduleDto)
        {
            return _mapper.Map<Schedule>(scheduleDto);
        }

        /// <summary>
        /// Converts a <see cref="User"/> entity to a <see cref="UserDto"/>.
        /// </summary>
        /// <param name="user">The user entity to convert.</param>
        /// <returns>The converted user DTO.</returns>
        public UserDto ConvertUserToDto(User user)
        {
            return _mapper.Map<UserDto>(user);
        }

        /// <summary>
        /// Converts a <see cref="UserDto"/> to a <see cref="User"/> entity.
        /// </summary>
        /// <param name="userDto">The user DTO to convert.</param>
        /// <returns>The converted user entity.</returns>
        public User ConvertUserDtoToEntity(UserDto userDto)
        {
            return _mapper.Map<User>(userDto);
        }

        /// <summary>
        /// Converts a <see cref="Vehicle"/> entity to a <see cref="VehicleDto"/>.
        /// </summary>
        /// <param name="vehicle">The vehicle entity to convert.</param>
        /// <returns>The converted vehicle DTO.</returns>
        public VehicleDto ConvertVehicleToDto(Vehicle vehicle)
        {
            return _mapper.Map<VehicleDto>(vehicle);
        }

        /// <summary>
        /// Converts a <see cref="VehicleDto"/> to a <see cref="Vehicle"/> entity.
        /// </summary>
        /// <param name="vehicleDto">The vehicle DTO to convert.</param>
        /// <returns>The converted vehicle entity.</returns>
        public Vehicle ConvertVehicleDtoToEntity(VehicleDto vehicleDto)
        {
            return _mapper.Map<Vehicle>(vehicleDto);
        }

        /// <summary>
        /// Converts a <see cref="VehicleMapping"/> entity to a <see cref="VehicleMappingDto"/>.
        /// </summary>
        /// <param name="vehicle">The vehicle mapping entity to convert.</param>
        /// <returns>The converted vehicle mapping DTO.</returns>
        public VehicleMappingDto ConvertVehicleMappingToDto(VehicleMapping vehicle)
        {
            return _mapper.Map<VehicleMappingDto>(vehicle);
        }

        /// <summary>
        /// Converts a <see cref="VehicleMappingDto"/> to a <see cref="VehicleMapping"/> entity.
        /// </summary>
        /// <param name="vehicleDto">The vehicle mapping DTO to convert.</param>
        /// <returns>The converted vehicle mapping entity.</returns>
        public VehicleMapping ConvertVehicleMappingDtoToEntity(VehicleMappingDto vehicleDto)
        {
            return _mapper.Map<VehicleMapping>(vehicleDto);
        }

        /// <summary>
        /// Converts a <see cref="Stop"/> entity to a <see cref="StopDto"/>.
        /// </summary>
        /// <param name="stop">The stop entity to convert.</param>
        /// <returns>The converted stop DTO.</returns>
        public StopDto ConvertStopToDto(Stop stop)
        {
            return _mapper.Map<StopDto>(stop);
        }

        /// <summary>
        /// Converts a <see cref="StopDto"/> to a <see cref="Stop"/> entity.
        /// </summary>
        /// <param name="stopDto">The stop DTO to convert.</param>
        /// <returns>The converted stop entity.</returns>
        public Stop ConvertStopDtoToEntity(StopDto stopDto)
        {
            return _mapper.Map<Stop>(stopDto);
        }

        /// <summary>
        /// Converts an <see cref="Invite"/> entity to an <see cref="InviteDto"/>.
        /// </summary>
        /// <param name="invite">The invite entity to convert.</param>
        /// <returns>The converted invite DTO.</returns>
        public InviteDto ConvertInviteToDto(Invite invite)
        {
            return _mapper.Map<InviteDto>(invite);
        }

        /// <summary>
        /// Converts an <see cref="InviteDto"/> to an <see cref="Invite"/> entity.
        /// </summary>
        /// <param name="inviteDto">The invite DTO to convert.</param>
        /// <returns>The converted invite entity.</returns>
        public Invite ConvertInviteDtoToEntity(InviteDto inviteDto)
        {
            return _mapper.Map<Invite>(inviteDto);
        }

        /// <summary>
        /// Converts an <see cref="EventMember"/> entity to an <see cref="EventMemberDto"/>.
        /// </summary>
        /// <param name="eventMember">The event member entity to convert.</param>
        /// <returns>The converted event member DTO.</returns>
        public EventMemberDto ConvertEventMemberToDto(EventMember eventMember)
        {
            return _mapper.Map<EventMemberDto>(eventMember);
        }

        /// <summary>
        /// Converts an <see cref="EventMemberDto"/> to an <see cref="EventMember"/> entity.
        /// </summary>
        /// <param name="eventMemberDto">The event member DTO to convert.</param>
        /// <returns>The converted event member entity.</returns>
        public EventMember ConvertEventMemberDtoToEntity(EventMemberDto eventMemberDto)
        {
            return _mapper.Map<EventMember>(eventMemberDto);
        }

        /// <summary>
        /// Converts a <see cref="ChatMessage"/> entity to a <see cref="ChatMessageDto"/>.
        /// </summary>
        /// <param name="chatMessage">The chat message entity to convert.</param>
        /// <returns>The converted chat message DTO.</returns>
        public ChatMessageDto ConvertChatMessageToDto(ChatMessage chatMessage)
        {
            return _mapper.Map<ChatMessageDto>(chatMessage);
        }

        /// <summary>
        /// Converts a <see cref="ChatMessageDto"/> to a <see cref="ChatMessage"/> entity.
        /// </summary>
        /// <param name="chatMessageDto">The chat message DTO to convert.</param>
        /// <returns>The converted chat message entity.</returns>
        public ChatMessage ConvertChatMessageDtoToEntity(ChatMessageDto chatMessageDto)
        {
            return _mapper.Map<ChatMessage>(chatMessageDto);
        }

        /// <summary>
        /// Converts a <see cref="Rating"/> entity to a <see cref="RatingDto"/>.
        /// </summary>
        /// <param name="rating">The rating entity to convert.</param>
        /// <returns>The converted rating DTO.</returns>
        public RatingDto ConvertRatingToDto(Rating rating)
        {
            return _mapper.Map<RatingDto>(rating);
        }

        /// <summary>
        /// Converts a <see cref="RatingDto"/> to a <see cref="Rating"/> entity.
        /// </summary>
        /// <param name="ratingDto">The rating DTO to convert.</param>
        /// <returns>The converted rating entity.</returns>
        public Rating ConvertRatingDtoToEntity(RatingDto ratingDto)
        {
            return _mapper.Map<Rating>(ratingDto);
        }

        /// <summary>
        /// Converts a <see cref="Notification"/> entity to a <see cref="NotificationDto"/>.
        /// </summary>
        /// <param name="notification">The notification entity to convert.</param>
        /// <returns>The converted notification DTO.</returns>
        public NotificationDto ConvertNotificationToDto(Notification notification)
        {
            return _mapper.Map<NotificationDto>(notification);
        }

        /// <summary>
        /// Converts a <see cref="NotificationDto"/> to a <see cref="Notification"/> entity.
        /// </summary>
        /// <param name="notificationDto">The notification DTO to convert.</param>
        /// <returns>The converted notification entity.</returns>
        public Notification ConvertNotificationDtoToEntity(NotificationDto notificationDto)
        {
            return _mapper.Map<Notification>(notificationDto);
        }

        /// <summary>
        /// Converts a <see cref="Status"/> entity to a <see cref="StatusDto"/>.
        /// </summary>
        /// <param name="status">The status entity to convert.</param>
        /// <returns>The converted status DTO.</returns>
        public StatusDto ConvertStatusToDto(Status status)
        {
            return _mapper.Map<StatusDto>(status);
        }

        /// <summary>
        /// Converts a <see cref="StatusDto"/> to a <see cref="Status"/> entity.
        /// </summary>
        /// <param name="statusDto">The status DTO to convert.</param>
        /// <returns>The converted status entity.</returns>
        public Status ConvertStatusDtoToEntity(StatusDto statusDto)
        {
            return _mapper.Map<Status>(statusDto);
        }
    }
}
