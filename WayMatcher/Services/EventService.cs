using Microsoft.IdentityModel.Tokens;
using WayMatcherBL.DtoModels;
using WayMatcherBL.Enums;
using WayMatcherBL.Interfaces;
using WayMatcherBL.LogicModels;

namespace WayMatcherBL.Services
{
    public class EventService : IEventService
    {
        IDatabaseService _databaseService;

        public EventService(IDatabaseService databaseService)
        {
            _databaseService = databaseService;
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
                member.StatusId = (int)State.Cancelled;
                _databaseService.DeleteEventMember(member);
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

                var eventMember = new EventMemberDto()
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

            invite.ConfirmationStatusId = 0;
            return _databaseService.InsertToInvite(invite);
        }

        public bool AddEventMember(EventMemberDto eventMemberDto)
        {
            if (eventMemberDto == null)
                return false;

            return _databaseService.InsertToEventMember(eventMemberDto);
        }

        public bool KickUserFromEvent(EventMemberDto eventMemberDto)
        {
            if (eventMemberDto == null)
                return false;

            return _databaseService.DeleteEventMember(eventMemberDto);
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

            return _databaseService.UpdateEvent(eventDto);
        }
    }
}
