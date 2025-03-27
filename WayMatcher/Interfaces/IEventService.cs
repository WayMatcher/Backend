using WayMatcherBL.DtoModels;
using WayMatcherBL.LogicModels;

namespace WayMatcherBL.Interfaces
{
    /// <summary>
    /// Defines the contract for event-related operations.
    /// </summary>
    public interface IEventService
    {
        /// <summary>
        /// Creates a new event.
        /// </summary>
        /// <param name="eventDto">The event DTO to create.</param>
        /// <param name="user">The user creating the event.</param>
        /// <returns>The created event DTO.</returns>
        public EventDto CreateEvent(EventDto eventDto, UserDto user);

        /// <summary>
        /// Updates an existing event.
        /// </summary>
        /// <param name="eventDto">The event DTO to update.</param>
        /// <param name="schedule">The schedule associated with the event.</param>
        /// <returns>The updated event DTO.</returns>
        public EventDto UpdateEvent(EventDto eventDto, ScheduleDto schedule);

        /// <summary>
        /// Cancels an event.
        /// </summary>
        /// <param name="eventDto">The event DTO to cancel.</param>
        /// <returns>True if the event was successfully canceled; otherwise, false.</returns>
        public bool CancelEvent(EventDto eventDto);

        /// <summary>
        /// Sends an event invite.
        /// </summary>
        /// <param name="invite">The invite DTO to send.</param>
        /// <returns>True if the invite was successfully sent; otherwise, false.</returns>
        public bool EventInvite(InviteDto invite);

        /// <summary>
        /// Adds a member to an event.
        /// </summary>
        /// <param name="eventMember">The event member DTO to add.</param>
        /// <returns>True if the member was successfully added; otherwise, false.</returns>
        public bool AddEventMember(EventMemberDto eventMember);

        /// <summary>
        /// Deletes a member from an event.
        /// </summary>
        /// <param name="eventMember">The event member DTO to delete.</param>
        /// <returns>True if the member was successfully deleted; otherwise, false.</returns>
        public bool DeleteEventMember(EventMemberDto eventMember);

        /// <summary>
        /// Adds a stop to an event.
        /// </summary>
        /// <param name="stop">The stop DTO to add.</param>
        /// <returns>True if the stop was successfully added; otherwise, false.</returns>
        public bool AddStop(StopDto stop);

        /// <summary>
        /// Removes stops from an event.
        /// </summary>
        /// <param name="stop">The stop DTO to remove.</param>
        /// <returns>True if the stops were successfully removed; otherwise, false.</returns>
        public bool RemoveStops(StopDto stop);

        /// <summary>
        /// Gets an event.
        /// </summary>
        /// <param name="eventDto">The event DTO to get.</param>
        /// <returns>The event DTO.</returns>
        public EventDto GetEvent(EventDto eventDto);

        /// <summary>
        /// Gets the list of events associated with a user.
        /// </summary>
        /// <param name="user">The user whose events are to be retrieved.</param>
        /// <returns>The list of event DTOs.</returns>
        public List<EventDto> GetUserEventList(UserDto user);

        /// <summary>
        /// Gets the list of events.
        /// </summary>
        /// <param name="filter">The filter to apply to the event list.</param>
        /// <returns>The list of event DTOs.</returns>
        public List<EventDto> GetEventList(bool? filter);

        /// <summary>
        /// Calculates the distance for all stops.
        /// </summary>
        public void CalculateDistance();

        /// <summary>
        /// Calculates the fuel consumption for all stops and the car.
        /// </summary>
        public void CalculateFuelConsumption();

        /// <summary>
        /// Calculates the time for all stops.
        /// </summary>
        public void CalculateTime();

        /// <summary>
        /// Sends a chat message.
        /// </summary>
        /// <param name="message">The chat message DTO to send.</param>
        /// <returns>True if the message was successfully sent; otherwise, false.</returns>
        public bool SendChatMessage(ChatMessageDto message);

        /// <summary>
        /// Gets the list of chat messages for an event member.
        /// </summary>
        /// <param name="eventMember">The event member DTO whose messages are to be retrieved.</param>
        /// <returns>The list of chat message DTOs.</returns>
        public List<ChatMessageDto> GetChatMessage(EventMemberDto eventMember);

        /// <summary>
        /// Gets the list of users to invite to an event.
        /// </summary>
        /// <param name="eventDto">The event DTO for which to get the users to invite.</param>
        /// <returns>The list of user DTOs.</returns>
        public List<UserDto> GetUserToInvite(EventDto eventDto);
    }
}
