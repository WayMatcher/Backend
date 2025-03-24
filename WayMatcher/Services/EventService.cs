using Microsoft.IdentityModel.Tokens;
using WayMatcherBL.DtoModels;
using WayMatcherBL.Enums;
using WayMatcherBL.Interfaces;
using WayMatcherBL.LogicModels;

namespace WayMatcherBL.Services
{
    public class EventService : IEventService
    {
        private IDatabaseService _databaseService;
        private IEmailService _emailService;

        public EventService(IDatabaseService databaseService, IEmailService emailService)
        {
            _databaseService = databaseService;
            _emailService = emailService;
        }

        private bool PlanSchedule(ScheduleDto schedule)
        {
            if (schedule == null)
                return false;

            return _databaseService.InsertSchedule(schedule);
        }

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

        public void CalculateDistance()
        {
            throw new NotImplementedException();
        }

        public void CalculateFuelConsumption()
        {
            throw new NotImplementedException();
        }

        public void CalculateTime()
        {
            throw new NotImplementedException();
        }

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
                    Subject = $"Event: {eventDto.EventId} cancelled",
                    Body = $@"<html><head><meta http-equiv=""Content-Type"" content=""text/html; charset=utf-8"" /></head><body class=""bg-light""><div class=""container""><div class=""card my-10""><div class=""card-body""><h1 class=""h3 mb-2"">Event Cancellation</h1><h5 class=""text-teal-700"">Your event has been cancelled</h5><hr><div class=""space-y-3""><p class=""text-gray-700"">Dear {eventDto.EventId},</p><p class=""text-gray-700"">We regret to inform you that your event has been cancelled. If you have any questions or need further assistance, feel free to reach out to us.</p></div><hr></div></div></div></body></html>",
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
                Subject = $"Event: {eventDto.EventId} created",
                Body = $@"<html><head><meta http-equiv=""Content-Type"" content=""text/html; charset=utf-8"" /><style></style></head><body class=""bg-light""><div class=""container""><div class=""card my-10""><div class=""card-body""><h1 class=""h3 mb-2"">Event Confirmation</h1><h5 class=""text-teal-700"">Your event has been successfully set up!</h5><hr><div class=""space-y-3"">  <p class=""text-gray-700"">Dear {user.Username},</p>  <p class=""text-gray-700"">We are pleased to inform you that your event has been successfully set up. If you need to make any changes or require further assistance, feel free to reach out to us.</p></div><hr>        <a class=""btn btn-primary"" href=""#"" target=""_blank"">View Event Details</a></div></div></div></body></html>",
                To = user.Email,
                IsHtml = true
            };
            _emailService.SendEmail(email);

            return eventDto;
        }

        public EventDto GetEvent(EventDto eventDto)
        {
            if (eventDto == null)
                return null;

            eventDto = _databaseService.GetEvent(eventDto);
            eventDto.StopList = _databaseService.GetStopList(eventDto);
            eventDto.EventMembers = _databaseService.GetEventMemberList(eventDto);

            eventDto.Schedule = _databaseService.GetScheduleById(eventDto.ScheduleId ?? -1);

            return eventDto;
        }

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

        public List<EventDto> GetUserEventList(UserDto user)
        {
            if (user == null)
                return null;

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

        public bool EventInvite(InviteDto invite)
        {
            if (invite == null)
                throw new ArgumentNullException("Invite cannot be null");

            invite.ConfirmationStatusId = (int)State.Unread;

            return _databaseService.InsertToInvite(invite);
        }

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
                Subject = $"Event: {eventMemberDto.EventId} joined",
                Body = $@"<html><head><meta http-equiv=""Content-Type"" content=""text/html; charset=utf-8"" /></head><body class=""bg-light""><div class=""container""><div class=""card my-10""><div class=""card-body""><h1 class=""h3 mb-2"">Event Join Confirmation</h1><h5 class=""text-teal-700"">You've successfully joined the event!</h5><hr><div class=""space-y-3""><p class=""text-gray-700"">Dear [User's Name],</p><p class=""text-gray-700"">We are excited to inform you that you've successfully joined the event. We look forward to your participation and hope you have a great experience.</p><p class=""text-gray-700"">If you have any questions or need further assistance, feel free to reach out to us.</p></div><hr></div></div></div></body></html>",
                To = user.Email,
                IsHtml = true

            };
            _emailService.SendEmail(email);

            return true;
        }

        public bool DeleteEventMember(EventMemberDto eventMemberDto)
        {
            if (eventMemberDto == null)
                return false;

            var user = _databaseService.GetUser(new UserDto() { UserId = eventMemberDto.User.UserId });
            var email = new EmailDto()
            {
                Subject = $"Event: {eventMemberDto.EventId} kicked",
                Body = $@"<html><head><meta http-equiv=""Content-Type"" content=""text/html; charset=utf-8"" /></head><body class=""bg-light""><div class=""container""><div class=""card my-10""><div class=""card-body""><h1 class=""h3 mb-2"">Event Removal Notice</h1><h5 class=""text-teal-700"">You've been removed from the event</h5><hr><div class=""space-y-3""><p class=""text-gray-700"">Dear {user.Username},</p><p class=""text-gray-700"">We regret to inform you that you have been removed from the event. If you believe this was a mistake or have any questions, please feel free to reach out to us for further assistance.</p><p class=""text-gray-700"">We appreciate your understanding.</p></div><hr></div></div></div></body></html>",
                To = user.Email,
                IsHtml = true
            };

            eventMemberDto.Status.StatusId = (int)State.Cancelled;
            eventMemberDto.EventId = -1;
            eventMemberDto.User.UserId = -1;

            return _databaseService.UpdateEventMember(eventMemberDto);
        }

        public bool RemoveStops(StopDto stop)
        {
            if (stop == null)
                throw new ArgumentNullException("Stop cannot be null");

            return _databaseService.DeleteStop(stop);
        }

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
                    Subject = $"Event: {eventDto.EventId} changed",
                    Body = $@"<html><head><meta http-equiv=""Content-Type"" content=""text/html; charset=utf-8"" /></head><body class=""bg-light""><div class=""container""><div class=""card my-10""><div class=""card-body""><h1 class=""h3 mb-2"">Event Update</h1><h5 class=""text-teal-700"">Your event details have changed</h5><hr><div class=""space-y-3""><p class=""text-gray-700"">Dear {user.Username},</p><p class=""text-gray-700"">We wanted to inform you that there has been a change to your event {eventDto.EventId}. Please review the updated information to ensure everything meets your expectations.</p><p class=""text-gray-700"">If you have any questions or need further assistance, feel free to reach out to us.</p></div><hr></div></div></div></body</html>",
                    To = user.Email,
                    IsHtml = true
                };
                _emailService.SendEmail(email);
            }
            return eventDto;
        }

        public bool SendChatMessage(ChatMessageDto message)
        {
            if (message == null)
                return false;

            return _databaseService.InsertChatMessage(message);
        }

        public List<ChatMessageDto> GetChatMessage(EventMemberDto eventMember)
        {
            if (eventMember == null)
                return null;

            return _databaseService.GetChatMessageList(eventMember);
        }
    }
}
