using WayMatcherBL.DtoModels;
using WayMatcherBL.LogicModels;

namespace WayMatcherBL.Interfaces
{
    public interface IEventService
    {
        public bool CreateEvent(EventDto eventDto, List<StopDto> stopList, UserDto user);
        public bool UpdateEvent(EventDto eventDto);
        public void CancelEvent(EventDto eventDto);
        public bool PlanSchedule(ScheduleDto schedule);
        public bool EventInvite(InviteDto invite);
        public bool AddEventMember(EventMemberDto eventMember); //EventMemberDto 
        public bool DeleteEventMember(EventMemberDto eventMember); //EventMemberDto 
        public bool AddStop(StopDto stop);
        public bool RemoveStops(StopDto stop);
        public EventDto GetEvent(EventDto eventDto);
        public List<EventDto> GetUserEventList(UserDto user);
        public List<EventDto> GetEventList(FilterDto filter);
        public void CalculateDistance(); //uses all stops 
        public void CalculateFuelConsumption(); //uses all stops and the car 
        public void CalculateTime(); //uses all stops
    }
}
