using WayMatcherBL.DtoModels;
using WayMatcherBL.Enums;
using WayMatcherBL.Interfaces;
using WayMatcherBL.LogicModels;
using WayMatcherBL.Mapper;
using WayMatcherBL.Models;

namespace WayMatcherBL.Services
{
    /// <summary>
    /// Provides database operations for various entities.
    /// </summary>
    public class DatabaseService : IDatabaseService
    {
        private readonly WayMatcherContext _dbContext;
        private ModelMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="DatabaseService"/> class.
        /// </summary>
        /// <param name="dbContext">The database context.</param>
        /// <param name="mapper">The model mapper.</param>
        public DatabaseService(WayMatcherContext dbContext, ModelMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        /// <summary>
        /// Gets the list of schedules.
        /// </summary>
        /// <returns>A list of <see cref="ScheduleDto"/>.</returns>
        private List<ScheduleDto> GetSchedules()
        {
            var scheduleList = new List<ScheduleDto>();
            foreach (var schedule in _dbContext.Schedules.Where(s => s.UserId.Equals(-1)).ToList())
            {
                scheduleList.Add(_mapper.ConvertScheduleToDto(schedule));
            }
            return scheduleList;
        }

        /// <summary>
        /// Gets the status by ID.
        /// </summary>
        /// <param name="id">The status ID.</param>
        /// <returns>A <see cref="StatusDto"/>.</returns>
        private StatusDto GetStatus(int id)
        {
            var status = _dbContext.Statuses.Where(s => s.StatusId.Equals(id)).FirstOrDefault();

            return _mapper.ConvertStatusToDto(status);
        }

        /// <summary>
        /// Gets the list of active addresses.
        /// </summary>
        /// <returns>A list of <see cref="AddressDto"/>.</returns>
        public List<AddressDto> GetActiveAddresses()
        {
            var addressList = new List<AddressDto>();

            foreach (var address in _dbContext.Addresses.Where(a => a.Status.StatusDescription.Equals(State.Active.GetDescription())).ToList())
            {
                addressList.Add(_mapper.ConvertAddressToDto(address));
            }

            return addressList;
        }

        /// <summary>
        /// Gets the list of schedules for a user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>A list of <see cref="ScheduleDto"/>.</returns>
        public List<ScheduleDto> GetUserSchedules(UserDto user)
        {
            var scheduleList = GetSchedules();

            foreach (var schedule in _dbContext.Schedules.Where(s => s.UserId.Equals(user.UserId)).ToList())
            {
                scheduleList.Add(_mapper.ConvertScheduleToDto(schedule));
            }

            return scheduleList;
        }

        /// <summary>
        /// Gets the list of active users.
        /// </summary>
        /// <returns>A list of <see cref="UserDto"/>.</returns>
        public List<UserDto> GetActiveUsers()
        {
            var userList = new List<UserDto>();

            foreach (var user in _dbContext.Users.Where(u => u.Status.StatusDescription.Equals(State.Active.GetDescription())).ToList())
            {
                var userDto = _mapper.ConvertUserToDto(user);
                if (userDto.Address == null)
                    userDto.Address = new AddressDto();
                userDto.Address.AddressId = user.AddressId ?? -1;
                userDto.Address = GetAddress(userDto.Address);
                userList.Add(userDto);
            }

            return userList;
        }

        /// <summary>
        /// Gets the list of active vehicles.
        /// </summary>
        /// <returns>A list of <see cref="VehicleDto"/>.</returns>
        public List<VehicleDto> GetActiveVehicles()
        {
            var vehicleList = new List<VehicleDto>();

            foreach (var vehicle in _dbContext.Vehicles.Where(v => v.Status.StatusDescription.Equals(State.Active.GetDescription())).ToList())
            {
                vehicleList.Add(_mapper.ConvertVehicleToDto(vehicle));
            }

            return vehicleList;
        }

        /// <summary>
        /// Gets the list of events.
        /// </summary>
        /// <param name="isPilot">Indicates if the events are for pilots.</param>
        /// <returns>A list of <see cref="EventDto"/>.</returns>
        public List<EventDto> GetEventList(bool? isPilot)
        {
            var eventList = new List<EventDto>();
            if (isPilot.HasValue)
            {
                if (isPilot == true)
                {
                    foreach (var eventItem in _dbContext.VwPilotEvents.ToList().Where(e => e.StatusId == (int)Enums.State.Active))
                    {
                        eventList.Add(_mapper.ConvertVwPilotEventToDto(eventItem));
                    }
                }
                else
                {
                    foreach (var eventItem in _dbContext.VwPassengerEvents.ToList().Where(e => e.StatusId == (int)Enums.State.Active))
                    {
                        eventList.Add(_mapper.ConvertVwPassengerEventToDto(eventItem));
                    }
                }
            }
            else
            {
                foreach (var eventItem in _dbContext.Events.ToList().Where(e => e.StatusId == (int)Enums.State.Active))
                {
                    eventList.Add(_mapper.ConvertEventToDto(eventItem));
                }
            }

            return eventList;
        }


        /// <summary>
        /// Gets the list of event members.
        /// </summary>
        /// <param name="eventDto">The event.</param>
        /// <returns>A list of <see cref="EventMemberDto"/>.</returns>
        public List<EventMemberDto> GetEventMemberList(EventDto eventDto)
        {
            var eventMemberList = new List<EventMemberDto>();
            var eventMembers = _dbContext.EventMembers.Where(em => em.EventId == eventDto.EventId).ToList();

            foreach (var eventMember in eventMembers)
            {
                var eventMemberDto = _mapper.ConvertEventMemberToDto(eventMember);

                var user = new UserDto()
                {
                    UserId = eventMember.UserId ?? -1
                };

                eventMemberDto.User = GetUser(user);
                eventMemberDto.EventRole = (EventRole)eventMember.EventMemberTypeId;
                eventMemberDto.Status = GetStatus(eventMember.StatusId ?? -1);
                eventMemberList.Add(eventMemberDto);
            }

            return eventMemberList;
        }

        /// <summary>
        /// Gets the list of ratings for a user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>A list of <see cref="RatingDto"/>.</returns>
        public List<RatingDto> GetRatingList(UserDto user)
        {
            var ratingList = new List<RatingDto>();
            var ratings = _dbContext.Ratings.Where(r => r.RatedUserId == user.UserId).ToList();

            foreach (var rating in ratings)
            {
                ratingList.Add(_mapper.ConvertRatingToDto(rating));
            }

            return ratingList;
        }

        /// <summary>
        /// Gets the list of vehicles for a user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>A list of <see cref="VehicleDto"/>.</returns>
        public List<VehicleDto> GetUserVehicles(UserDto user)
        {
            var vehicleList = new List<VehicleDto>();
            var vehicles = _dbContext.VehicleMappings.Where(vm => vm.UserId == user.UserId).Select(vm => vm.Vehicle).ToList();

            foreach (var vehicle in vehicles)
            {
                vehicleList.Add(_mapper.ConvertVehicleToDto(vehicle));
            }

            return vehicleList;
        }

        /// <summary>
        /// Gets the list of notifications for a user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>A list of <see cref="NotificationDto"/>.</returns>
        public List<NotificationDto> GetNotificationList(UserDto user)
        {
            var notificationList = new List<NotificationDto>();
            var notifications = _dbContext.Notifications.Where(n => n.UserId == user.UserId).ToList();

            foreach (var notification in notifications)
            {
                notificationList.Add(_mapper.ConvertNotificationToDto(notification));
            }

            return notificationList;
        }

        /// <summary>
        /// Gets the list of invites for an event.
        /// </summary>
        /// <param name="eventDto">The event.</param>
        /// <returns>A list of <see cref="InviteDto"/>.</returns>
        public List<InviteDto> GetInviteList(EventDto eventDto)
        {
            var inviteList = new List<InviteDto>();
            var invites = _dbContext.Invites.Where(i => i.EventId == eventDto.EventId && i.StatusId == (int)State.Pending).ToList();

            foreach (var invite in invites)
            {
                var inviteDto = _mapper.ConvertInviteToDto(invite);
                if (inviteDto.User == null)
                    inviteDto.User = new UserDto();

                inviteDto.User = GetUser(new UserDto() { UserId = invite.UserId ?? -1 });

                inviteList.Add(inviteDto);
            }

            return inviteList;
        }

        /// <summary>
        /// Gets the address by address details.
        /// </summary>
        /// <param name="address">The address details.</param>
        /// <returns>A <see cref="AddressDto"/>.</returns>
        public AddressDto GetAddress(AddressDto address)
        {
            var dbAddress = _dbContext.Addresses.FirstOrDefault(a => a.AddressId == address.AddressId || a.Longitude == address.Longitude && a.Latitude == address.Latitude && a.City == address.City && a.PostalCode == address.PostalCode && a.Country == address.Country);

            if (dbAddress == null)
            {
                dbAddress = new Address()
                {
                    AddressId = -1
                };
            }

            return _mapper.ConvertAddressToDto(dbAddress);
        }

        /// <summary>
        /// Gets the address by user details.
        /// </summary>
        /// <param name="user">The user details.</param>
        /// <returns>A <see cref="AddressDto"/>.</returns>
        public AddressDto GetAddress(UserDto user)
        {
            var dbUser = _dbContext.Users.FirstOrDefault(u => u.EMail == user.Email || u.Username == user.Username || u.UserId == user.UserId);

            if (dbUser == null)
                return null;

            var dbAddress = _dbContext.Addresses.FirstOrDefault(a => a.AddressId == dbUser.AddressId);

            if (dbAddress == null)
                return null;

            return _mapper.ConvertAddressToDto(dbAddress);
        }

        /// <summary>
        /// Gets the event by event details.
        /// </summary>
        /// <param name="eventDto">The event details.</param>
        /// <returns>A <see cref="EventDto"/>.</returns>
        public EventDto GetEvent(EventDto eventDto)
        {
            var eventItem = _dbContext.Events.FirstOrDefault(e => e.EventId == eventDto.EventId);

            if (eventItem == null)
            {
                return null;
            }
            eventDto = _mapper.ConvertEventToDto(eventItem);
            eventDto.Status = GetStatus(eventItem.StatusId ?? -1);
            eventDto.Owner = GetEventOwner(eventDto);
            eventDto.StopList = GetStopList(eventDto);

            return eventDto;
        }

        /// <summary>
        /// Gets the schedule by ID.
        /// </summary>
        /// <param name="id">The schedule ID.</param>
        /// <returns>A <see cref="ScheduleDto"/>.</returns>
        public ScheduleDto GetScheduleById(int id)
        {
            var schedule = _dbContext.Schedules.FirstOrDefault(s => s.ScheduleId == id);

            if (schedule == null)
            {
                return null;
            }

            return _mapper.ConvertScheduleToDto(schedule);
        }

        /// <summary>
        /// Gets the user by user details.
        /// </summary>
        /// <param name="userDto">The user details.</param>
        /// <returns>A <see cref="UserDto"/>.</returns>
        public UserDto GetUser(UserDto userDto)
        {
            var user = _dbContext.Users.FirstOrDefault(u => u.EMail == userDto.Email || u.Username == userDto.Username || u.UserId == userDto.UserId);

            if (user == null)
                return null;

            userDto = _mapper.ConvertUserToDto(user);
            userDto.Address = GetAddress(userDto);

            return userDto;
        }

        /// <summary>
        /// Gets the vehicle by ID.
        /// </summary>
        /// <param name="id">The vehicle ID.</param>
        /// <returns>A <see cref="VehicleDto"/>.</returns>
        public VehicleDto GetVehicleById(int id)
        {
            var vehicle = _dbContext.Vehicles.FirstOrDefault(v => v.VehicleId == id);

            if (vehicle == null)
            {
                return null;
            }

            return _mapper.ConvertVehicleToDto(vehicle);
        }

        /// <summary>
        /// Gets the rating by rating details.
        /// </summary>
        /// <param name="rating">The rating details.</param>
        /// <returns>A <see cref="RatingDto"/>.</returns>
        public RatingDto GetRating(RatingDto rating)
        {
            var ratingItem = _dbContext.Ratings.FirstOrDefault(r => r.RatingId == rating.RatingId || r.RatedUserId == rating.RatedUserId && r.UserWhoRatedId == rating.UserWhoRatedId);

            if (ratingItem == null)
                return null;

            return _mapper.ConvertRatingToDto(ratingItem);
        }

        /// <summary>
        /// Gets the event ID by event details.
        /// </summary>
        /// <param name="eventModel">The event details.</param>
        /// <returns>The event ID.</returns>
        public int GetEventId(EventDto eventModel)
        {
            var eventItem = _dbContext.Events.FirstOrDefault(e => e.Description == eventModel.Description && e.EventTypeId == eventModel.EventTypeId && e.FreeSeats == eventModel.FreeSeats && e.StartTimestamp == eventModel.StartTimestamp);

            if (eventItem == null)
            {
                return -1;
            }

            return eventItem.EventId;
        }

        /// <summary>
        /// Gets the schedule ID by schedule details.
        /// </summary>
        /// <param name="scheduleModel">The schedule details.</param>
        /// <returns>The schedule ID.</returns>
        public int GetScheduleId(ScheduleDto scheduleModel)
        {
            var schedule = _dbContext.Schedules.FirstOrDefault(s => s.CronSchedule == scheduleModel.CronSchedule);

            if (schedule == null)
            {
                return -1;
            }

            return schedule.ScheduleId;
        }

        /// <summary>
        /// Gets the vehicle ID by vehicle details.
        /// </summary>
        /// <param name="vehicleModel">The vehicle details.</param>
        /// <returns>The vehicle ID.</returns>
        public int GetVehicleId(VehicleDto vehicleModel)
        {
            var vehicle = _dbContext.Vehicles.FirstOrDefault(v => v.Model == vehicleModel.Model && v.Seats == vehicleModel.Seats);

            if (vehicle == null)
            {
                return -1;
            }

            return vehicle.VehicleId;
        }

        /// <summary>
        /// Gets the list of stops for an event.
        /// </summary>
        /// <param name="eventDto">The event.</param>
        /// <returns>A list of <see cref="StopDto"/>.</returns>
        public List<StopDto> GetStopList(EventDto eventDto)
        {
            var stopList = new List<StopDto>();
            var stops = _dbContext.Stops.Where(s => s.EventId == eventDto.EventId).ToList();

            foreach (var stop in stops)
            {
                var stopDto = _mapper.ConvertStopToDto(stop);
                if (stopDto.Address == null)
                    stopDto.Address = new AddressDto();
                stopDto.Address.AddressId = stop.AddressId ?? -1;
                stopDto.Address = GetAddress(stopDto.Address);
                stopList.Add(stopDto);
            }

            return stopList;
        }

        /// <summary>
        /// Gets the list of chat messages for an event member.
        /// </summary>
        /// <param name="eventMember">The event member.</param>
        /// <returns>A list of <see cref="ChatMessageDto"/>.</returns>
        public List<ChatMessageDto> GetChatMessageList(EventMemberDto eventMember)
        {
            var chatMessageList = new List<ChatMessageDto>();
            var chatMessages = _dbContext.ChatMessages.Where(tm => tm.EventId == eventMember.EventId).ToList();

            foreach (var chatMessage in chatMessages)
            {
                chatMessageList.Add(_mapper.ConvertChatMessageToDto(chatMessage));
            }

            return chatMessageList;
        }

        /// <summary>
        /// Gets the event owner by event details.
        /// </summary>
        /// <param name="eventDto">The event details.</param>
        /// <returns>A <see cref="UserDto"/> representing the event owner.</returns>
        public UserDto GetEventOwner(EventDto eventDto)
        {
            var eventOwnerId = _dbContext.Events.Where(e => e.EventId.Equals(eventDto.EventId)).FirstOrDefault().EventOwnerId;
            var eventOwner = GetUser(new UserDto() { UserId = eventOwnerId ?? -1 });

            if (eventOwner == null)
                return null;

            return eventOwner;
        }

        /// <summary>
        /// Inserts a new address.
        /// </summary>
        /// <param name="addressModel">The address details.</param>
        /// <returns><c>true</c> if the address was inserted successfully; otherwise, <c>false</c>.</returns>
        public bool InsertAddress(AddressDto addressModel)
        {
            var existingAddress = _dbContext.Addresses.FirstOrDefault(a => a.AddressId == addressModel.AddressId);
            if (existingAddress != null)
                throw new ArgumentException("An address with the same ID already exists.");

            var addressEntity = _mapper.ConvertAddressDtoToEntity(addressModel);

            addressEntity.StatusId = (int)Enums.State.Active;
            addressEntity.AddressId = 0;

            _dbContext.Addresses.Add(addressEntity);
            return _dbContext.SaveChanges() > 0;
        }

        /// <summary>
        /// Inserts a new event.
        /// </summary>
        /// <param name="eventModel">The event details.</param>
        /// <returns>The created <see cref="EventDto"/>.</returns>
        public EventDto InsertEvent(EventDto eventModel)
        {
            var existingEvent = _dbContext.Events.FirstOrDefault(e => e.EventId == eventModel.EventId);
            if (existingEvent != null)
                throw new ArgumentException("An event with the same ID already exists.");

            var eventEntity = _mapper.ConvertEventDtoToEntity(eventModel);

            eventEntity.Status = null;
            eventEntity.Schedule = null;
            eventEntity.StatusId = (int)State.Active;
            eventEntity.EventOwnerId = eventModel.Owner.UserId;
            eventEntity.ScheduleId = eventModel.Schedule.ScheduleId;

            _dbContext.Events.Add(eventEntity);
            _dbContext.SaveChanges();

            var savedEvent = _dbContext.Events.FirstOrDefault(e => e.EventId == eventEntity.EventId);
            return _mapper.ConvertEventToDto(savedEvent);
        }

        /// <summary>
        /// Inserts a new schedule.
        /// </summary>
        /// <param name="scheduleModel">The schedule details.</param>
        /// <returns><c>true</c> if the schedule was inserted successfully; otherwise, <c>false</c>.</returns>
        public ScheduleDto InsertSchedule(ScheduleDto scheduleModel)
        {
            var existingSchedule = _dbContext.Schedules.FirstOrDefault(s => s.ScheduleId == scheduleModel.ScheduleId);
            if (existingSchedule != null)
                throw new ArgumentException("A schedule with the same ID already exists.");

            var scheduleEntity = _mapper.ConvertScheduleDtoToEntity(scheduleModel);

            _dbContext.Schedules.Add(scheduleEntity);
            _dbContext.SaveChanges();

            var savedSchedule = _dbContext.Schedules.FirstOrDefault(s => s.CronSchedule == scheduleEntity.CronSchedule);
            return _mapper.ConvertScheduleToDto(savedSchedule);
        }

        /// <summary>
        /// Inserts a new user.
        /// </summary>
        /// <param name="userModel">The user details.</param>
        /// <returns><c>true</c> if the user was inserted successfully; otherwise, <c>false</c>.</returns>
        public bool InsertUser(UserDto userModel)
        {
            var existingUser = _dbContext.Users.FirstOrDefault(u => u.EMail == userModel.Email || u.Username == userModel.Username);
            if (existingUser != null)
                throw new ArgumentException("A user with the same email or username already exists.");

            var userEntity = _mapper.ConvertUserDtoToEntity(userModel);

            userEntity.StatusId = (int)Enums.State.Active;
            userEntity.AddressId = userModel.Address.AddressId;
            userEntity.Address = null;

            _dbContext.Users.Add(userEntity);
            return _dbContext.SaveChanges() > 0;
        }

        /// <summary>
        /// Inserts a new vehicle.
        /// </summary>
        /// <param name="vehicleModel">The vehicle details.</param>
        /// <returns><c>true</c> if the vehicle was inserted successfully; otherwise, <c>false</c>.</returns>
        public bool InsertVehicle(VehicleDto vehicleModel)
        {
            var existingVehicle = _dbContext.Vehicles.FirstOrDefault(v => v.VehicleId == vehicleModel.VehicleId);
            if (existingVehicle != null)
                throw new ArgumentException("A vehicle with the same ID already exists.");

            var vehicleEntity = _mapper.ConvertVehicleDtoToEntity(vehicleModel);

            vehicleEntity.VehicleId = 0;
            vehicleEntity.StatusId = (int)Enums.State.Active;

            _dbContext.Vehicles.Add(vehicleEntity);
            return _dbContext.SaveChanges() > 0;
        }

        /// <summary>
        /// Inserts a new vehicle mapping.
        /// </summary>
        /// <param name="vehicleMapping">The vehicle mapping details.</param>
        /// <returns><c>true</c> if the vehicle mapping was inserted successfully; otherwise, <c>false</c>.</returns>
        public bool InsertVehicleMapping(VehicleMappingDto vehicleMapping)
        {
            var existingVehicleMapping = _dbContext.VehicleMappings.FirstOrDefault(vm => vm.VehicleMappingId == vehicleMapping.VehicleMappingId);
            if (existingVehicleMapping != null)
                throw new ArgumentException("A vehicle mapping with the same ID already exists.");

            var vehicleMappingEntity = _mapper.ConvertVehicleMappingDtoToEntity(vehicleMapping);

            vehicleMappingEntity.StatusId = (int)Enums.State.Active;

            _dbContext.VehicleMappings.Add(vehicleMappingEntity);
            return _dbContext.SaveChanges() > 0;
        }

        /// <summary>
        /// Inserts a new stop.
        /// </summary>
        /// <param name="stop">The stop details.</param>
        /// <returns><c>true</c> if the stop was inserted successfully; otherwise, <c>false</c>.</returns>
        public bool InsertStop(StopDto stop)
        {
            var existingStop = _dbContext.Stops.FirstOrDefault(s => s.StopId == stop.StopId);
            if (existingStop != null)
                throw new ArgumentException("A stop with the same ID already exists.");

            var stopEntity = _mapper.ConvertStopDtoToEntity(stop);

            stopEntity.Address = null;
            stopEntity.Event = null;
            stopEntity.AddressId = stop.Address.AddressId;
            stopEntity.EventId = stop.EventId;

            _dbContext.Stops.Add(stopEntity);

            return _dbContext.SaveChanges() > 0;
        }

        /// <summary>
        /// Inserts a new invite.
        /// </summary>
        /// <param name="invite">The invite details.</param>
        /// <returns><c>true</c> if the invite was inserted successfully; otherwise, <c>false</c>.</returns>
        public bool InsertToInvite(InviteDto invite)
        {
            var existingInvite = _dbContext.Invites.FirstOrDefault(i => i.InviteId == invite.InviteId);
            if (existingInvite != null)
                throw new ArgumentException("An invite with the same ID already exists.");

            var inviteEntity = _mapper.ConvertInviteDtoToEntity(invite);

            inviteEntity.UserId = invite.User.UserId;
            inviteEntity.User = null;
            inviteEntity.Status = null;

            _dbContext.Invites.Add(inviteEntity);
            return _dbContext.SaveChanges() > 0;
        }

        /// <summary>
        /// Inserts a new event member.
        /// </summary>
        /// <param name="eventMember">The event member details.</param>
        /// <returns><c>true</c> if the event member was inserted successfully; otherwise, <c>false</c>.</returns>
        public bool InsertToEventMember(EventMemberDto eventMember)
        {
            var existingEventMember = _dbContext.EventMembers.FirstOrDefault(em => em.MemberId == eventMember.MemberId);
            if (existingEventMember != null)
                throw new ArgumentException("An event member with the same ID already exists.");

            var eventMemberEntity = _mapper.ConvertEventMemberDtoToEntity(eventMember);

            var trackedUser = _dbContext.Users.Local.FirstOrDefault(u => u.UserId == eventMemberEntity.User.UserId);
            if (trackedUser != null)
                eventMemberEntity.User = trackedUser;

            else
                _dbContext.Users.Attach(eventMemberEntity.User);

            eventMemberEntity.Status = null;

            _dbContext.EventMembers.Add(eventMemberEntity);
            return _dbContext.SaveChanges() > 0;
        }

        /// <summary>
        /// Inserts a new chat message.
        /// </summary>
        /// <param name="chatMessage">The chat message details.</param>
        /// <returns><c>true</c> if the chat message was inserted successfully; otherwise, <c>false</c>.</returns>
        public bool InsertChatMessage(ChatMessageDto chatMessage)
        {
            var existingChatMessage = _dbContext.ChatMessages.FirstOrDefault(cm => cm.ChatMessageId == chatMessage.ChatMessageId);
            if (existingChatMessage != null)
                throw new ArgumentException("A chat message with the same ID already exists.");

            var chatMessageEntity = _mapper.ConvertChatMessageDtoToEntity(chatMessage);

            _dbContext.ChatMessages.Add(chatMessageEntity);
            return _dbContext.SaveChanges() > 0;
        }

        /// <summary>
        /// Inserts a new rating.
        /// </summary>
        /// <param name="rating">The rating details.</param>
        /// <returns><c>true</c> if the rating was inserted successfully; otherwise, <c>false</c>.</returns>
        public bool InsertRating(RatingDto rating)
        {
            var existingRating = _dbContext.Ratings.FirstOrDefault(r => r.RatingId == rating.RatingId || r.RatedUserId == rating.RatedUserId && r.UserWhoRatedId == rating.UserWhoRatedId);
            if (existingRating != null)
                throw new ArgumentException("A rating with the same ID already exists.");

            var ratingEntity = _mapper.ConvertRatingDtoToEntity(rating);

            _dbContext.Ratings.Add(ratingEntity);
            return _dbContext.SaveChanges() > 0;
        }

        /// <summary>
        /// Inserts a new notification.
        /// </summary>
        /// <param name="notification">The notification details.</param>
        /// <returns><c>true</c> if the notification was inserted successfully; otherwise, <c>false</c>.</returns>
        public bool InsertNotification(NotificationDto notification)
        {
            var existingNotification = _dbContext.Notifications.FirstOrDefault(n => n.NotificationId == notification.NotificationId);
            if (existingNotification != null)
                throw new ArgumentException("A notification with the same ID already exists.");

            var notificationEntity = _mapper.ConvertNotificationDtoToEntity(notification);

            _dbContext.Notifications.Add(notificationEntity);
            return _dbContext.SaveChanges() > 0;
        }

        /// <summary>
        /// Updates an existing address.
        /// </summary>
        /// <param name="addressModel">The address details.</param>
        /// <returns><c>true</c> if the address was updated successfully; otherwise, <c>false</c>.</returns>
        public bool UpdateAddress(AddressDto addressModel)
        {
            var addressEntity = _dbContext.Addresses.FirstOrDefault(a => a.AddressId == addressModel.AddressId);
            if (addressEntity == null)
                return false;

            if (!string.IsNullOrEmpty(addressModel.City))
                addressEntity.City = addressModel.City;

            if (!string.IsNullOrEmpty(addressModel.PostalCode))
                addressEntity.PostalCode = addressModel.PostalCode;

            if (!string.IsNullOrEmpty(addressModel.Street))
                addressEntity.Street = addressModel.Street;

            if (!string.IsNullOrEmpty(addressModel.Country))
                addressEntity.Country = addressModel.Country;

            if (!string.IsNullOrEmpty(addressModel.CountryCode))
                addressEntity.CountryCode = addressModel.CountryCode;

            if (!string.IsNullOrEmpty(addressModel.Region))
                addressEntity.Region = addressModel.Region;

            if (!string.IsNullOrEmpty(addressModel.State))
                addressEntity.State = addressModel.State;

            if (addressModel.Longitude.HasValue)
                addressEntity.Longitude = addressModel.Longitude;

            if (addressModel.Latitude.HasValue)
                addressEntity.Latitude = addressModel.Latitude;

            if (!string.IsNullOrEmpty(addressModel.AddressLine1))
                addressEntity.AddressLine1 = addressModel.AddressLine1;

            if (!string.IsNullOrEmpty(addressModel.AddressLine2))
                addressEntity.AddressLine2 = addressModel.AddressLine2;

            if (addressModel.StatusId.HasValue)
                addressEntity.StatusId = addressModel.StatusId;

            return _dbContext.SaveChanges() > 0;
        }

        /// <summary>
        /// Updates an existing event.
        /// </summary>
        /// <param name="eventModel">The event details.</param>
        /// <returns><c>true</c> if the event was updated successfully; otherwise, <c>false</c>.</returns>
        public bool UpdateEvent(EventDto eventModel)
        {
            var eventEntity = _dbContext.Events.FirstOrDefault(e => e.EventId == eventModel.EventId);
            if (eventEntity == null)
                return false;

            if (eventModel.EventTypeId.HasValue)
                eventEntity.EventTypeId = eventModel.EventTypeId;

            if (eventModel.FreeSeats.HasValue)
                eventEntity.FreeSeats = eventModel.FreeSeats;

            if (!string.IsNullOrEmpty(eventModel.Description))
                eventEntity.Description = eventModel.Description;

            if (eventModel.StartTimestamp.HasValue)
                eventEntity.StartTimestamp = eventModel.StartTimestamp;

            return _dbContext.SaveChanges() > 0;
        }

        /// <summary>
        /// Updates an existing schedule.
        /// </summary>
        /// <param name="scheduleModel">The schedule details.</param>
        /// <returns><c>true</c> if the schedule was updated successfully; otherwise, <c>false</c>.</returns>
        public bool UpdateSchedule(ScheduleDto scheduleModel)
        {
            var scheduleEntity = _dbContext.Schedules.FirstOrDefault(s => s.ScheduleId == scheduleModel.ScheduleId);
            if (scheduleEntity == null)
                return false;

            if (!string.IsNullOrEmpty(scheduleModel.CronSchedule))
                scheduleEntity.CronSchedule = scheduleModel.CronSchedule;

            return _dbContext.SaveChanges() > 0;
        }

        /// <summary>
        /// Updates an existing user.
        /// </summary>
        /// <param name="updateUserDto">The user details.</param>
        /// <returns><c>true</c> if the user was updated successfully; otherwise, <c>false</c>.</returns>
        public bool UpdateUser(UserDto updateUserDto)
        {
            var userEntity = _dbContext.Users.FirstOrDefault(u => u.UserId == GetUser(updateUserDto).UserId);
            if (userEntity == null)
                return false;

            if (!string.IsNullOrEmpty(updateUserDto.Firstname))
                userEntity.Firstname = updateUserDto.Firstname;

            if (!string.IsNullOrEmpty(updateUserDto.Name))
                userEntity.Name = updateUserDto.Name;

            if (!string.IsNullOrEmpty(updateUserDto.Username))
                userEntity.Username = updateUserDto.Username;

            if (!string.IsNullOrEmpty(updateUserDto.Password))
                userEntity.Password = updateUserDto.Password;

            if (!string.IsNullOrEmpty(updateUserDto.Email))
                userEntity.EMail = updateUserDto.Email;

            if (!string.IsNullOrEmpty(updateUserDto.Telephone))
                userEntity.Telephone = updateUserDto.Telephone;

            if (!string.IsNullOrEmpty(updateUserDto.AdditionalDescription))
                userEntity.AdditionalDescription = updateUserDto.AdditionalDescription;

            if (updateUserDto.LicenseVerified.HasValue)
                userEntity.LicenseVerified = updateUserDto.LicenseVerified;

            if (updateUserDto.ProfilePicture != null && updateUserDto.ProfilePicture.Length > 0)
                userEntity.ProfilePicture = updateUserDto.ProfilePicture;

            if (updateUserDto.Address != null && updateUserDto.Address.AddressId != -1)
                userEntity.AddressId = updateUserDto.Address.AddressId;

            if (!string.IsNullOrEmpty(updateUserDto.MfAtoken))
                userEntity.MfAtoken = updateUserDto.MfAtoken;

            if (updateUserDto.StatusId.HasValue)
                userEntity.StatusId = updateUserDto.StatusId;

            return _dbContext.SaveChanges() > 0;
        }

        /// <summary>
        /// Updates an existing vehicle.
        /// </summary>
        /// <param name="vehicleModel">The vehicle details.</param>
        /// <returns><c>true</c> if the vehicle was updated successfully; otherwise, <c>false</c>.</returns>
        public bool UpdateVehicle(VehicleDto vehicleModel)
        {
            var vehicleEntity = _dbContext.Vehicles.FirstOrDefault(v => v.VehicleId == vehicleModel.VehicleId);
            if (vehicleEntity == null)
                return false;

            if (!string.IsNullOrEmpty(vehicleModel.Model))
                vehicleEntity.Model = vehicleModel.Model;

            if (vehicleModel.Seats.HasValue)
                vehicleEntity.Seats = vehicleModel.Seats;

            if (vehicleModel.YearOfManufacture.HasValue)
                vehicleEntity.YearOfManufacture = vehicleModel.YearOfManufacture;

            if (!string.IsNullOrEmpty(vehicleModel.ManufacturerName))
                vehicleEntity.ManufacturerName = vehicleModel.ManufacturerName;

            return _dbContext.SaveChanges() > 0;
        }

        /// <summary>
        /// Updates an existing event member.
        /// </summary>
        /// <param name="eventMember">The event member details.</param>
        /// <returns><c>true</c> if the event member was updated successfully; otherwise, <c>false</c>.</returns>
        public bool UpdateEventMember(EventMemberDto eventMember)
        {
            var eventMemberEntity = _dbContext.EventMembers.FirstOrDefault(em => em.MemberId == eventMember.MemberId);
            if (eventMemberEntity == null)
                return false;

            if ((int)eventMember.EventRole != -1)
                eventMemberEntity.EventMemberTypeId = (int)eventMember.EventRole;

            if (eventMember.EventId != -1)
                eventMemberEntity.EventId = eventMember.EventId;

            if (eventMember.Status.StatusId != -1)
                eventMemberEntity.Status.StatusId = eventMember.Status.StatusId;

            return _dbContext.SaveChanges() > 0;
        }

        /// <summary>
        /// Updates an existing rating.
        /// </summary>
        /// <param name="rating">The rating details.</param>
        /// <returns><c>true</c> if the rating was updated successfully; otherwise, <c>false</c>.</returns>
        public bool UpdateRating(RatingDto rating)
        {
            var ratingEntity = _dbContext.Ratings.FirstOrDefault(r => r.RatingId == rating.RatingId || r.RatedUserId == rating.RatedUserId && r.UserWhoRatedId == rating.UserWhoRatedId);

            if (ratingEntity == null && rating.UserWhoRatedId != ratingEntity.UserWhoRatedId)
                return false;

            if (rating.RatingValue != -1)
                ratingEntity.RatingValue = rating.RatingValue;

            if (rating.RatingText != null)
                ratingEntity.RatingText = rating.RatingText;

            if (rating.StatusId != -1)
                ratingEntity.StatusId = rating.StatusId;

            return _dbContext.SaveChanges() > 0;
        }

        /// <summary>
        /// Updates an existing invite.
        /// </summary>
        /// <param name="invite">The invite details.</param>
        /// <returns><c>true</c> if the invite was updated successfully; otherwise, <c>false</c>.</returns>
        public bool UpdateInvite(InviteDto invite)
        {
            var inviteEntity = _dbContext.Invites.FirstOrDefault(i => i.InviteId == invite.InviteId);
            if (inviteEntity == null)
                return false;

            if (invite.StatusId != -1)
                inviteEntity.StatusId = invite.StatusId ?? -1;

            return _dbContext.SaveChanges() > 0;
        }

        /// <summary>
        /// Updates the read status of an existing notification.
        /// </summary>
        /// <param name="notification">The notification DTO containing the updated read status.</param>
        /// <returns><c>true</c> if the notification was successfully updated; otherwise, <c>false</c>.</returns>
        public bool UpdateNotification(NotificationDto notification)
        {
            var notificationEntity = _dbContext.Notifications.FirstOrDefault(n => n.NotificationId == notification.NotificationId);
            if (notificationEntity == null)
                return false;

            if (notification.Read != null)
                notificationEntity.Read = notification.Read;

            return _dbContext.SaveChanges() > 0;
        }

        /// <summary>
        /// Deletes an existing stop.
        /// </summary>
        /// <param name="stop">The stop details.</param>
        /// <returns><c>true</c> if the stop was deleted successfully; otherwise, <c>false</c>.</returns>
        public bool DeleteStop(StopDto stop)
        {
            var stopEntity = _dbContext.Stops.FirstOrDefault(s => s.StopId == stop.StopId);

            if (stopEntity == null)
                return false;

            _dbContext.Stops.Remove(stopEntity);

            return _dbContext.SaveChanges() > 0;
        }
    }
}
