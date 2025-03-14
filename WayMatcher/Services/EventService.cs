﻿using WayMatcherBL.DtoModels;
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
                EventId = stop.EventId
            };

            var stopList = _databaseService.GetStopList(eventDto);

            //if address does not exist, create new one #TODO

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

            //#TODO
            //foreach member in event
            //schedule in event
            }

            _databaseService.UpdateEvent(eventDto);
        }

        public bool CreateEvent(EventDto eventDto)
        {
            if (eventDto == null)
                return false;

            return _databaseService.InsertEvent(eventDto);
        }

        public EventDto GetEvent(EventDto eventDto)
        {
            if (eventDto == null)
                return null;

            return _databaseService.GetEvent(eventDto);
        }

        public List<EventDto> GetEventList()
        {
            //findet man mit eventNamen, eventZeit, eventStart und eventZiel, mithilfe von VIEWS zurückbekommen #TODO
            return _databaseService.GetFilteredEventList();

            throw new NotImplementedException();
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
            //invite.IsRequest = false;
            //invite.IsRequest = true;
            return _databaseService.InsertToInvite(invite);
        }

        public bool InviteExcepted(EventMemberDto eventMemberDto)
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
