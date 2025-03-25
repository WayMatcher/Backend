﻿using Microsoft.IdentityModel.Tokens;
using WayMatcherBL.DtoModels;
using WayMatcherBL.Enums;
using WayMatcherBL.Interfaces;
using WayMatcherBL.LogicModels;

namespace WayMatcherBL.Services
{
    /// <summary>
    /// Provides services for managing events, including creation, updating, cancellation, and member management.
    /// </summary>
    public class EventService : IEventService
    {
        private readonly IDatabaseService _databaseService;
        private readonly IEmailService _emailService;

        /// <summary>
        /// Initializes a new instance of the <see cref="EventService"/> class.
        /// </summary>
        /// <param name="databaseService">The database service.</param>
        /// <param name="emailService">The email service.</param>
        public EventService(IDatabaseService databaseService, IEmailService emailService)
        {
            _databaseService = databaseService;
            _emailService = emailService;
        }

        /// <summary>
        /// Plans the schedule.
        /// </summary>
        /// <param name="schedule">The schedule DTO.</param>
        /// <returns><c>true</c> if the schedule was successfully planned; otherwise, <c>false</c>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the schedule is null.</exception>
        private bool PlanSchedule(ScheduleDto schedule)
        {
            if (schedule == null)
                throw new ArgumentNullException(nameof(schedule));
            return _databaseService.InsertSchedule(schedule);
        }

        /// <summary>
        /// Gets the address identifier.
        /// </summary>
        /// <param name="address">The address DTO.</param>
        /// <returns>The address identifier.</returns>
        private int GetAddressId(AddressDto address)
        {
            address.AddressId = _databaseService.GetAddress(address).AddressId;

            if (address.AddressId == -1)
            {
                _databaseService.InsertAddress(address);
                return _databaseService.GetAddress(address).AddressId;
            }

            return address.AddressId;
        }

        /// <summary>
        /// Adds a stop to an event.
        /// </summary>
        /// <param name="stop">The stop DTO.</param>
        /// <returns><c>true</c> if the stop was successfully added; otherwise, <c>false</c>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the stop is null or already exists.</exception>
        public bool AddStop(StopDto stop)
        {
            if (stop == null)
                throw new ArgumentNullException("Stop cannot be null");

            var eventDto = new EventDto()
            {
                EventId = stop.EventId,
            };

            var stopList = _databaseService.GetStopList(eventDto);

            if (!stopList.Contains(stop))
                throw new ArgumentNullException("Stop already exists");

            return _databaseService.InsertStop(stop);
        }

        /// <summary>
        /// Calculates the distance for an event.
        /// </summary>
        /// <exception cref="NotImplementedException">Thrown when the method is not implemented.</exception>
        public void CalculateDistance()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Calculates the fuel consumption for an event.
        /// </summary>
        /// <exception cref="NotImplementedException">Thrown when the method is not implemented.</exception>
        public void CalculateFuelConsumption()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Calculates the time for an event.
        /// </summary>
        /// <exception cref="NotImplementedException">Thrown when the method is not implemented.</exception>
        public void CalculateTime()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Cancels an event.
        /// </summary>
        /// <param name="eventDto">The event DTO.</param>
        /// <returns><c>true</c> if the event was successfully cancelled; otherwise, <c>false</c>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the event is null or a stop/member could not be removed.</exception>
        public bool CancelEvent(EventDto eventDto)
        {
            if (eventDto == null)
                throw new ArgumentNullException("Event cannot be null");

            eventDto.Status.StatusId = (int)State.Cancelled;

            foreach (var stop in _databaseService.GetStopList(eventDto))
            {
                if (!_databaseService.DeleteStop(stop))
                    throw new ArgumentNullException($"Stop {stop.StopId} could not be removed");
            }

            foreach (var member in _databaseService.GetEventMemberList(eventDto))
            {
                var user = _databaseService.GetUser(new UserDto() { UserId = member.User.UserId });
                var email = new EmailDto()
                {
                    Subject = $"Way: {eventDto.EventId} cancelled",
                    Body = $@"<html><head><meta http-equiv=""Content-Type"" content=""text/html; charset=utf-8"" /></head><body class=""bg-light""><div class=""container""><div class=""card my-10""><div class=""card-body""><h1 class=""h3 mb-2"">Way Cancellation</h1><h5 class=""text-teal-700"">Your Way has been cancelled</h5><hr>
<div class=""space-y-3""><p class=""text-gray-700"">Dear {eventDto.EventId},</p>
<p class=""text-gray-700"">We regret to inform you that your Way has been cancelled. If you have any questions or need further assistance, feel free to reach out to us.</p></div><hr></div></div></div></body></html>",
                    To = user.Email,
                    IsHtml = true
                };

                member.Status.StatusId = (int)State.Cancelled;

                if (!_databaseService.UpdateEventMember(member))
                    throw new ArgumentNullException($"Member {member.User.UserId} could not be removed");

                _emailService.SendEmail(email);
            }

            return _databaseService.UpdateEvent(eventDto);
        }

        /// <summary>
        /// Creates a new event.
        /// </summary>
        /// <param name="eventDto">The event DTO.</param>
        /// <param name="stopList">The list of stops.</param>
        /// <param name="user">The user DTO.</param>
        /// <param name="schedule">The schedule DTO.</param>
        /// <returns>The created event DTO.</returns>
        /// <exception cref="ArgumentNullException">Thrown when any of the parameters are null or the schedule/event could not be created.</exception>
        public EventDto CreateEvent(EventDto eventDto, List<StopDto> stopList, UserDto user, ScheduleDto schedule)
        {
            if (eventDto == null || user == null || stopList.IsNullOrEmpty() || schedule == null)
                throw new ArgumentNullException("Objects cannot be null");

            if (!PlanSchedule(schedule))
                throw new ArgumentNullException("Schedule could not be planned");

            eventDto.ScheduleId = _databaseService.GetScheduleId(schedule);

            if (!_databaseService.InsertEvent(eventDto))
                throw new ArgumentNullException("Event could not be created");

            foreach (var stop in stopList)
            {
                stop.Address.AddressId = GetAddressId(stop.Address);
                stop.EventId = _databaseService.GetEvent(eventDto).EventId;

                AddStop(stop);
            }



            var eventMember = new EventMemberDto()
            {
                EventId = _databaseService.GetEvent(eventDto).EventId,
                User = _databaseService.GetUser(user),
                Status = new StatusDto() { StatusId = (int)State.Active }

            };

            if (eventDto.EventTypeId == (int)EventRole.Passenger)
                eventMember.EventRole = EventRole.Passenger;
            else
                eventMember.EventRole = EventRole.Pilot;

            AddEventMember(eventMember);

            var email = new EmailDto()
            {
                Subject = $"Way: {eventDto.EventId} created",
                Body = $@"<html><head><meta http-equiv=""Content-Type"" content=""text/html; charset=utf-8"" /></head><body class=""bg-light""><div class=""container""><div class=""card my-10""><div class=""card-body""><h1 class=""h3 mb-2"">Way Confirmation</h1><h5 class=""text-teal-700"">Your Way has been successfully set up!</h5><hr><div class=""space-y-3"">
<p class=""text-gray-700"">Dear {user.Username},</p>
<p class=""text-gray-700"">We are pleased to inform you that your Way has been successfully set up. If you need to make any changes or require further assistance, feel free to reach out to us.</p></div><hr>        <a class=""btn btn-primary"" href=""#"" target=""_blank"">View Way Details</a></div></div></div></body></html>",
                To = user.Email,
                IsHtml = true
            };
            _emailService.SendEmail(email);

            return eventDto;
        }

        /// <summary>
        /// Gets an event by its DTO.
        /// </summary>
        /// <param name="eventDto">The event DTO.</param>
        /// <returns>The event DTO with additional details.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the event is null or has an invalid ID.</exception>
        public EventDto GetEvent(EventDto eventDto)
        {
            if (eventDto == null || eventDto.EventId == 0)
                throw new ArgumentNullException("Event cannot be null");

            eventDto = _databaseService.GetEvent(eventDto);
            eventDto.StopList = _databaseService.GetStopList(eventDto);
            eventDto.EventMembers = _databaseService.GetEventMemberList(eventDto);
            eventDto.InviteList = _databaseService.GetInviteList(eventDto);
            eventDto.Schedule = _databaseService.GetScheduleById(eventDto.ScheduleId ?? -1);

            return eventDto;
        }

        /// <summary>
        /// Gets a list of events.
        /// </summary>
        /// <param name="isPilot">Filter for pilot events.</param>
        /// <returns>A list of event DTOs.</returns>
        public List<EventDto> GetEventList(bool? isPilot)
        {
            var eventList = _databaseService.GetEventList(isPilot);
            foreach (var eventDto in eventList)
            {
                eventDto.StopList = _databaseService.GetStopList(eventDto);
                eventDto.EventMembers = _databaseService.GetEventMemberList(eventDto);
                eventDto.Schedule = _databaseService.GetScheduleById(eventDto.ScheduleId ?? -1);
            }

            return eventList;
        }

        /// <summary>
        /// Gets a list of events for a user.
        /// </summary>
        /// <param name="user">The user DTO.</param>
        /// <returns>A list of event DTOs.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the user is null.</exception>
        public List<EventDto> GetUserEventList(UserDto user)
        {
            if (user == null)
                throw new ArgumentNullException("User cannot be null");

            var userEvents = new List<EventDto>();
            var eventMembers = _databaseService.GetEventMemberList(new EventDto { EventId = user.UserId });

            foreach (var eventMember in eventMembers)
            {
                var eventDto = _databaseService.GetEvent(new EventDto { EventId = eventMember.EventId });
                if (eventDto != null)
                {
                    userEvents.Add(eventDto);
                }
            }

            return userEvents;
        }


        /// <summary>
        /// Sends an event invite.
        /// </summary>
        /// <param name="invite">The invite DTO.</param>
        /// <returns><c>true</c> if the invite was successfully sent; otherwise, <c>false</c>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the invite or user is null.</exception>
        public bool EventInvite(InviteDto invite)
        {
            var email = new EmailDto()
            {
                IsHtml = true
            };

            if (invite == null || invite.User == null)
                throw new ArgumentNullException("Invite cannot be null");

            invite.ConfirmationStatusId = (int)State.Pending;
            invite.User = _databaseService.GetUser(invite.User);

            if (!invite.IsRequest)
            {
                email.Subject = $"You have been Invited to a Way: {invite.EventId} as a {invite.eventRole.GetDescription()}!";
                email.Body = $@"<html><head><meta http-equiv=""Content-Type"" content=""text/html; charset=utf-8"" /></head><body class=""bg-light""><div class=""container""><div class=""card my-10""><div class=""card-body""><h1 class=""h3 mb-2"">You're Invited!</h1><h5 class=""text-teal-700"">You have been invited to a Way!</h5><hr><div class=""space-y-3""><p class=""text-gray-700"">Hello,</p><p class=""text-gray-700"">We are excited to inform you that you have been invited to join us for a Way </p><p class=""text-gray-700"">Please click the link below to confirm your attendance.</p><p class=""text-gray-700"">We hope to see you there!</p></div><hr>
<a class=""btn btn-primary"" href=""{invite.User.UserId}{invite.EventId}{(int)invite.eventRole}"" target=""_blank"">Confirm Your Attendance</a>
</div></div></div></body></html>";
                //add link to confirm attendance
                email.To = invite.User.Email;
            }
            else
            {
                email.Subject = $"Way ({invite.EventId}) Request from User: {invite.User.Username} as a {invite.eventRole.GetDescription()}";
                email.Body = $@"<html><head><meta http-equiv=""Content-Type"" content=""text/html; charset=utf-8"" /></head><body class=""bg-light""><div class=""container""><div class=""card my-10""><div class=""card-body""><h1 class=""h3 mb-2"">Request to Join the Way</h1><h5 class=""text-teal-700"">A user has requested to join the Way!</h5><hr><div class=""space-y-3""><p class=""text-gray-700"">Hello,</p><p class=""text-gray-700"">A user has expressed interest in joining the Way. Please review their request and consider granting access.</p><p class=""text-gray-700"">Here is the message from the user:</p>
<blockquote class=""text-gray-700 bg-gray-100 p-3 rounded"">{invite.Message}</blockquote><p class=""text-gray-700"">To accept or decline this request, please follow the link below.</p></div><hr>
<a class=""btn btn-primary"" href=""{invite.User.UserId}{invite.EventId}{(int)invite.eventRole}"" target=""_blank"">Review Request</a>
</div></div></div></body></html>";
                //add link to confirm attendance
                email.To = _databaseService.GetEventOwner(new EventDto() { EventId = invite.EventId ?? -1 }).Email;
            }
            _emailService.SendEmail(email);

            return _databaseService.InsertToInvite(invite);
        }

        /// <summary>
        /// Adds an event member.
        /// </summary>
        /// <param name="eventMemberDto">The event member DTO.</param>
        /// <returns><c>true</c> if the event member was successfully added; otherwise, <c>false</c>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the event member is null or could not be added.</exception>
        public bool AddEventMember(EventMemberDto eventMemberDto)
        {
            if (eventMemberDto == null)
                throw new ArgumentNullException("Event member cannot be null");

            eventMemberDto.Status.StatusId = (int)State.Active;

            if (!_databaseService.InsertToEventMember(eventMemberDto))
                throw new ArgumentNullException("Event member could not be added");

            var user = _databaseService.GetUser(new UserDto() { UserId = eventMemberDto.User.UserId });
            var email = new EmailDto()
            {
                Subject = $"Way: {eventMemberDto.EventId} joined",
                Body = $@"<html><head><meta http-equiv=""Content-Type"" content=""text/html; charset=utf-8"" /></head><body class=""bg-light""><div class=""container""><div class=""card my-10""><div class=""card-body""><h1 class=""h3 mb-2"">Way Join Confirmation</h1><h5 class=""text-teal-700"">You've successfully joined the Way!</h5><hr><div class=""space-y-3"">
<p class=""text-gray-700"">Dear {eventMemberDto.User.Username},</p>
<p class=""text-gray-700"">We are excited to inform you that you've successfully joined the Way. We look forward to your participation and hope you have a great experience.</p></div><hr></div></div></div></body></html>",
                To = user.Email,
                IsHtml = true

            };
            _emailService.SendEmail(email);

            return true;
        }

        /// <summary>
        /// Deletes an event member.
        /// </summary>
        /// <param name="eventMemberDto">The event member DTO.</param>
        /// <returns><c>true</c> if the event member was successfully deleted; otherwise, <c>false</c>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the event member is null.</exception>
        public bool DeleteEventMember(EventMemberDto eventMemberDto)
        {
            if (eventMemberDto == null)
                throw new ArgumentNullException("Event member cannot be null");

            var user = _databaseService.GetUser(new UserDto() { UserId = eventMemberDto.User.UserId });
            var email = new EmailDto()
            {
                Subject = $"Way: {eventMemberDto.EventId} kicked",
                Body = $@"<html><head><meta http-equiv=""Content-Type"" content=""text/html; charset=utf-8"" /></head><body class=""bg-light""><div class=""container""><div class=""card my-10""><div class=""card-body""><h1 class=""h3 mb-2"">Way Removal Notice</h1><h5 class=""text-teal-700"">You've been removed from the Way</h5><hr>
<div class=""space-y-3""><p class=""text-gray-700"">Dear {user.Username},</p>
<p class=""text-gray-700"">We regret to inform you that you have been removed from the Way. If you believe this was a mistake or have any questions, please feel free to reach out to us for further assistance.</p><p class=""text-gray-700"">We appreciate your understanding.</p></div><hr></div></div></div></body></html>",
                To = user.Email,
                IsHtml = true
            };

            eventMemberDto.Status.StatusId = (int)State.Cancelled;
            eventMemberDto.EventId = -1;
            eventMemberDto.User.UserId = -1;

            return _databaseService.UpdateEventMember(eventMemberDto);
        }

        /// <summary>
        /// Removes a stop from an event.
        /// </summary>
        /// <param name="stop">The stop DTO.</param>
        /// <returns><c>true</c> if the stop was successfully removed; otherwise, <c>false</c>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the stop is null.</exception>
        public bool RemoveStops(StopDto stop)
        {
            if (stop == null)
                throw new ArgumentNullException("Stop cannot be null");

            return _databaseService.DeleteStop(stop);
        }

        /// <summary>
        /// Updates an event with a new schedule.
        /// </summary>
        /// <param name="eventDto">The event DTO.</param>
        /// <param name="schedule">The schedule DTO.</param>
        /// <returns>The updated event DTO.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the event or schedule is null.</exception>
        public EventDto UpdateEvent(EventDto eventDto, ScheduleDto schedule)
        {
            if (eventDto == null || schedule == null)
                throw new ArgumentNullException("Objects cannot be null");

            eventDto.ScheduleId = _databaseService.GetScheduleId(schedule);

            if (!_databaseService.UpdateEvent(eventDto))
                throw new ArgumentNullException("Event could not be updated");

            foreach (var member in _databaseService.GetEventMemberList(eventDto))
            {
                var user = _databaseService.GetUser(new UserDto() { UserId = member.User.UserId });

                var email = new EmailDto()
                {
                    Subject = $"Way: {eventDto.EventId} changed",
                    Body = $@"<html><head><meta http-equiv=""Content-Type"" content=""text/html; charset=utf-8"" /></head><body class=""bg-light""><div class=""container""><div class=""card my-10""><div class=""card-body""><h1 class=""h3 mb-2"">Way Update</h1><h5 class=""text-teal-700"">Your Way details have changed</h5><hr><div class=""space-y-3"">
<p class=""text-gray-700"">Dear {user.Username},</p>
<p class=""text-gray-700"">We wanted to inform you that there has been a change to your Way {eventDto.EventId}. Please review the updated information to ensure everything meets your expectations.</p>
<p class=""text-gray-700"">If you have any questions or need further assistance, feel free to reach out to us.</p></div><hr></div></div></div></body</html>",
                    To = user.Email,
                    IsHtml = true
                };
                _emailService.SendEmail(email);
            }
            return eventDto;
        }

        /// <summary>
        /// Sends a chat message.
        /// </summary>
        /// <param name="message">The chat message DTO.</param>
        /// <returns><c>true</c> if the message was successfully sent; otherwise, <c>false</c>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the message is null.</exception>
        public bool SendChatMessage(ChatMessageDto message)
        {
            if (message == null)
                throw new ArgumentNullException("Message cannot be null");
            return _databaseService.InsertChatMessage(message);
        }

        /// <summary>
        /// Gets chat messages for an event member.
        /// </summary>
        /// <param name="eventMember">The event member DTO.</param>
        /// <returns>A list of chat message DTOs.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the event member is null.</exception>
        public List<ChatMessageDto> GetChatMessage(EventMemberDto eventMember)
        {
            if (eventMember == null)
                throw new ArgumentNullException("Event member cannot be null");
            return _databaseService.GetChatMessageList(eventMember);
        }

        /// <summary>
        /// Gets a list of users to invite to an event.
        /// </summary>
        /// <param name="eventDto">The event DTO.</param>
        /// <returns>A list of user DTOs.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the event is null.</exception>
        public List<UserDto> GetUserToInvite(EventDto eventDto)
        {
            if (eventDto == null)
                throw new ArgumentNullException("Event cannot be null");
            var userList = _databaseService.GetActiveUsers();
            var eventMembers = _databaseService.GetEventMemberList(eventDto);
            foreach (var member in eventMembers)
            {
                userList.RemoveAll(u => u.UserId.Equals(member.User.UserId));
            }
            return userList;
        }
    }
}
