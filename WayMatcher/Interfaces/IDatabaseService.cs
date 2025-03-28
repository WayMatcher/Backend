using WayMatcherBL.DtoModels;
using WayMatcherBL.LogicModels;

namespace WayMatcherBL.Interfaces
{
    /// <summary>
    /// Defines the contract for database-related operations.
    /// </summary>
    public interface IDatabaseService
    {
        /// <summary>
        /// Inserts a new user into the database.
        /// </summary>
        /// <param name="userModel">The user DTO to insert.</param>
        /// <returns>True if the user was successfully inserted; otherwise, false.</returns>
        public bool InsertUser(UserDto userModel);

        /// <summary>
        /// Updates an existing user in the database.
        /// </summary>
        /// <param name="userModel">The user DTO to update.</param>
        /// <returns>True if the user was successfully updated; otherwise, false.</returns>
        public bool UpdateUser(UserDto userModel);

        /// <summary>
        /// Gets a user from the database.
        /// </summary>
        /// <param name="user">The user DTO to get.</param>
        /// <returns>The user DTO.</returns>
        public UserDto GetUser(UserDto user);

        /// <summary>
        /// Gets the list of active users from the database.
        /// </summary>
        /// <returns>The list of active user DTOs.</returns>
        public List<UserDto> GetActiveUsers();

        /// <summary>
        /// Inserts a new address into the database.
        /// </summary>
        /// <param name="address">The address DTO to insert.</param>
        /// <returns>True if the address was successfully inserted; otherwise, false.</returns>
        public bool InsertAddress(AddressDto address);

        /// <summary>
        /// Updates an existing address in the database.
        /// </summary>
        /// <param name="address">The address DTO to update.</param>
        /// <returns>True if the address was successfully updated; otherwise, false.</returns>
        public bool UpdateAddress(AddressDto address);

        /// <summary>
        /// Gets an address from the database.
        /// </summary>
        /// <param name="address">The address DTO to get.</param>
        /// <returns>The address DTO.</returns>
        public AddressDto GetAddress(AddressDto address);

        /// <summary>
        /// Gets the address of a user from the database.
        /// </summary>
        /// <param name="user">The user DTO whose address is to be retrieved.</param>
        /// <returns>The address DTO.</returns>
        public AddressDto GetAddress(UserDto user);

        /// <summary>
        /// Gets the list of active addresses from the database.
        /// </summary>
        /// <returns>The list of active address DTOs.</returns>
        public List<AddressDto> GetActiveAddresses();

        /// <summary>
        /// Inserts a new vehicle into the database.
        /// </summary>
        /// <param name="vehicleModel">The vehicle DTO to insert.</param>
        /// <returns>True if the vehicle was successfully inserted; otherwise, false.</returns>
        public bool InsertVehicle(VehicleDto vehicleModel);

        /// <summary>
        /// Updates an existing vehicle in the database.
        /// </summary>
        /// <param name="vehicleModel">The vehicle DTO to update.</param>
        /// <returns>True if the vehicle was successfully updated; otherwise, false.</returns>
        public bool UpdateVehicle(VehicleDto vehicleModel);

        /// <summary>
        /// Gets a vehicle by its ID from the database.
        /// </summary>
        /// <param name="id">The ID of the vehicle to get.</param>
        /// <returns>The vehicle DTO.</returns>
        public VehicleDto GetVehicleById(int id);

        /// <summary>
        /// Gets the list of active vehicles from the database.
        /// </summary>
        /// <returns>The list of active vehicle DTOs.</returns>
        public List<VehicleDto> GetActiveVehicles();

        /// <summary>
        /// Gets the ID of a vehicle from the database.
        /// </summary>
        /// <param name="vehicleModel">The vehicle DTO to get the ID for.</param>
        /// <returns>The ID of the vehicle.</returns>
        public int GetVehicleId(VehicleDto vehicleModel);

        /// <summary>
        /// Inserts a new schedule into the database.
        /// </summary>
        /// <param name="scheduleModel">The schedule DTO to insert.</param>
        /// <returns>True if the schedule was successfully inserted; otherwise, false.</returns>
        public ScheduleDto InsertSchedule(ScheduleDto scheduleModel);

        /// <summary>
        /// Updates an existing schedule in the database.
        /// </summary>
        /// <param name="scheduleModel">The schedule DTO to update.</param>
        /// <returns>True if the schedule was successfully updated; otherwise, false.</returns>
        public bool UpdateSchedule(ScheduleDto scheduleModel);

        /// <summary>
        /// Gets a schedule by its ID from the database.
        /// </summary>
        /// <param name="id">The ID of the schedule to get.</param>
        /// <returns>The schedule DTO.</returns>
        public ScheduleDto GetScheduleById(int id);

        /// <summary>
        /// Gets the list of schedules associated with a user from the database.
        /// </summary>
        /// <param name="user">The user DTO whose schedules are to be retrieved.</param>
        /// <returns>The list of schedule DTOs.</returns>
        public List<ScheduleDto> GetUserSchedules(UserDto user);

        /// <summary>
        /// Gets the ID of a schedule from the database.
        /// </summary>
        /// <param name="scheduleModel">The schedule DTO to get the ID for.</param>
        /// <returns>The ID of the schedule.</returns>
        public int GetScheduleId(ScheduleDto scheduleModel);

        /// <summary>
        /// Inserts a new event into the database.
        /// </summary>
        /// <param name="eventModel">The event DTO to insert.</param>
        /// <returns>True if the event was successfully inserted; otherwise, false.</returns>
        public EventDto InsertEvent(EventDto eventModel);

        /// <summary>
        /// Updates an existing event in the database.
        /// </summary>
        /// <param name="eventModel">The event DTO to update.</param>
        /// <returns>True if the event was successfully updated; otherwise, false.</returns>
        public bool UpdateEvent(EventDto eventModel);

        /// <summary>
        /// Gets an event from the database.
        /// </summary>
        /// <param name="eventDto">The event DTO to get.</param>
        /// <returns>The event DTO.</returns>
        public EventDto GetEvent(EventDto eventDto);

        /// <summary>
        /// Gets the list of events from the database.
        /// </summary>
        /// <param name="isPilot">The filter to apply to the event list.</param>
        /// <returns>The list of event DTOs.</returns>
        public List<EventDto> GetEventList(bool? isPilot);

        /// <summary>
        /// Gets the list of all user events from the database.
        /// </summary>
        /// <param name="user">The filter to apply to the event list.</param>
        /// <returns>The list of event DTOs.</returns>
        public List<EventDto> GetUserEventList(UserDto user);

        /// <summary>
        /// Gets the ID of an event from the database.
        /// </summary>
        /// <param name="eventModel">The event DTO to get the ID for.</param>
        /// <returns>The ID of the event.</returns>
        public int GetEventId(EventDto eventModel);

        /// <summary>
        /// Gets the list of vehicles associated with a user from the database.
        /// </summary>
        /// <param name="user">The user DTO whose vehicles are to be retrieved.</param>
        /// <returns>The list of vehicle DTOs.</returns>
        public List<VehicleDto> GetUserVehicles(UserDto user);

        /// <summary>
        /// Inserts a new vehicle mapping into the database.
        /// </summary>
        /// <param name="vehicleMapping">The vehicle mapping DTO to insert.</param>
        /// <returns>True if the vehicle mapping was successfully inserted; otherwise, false.</returns>
        public bool InsertVehicleMapping(VehicleMappingDto vehicleMapping);

        /// <summary>
        /// Inserts a new stop into the database.
        /// </summary>
        /// <param name="stop">The stop DTO to insert.</param>
        /// <returns>True if the stop was successfully inserted; otherwise, false.</returns>
        public bool InsertStop(StopDto stop);

        /// <summary>
        /// Deletes a stop from the database.
        /// </summary>
        /// <param name="stop">The stop DTO to delete.</param>
        /// <returns>True if the stop was successfully deleted; otherwise, false.</returns>
        public bool DeleteStop(StopDto stop);

        /// <summary>
        /// Gets the list of stops associated with an event from the database.
        /// </summary>
        /// <param name="eventDto">The event DTO whose stops are to be retrieved.</param>
        /// <returns>The list of stop DTOs.</returns>
        public List<StopDto> GetStopList(EventDto eventDto);

        /// <summary>
        /// Inserts a new invite into the database.
        /// </summary>
        /// <param name="invite">The invite DTO to insert.</param>
        /// <returns>True if the invite was successfully inserted; otherwise, false.</returns>
        public bool InsertToInvite(InviteDto invite);

        /// <summary>
        /// Updates an existing invite in the database.
        /// </summary>
        /// <param name="invite">The invite DTO to update.</param>
        /// <returns>True if the invite was successfully updated; otherwise, false.</returns>
        public bool UpdateInvite(InviteDto invite);

        /// <summary>
        /// Gets the list of invites associated with an event from the database.
        /// </summary>
        /// <param name="eventDto">The event DTO whose invites are to be retrieved.</param>
        /// <returns>The list of invite DTOs.</returns>
        public List<InviteDto> GetInviteList(EventDto eventDto);

        /// <summary>
        /// Inserts a new event member into the database.
        /// </summary>
        /// <param name="eventMember">The event member DTO to insert.</param>
        /// <returns>True if the event member was successfully inserted; otherwise, false.</returns>
        public bool InsertToEventMember(EventMemberDto eventMember);

        /// <summary>
        /// Updates an existing event member in the database.
        /// </summary>
        /// <param name="eventMember">The event member DTO to update.</param>
        /// <returns>True if the event member was successfully updated; otherwise, false.</returns>
        public bool UpdateEventMember(EventMemberDto eventMember);

        /// <summary>
        /// Gets the owner of an event from the database.
        /// </summary>
        /// <param name="eventDto">The event DTO whose owner is to be retrieved.</param>
        /// <returns>The user DTO of the event owner.</returns>
        public UserDto GetEventOwner(EventDto eventDto);

        /// <summary>
        /// Gets the list of event members associated with an event from the database.
        /// </summary>
        /// <param name="eventDto">The event DTO whose members are to be retrieved.</param>
        /// <returns>The list of event member DTOs.</returns>
        public List<EventMemberDto> GetEventMemberList(EventDto eventDto);

        /// <summary>
        /// Inserts a new chat message into the database.
        /// </summary>
        /// <param name="textMessage">The chat message DTO to insert.</param>
        /// <returns>True if the chat message was successfully inserted; otherwise, false.</returns>
        public bool InsertChatMessage(ChatMessageDto textMessage);

        /// <summary>
        /// Gets the list of chat messages associated with an event member from the database.
        /// </summary>
        /// <param name="eventMember">The event member DTO whose chat messages are to be retrieved.</param>
        /// <returns>The list of chat message DTOs.</returns>
        public List<ChatMessageDto> GetChatMessageList(EventMemberDto eventMember);

        /// <summary>
        /// Inserts a new rating into the database.
        /// </summary>
        /// <param name="rating">The rating DTO to insert.</param>
        /// <returns>True if the rating was successfully inserted; otherwise, false.</returns>
        public bool InsertRating(RatingDto rating);

        /// <summary>
        /// Updates an existing rating in the database.
        /// </summary>
        /// <param name="rating">The rating DTO to update.</param>
        /// <returns>True if the rating was successfully updated; otherwise, false.</returns>
        public bool UpdateRating(RatingDto rating);

        /// <summary>
        /// Gets the list of ratings associated with a user from the database.
        /// </summary>
        /// <param name="user">The user DTO whose ratings are to be retrieved.</param>
        /// <returns>The list of rating DTOs.</returns>
        public List<RatingDto> GetRatingList(UserDto user);

        /// <summary>
        /// Gets a rating from the database.
        /// </summary>
        /// <param name="rating">The rating DTO to get.</param>
        /// <returns>The rating DTO.</returns>
        public RatingDto GetRating(RatingDto rating);

        /// <summary>
        /// Inserts a new notification into the database.
        /// </summary>
        /// <param name="notification">The notification DTO to insert.</param>
        /// <returns>True if the notification was successfully inserted; otherwise, false.</returns>
        public bool InsertNotification(NotificationDto notification);

        /// <summary>
        /// Gets the list of notifications associated with a user from the database.
        /// </summary>
        /// <param name="user">The user DTO whose notifications are to be retrieved.</param>
        /// <returns>The list of notification DTOs.</returns>
        public List<NotificationDto> GetNotificationList(UserDto user);

        /// <summary>
        /// Updates the read status of an existing notification.
        /// </summary>
        /// <param name="notification">The notification DTO containing the updated read status.</param>
        /// <returns><c>true</c> if the notification was successfully updated; otherwise, <c>false</c>.</returns>
        public bool UpdateNotification(NotificationDto notification);
    }
}
