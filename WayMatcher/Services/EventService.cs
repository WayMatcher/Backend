using Microsoft.IdentityModel.Tokens;
using WayMatcherBL.DtoModels;
using WayMatcherBL.Enums;
using WayMatcherBL.Interfaces;
using WayMatcherBL.LogicModels;
using WayMatcherBL.Models;

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

        public bool AddStop(StopDto stop)
        {
            var eventDto = new EventDto()
            {
                EventId = stop.EventId,
            };

            var stopList = _databaseService.GetStopList(eventDto);

            if (stopList.Contains(stop))
                return _databaseService.InsertStop(stop);

            return false;
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

        public void CancelEvent(EventDto eventDto)
        {
            eventDto.StatusId = (int)State.Cancelled;

            foreach (var stop in _databaseService.GetStopList(eventDto))
            {
                _databaseService.DeleteStop(stop);
            }

            foreach (var member in _databaseService.GetEventMemberList(eventDto))
            {
                var user = _databaseService.GetUser(new UserDto() { UserId = member.UserId });
                var email = new EmailDto()
                {
                    Subject = $"Event: {eventDto.EventId} cancelled",
                    Body = $@"<html><head><meta http-equiv=""Content-Type"" content=""text/html; charset=utf-8"" /></head><body class=""bg-light""><div class=""container""><div class=""card my-10""><div class=""card-body""><h1 class=""h3 mb-2"">Event Cancellation</h1><h5 class=""text-teal-700"">Your event has been cancelled</h5><hr><div class=""space-y-3""><p class=""text-gray-700"">Dear {eventDto.EventId},</p><p class=""text-gray-700"">We regret to inform you that your event has been cancelled. If you have any questions or need further assistance, feel free to reach out to us.</p></div><hr></div></div></div></body></html>",
                    To = user.Email,
                    IsHtml = true
                };
                _emailService.SendEmail(email);

                member.StatusId = (int)State.Cancelled;
                member.EventId = -1;
                member.UserId = -1;
                _databaseService.UpdateEventMember(member);
            }

            _databaseService.UpdateEvent(eventDto);



        }

        public bool CreateEvent(EventDto eventDto, List<StopDto> stopList, UserDto user)
        {
            if (eventDto == null || user == null || stopList.IsNullOrEmpty())
                return false;

            if (_databaseService.InsertEvent(eventDto))
            {
                foreach (var stop in stopList)
                {
                    stop.Address.AddressId = _databaseService.GetAddressId(stop.Address);

                    if (stop.Address.AddressId == -1)
                    {
                        _databaseService.InsertAddress(stop.Address);
                        stop.Address.AddressId = _databaseService.GetAddressId(stop.Address);
                    }

                    stop.EventId = _databaseService.GetEvent(eventDto).EventId;
                    AddStop(stop);
                }

                var eventMember = new EventMemberDto() //ersteller des Eventes
                {
                    EventId = _databaseService.GetEvent(eventDto).EventId,
                    UserId = _databaseService.GetUser(user).UserId,
                    StatusId = (int)State.Active,
                };

                if (eventDto.EventRole == (int)EventRole.Passenger)
                    eventMember.EventRole = (int)EventRole.Passenger;
                else
                    eventMember.EventRole = (int)EventRole.Pilot;

                AddEventMember(eventMember);

                var email = new EmailDto()
                {
                    Subject = $"Event: {eventDto.EventId} created",
                    Body = $@"<html><head><meta http-equiv=""Content-Type"" content=""text/html; charset=utf-8"" /><style></style></head><body class=""bg-light""><div class=""container""><div class=""card my-10""><div class=""card-body""><h1 class=""h3 mb-2"">Event Confirmation</h1><h5 class=""text-teal-700"">Your event has been successfully set up!</h5><hr><div class=""space-y-3"">  <p class=""text-gray-700"">Dear {user.Username},</p>  <p class=""text-gray-700"">We are pleased to inform you that your event has been successfully set up. If you need to make any changes or require further assistance, feel free to reach out to us.</p></div><hr>        <a class=""btn btn-primary"" href=""#"" target=""_blank"">View Event Details</a></div></div></div></body></html>",
                    To = user.Email,
                    IsHtml = true
                };
                _emailService.SendEmail(email);

                return true;
            }
            return false;
        }

        public EventDto GetEvent(EventDto eventDto)
        {
            if (eventDto == null)
                return null;

            return _databaseService.GetEvent(eventDto);
        }

        public List<EventDto> GetEventList(FilterDto filter)
        {
            if (filter == null)
                return _databaseService.GetFilteredEventList(new FilterDto());

            if (filter.StartTime != null)
                filter.StartTime.ScheduleId = _databaseService.GetScheduleId(filter.StartTime);
            if (filter.StopLocation != null)
                filter.StopLocation.AddressId = _databaseService.GetAddressId(filter.StopLocation);
            if (filter.DestinationLocation != null)
                filter.DestinationLocation.AddressId = _databaseService.GetAddressId(filter.DestinationLocation);

            return _databaseService.GetFilteredEventList(filter);
        }

        public List<EventDto> GetUserEventList(UserDto user)
        {
            //mithilfe von VIEWS in der datenbank holen #TODO
            throw new NotImplementedException();
        }

        public bool EventInvite(InviteDto invite)
        {
            if (invite == null)
                return false;

            invite.ConfirmationStatusId = (int)State.Unread;
            return _databaseService.InsertToInvite(invite);
        }

        public bool AddEventMember(EventMemberDto eventMemberDto)
        {
            if (eventMemberDto == null)
                return false;

            eventMemberDto.StatusId = (int)State.Active;

            if (_databaseService.InsertToEventMember(eventMemberDto))
            {
                var user = _databaseService.GetUser(new UserDto() { UserId = eventMemberDto.UserId });
                var email = new EmailDto()
                {
                    Subject = $"Event: {eventMemberDto.EventId} joined",
                    Body = $@"<html><head><meta http-equiv=""Content-Type"" content=""text/html; charset=utf-8"" /></head><body class=""bg-light""><div class=""container""><div class=""card my-10""><div class=""card-body""><h1 class=""h3 mb-2"">Event Join Confirmation</h1><h5 class=""text-teal-700"">You've successfully joined the event!</h5><hr><div class=""space-y-3""><p class=""text-gray-700"">Dear [User's Name],</p><p class=""text-gray-700"">We are excited to inform you that you've successfully joined the event. We look forward to your participation and hope you have a great experience.</p><p class=""text-gray-700"">If you have any questions or need further assistance, feel free to reach out to us.</p></div><hr></div></div></div></body></html>",
                    To = user.Email,
                    IsHtml = true
                };
            }

            return false;
        }

        public bool DeleteEventMember(EventMemberDto eventMemberDto)
        {
            if (eventMemberDto == null)
                return false;

            var user = _databaseService.GetUser(new UserDto() { UserId = eventMemberDto.UserId });
            var email = new EmailDto()
            {
                Subject = $"Event: {eventMemberDto.EventId} kicked",
                Body = $@"<html><head><meta http-equiv=""Content-Type"" content=""text/html; charset=utf-8"" /></head><body class=""bg-light""><div class=""container""><div class=""card my-10""><div class=""card-body""><h1 class=""h3 mb-2"">Event Removal Notice</h1><h5 class=""text-teal-700"">You've been removed from the event</h5><hr><div class=""space-y-3""><p class=""text-gray-700"">Dear {user.Username},</p><p class=""text-gray-700"">We regret to inform you that you have been removed from the event. If you believe this was a mistake or have any questions, please feel free to reach out to us for further assistance.</p><p class=""text-gray-700"">We appreciate your understanding.</p></div><hr></div></div></div></body></html>",
                To = user.Email,
                IsHtml = true
            };

            eventMemberDto.StatusId = (int)State.Cancelled;
            eventMemberDto.EventId = -1;
            eventMemberDto.UserId = -1;

            return _databaseService.UpdateEventMember(eventMemberDto);
        }

        public bool PlanSchedule(ScheduleDto schedule)
        {
            if (schedule == null)
                return false;

            return _databaseService.InsertSchedule(schedule);
        }

        public bool RemoveStops(StopDto stop)
        {
            if (stop == null)
                return false;

            return _databaseService.DeleteStop(stop);
        }

        public bool UpdateEvent(EventDto eventDto)
        {
            if (eventDto == null)
                return false;

            if (_databaseService.UpdateEvent(eventDto))
            {
                foreach (var member in _databaseService.GetEventMemberList(eventDto))
                {
                    var user = _databaseService.GetUser(new UserDto() { UserId = member.UserId });

                    var email = new EmailDto()
                    {
                        Subject = $"Event: {eventDto.EventId} changed",
                        Body = $@"<html><head><meta http-equiv=""Content-Type"" content=""text/html; charset=utf-8"" /></head><body class=""bg-light""><div class=""container""><div class=""card my-10""><div class=""card-body""><h1 class=""h3 mb-2"">Event Update</h1><h5 class=""text-teal-700"">Your event details have changed</h5><hr><div class=""space-y-3""><p class=""text-gray-700"">Dear {user.Username},</p><p class=""text-gray-700"">We wanted to inform you that there has been a change to your event {eventDto.EventId}. Please review the updated information to ensure everything meets your expectations.</p><p class=""text-gray-700"">If you have any questions or need further assistance, feel free to reach out to us.</p></div><hr></div></div></div></body</html>",
                        To = user.Email,
                        IsHtml = true
                    };
                    _emailService.SendEmail(email);
                }

                return true;
            }

            return false;
        }
    }
}
