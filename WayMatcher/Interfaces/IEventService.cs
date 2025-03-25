using WayMatcherBL.DtoModels;
using WayMatcherBL.LogicModels;

namespace WayMatcherBL.Interfaces
{
    public interface IEventService
    {
        public EventDto CreateEvent(EventDto eventDto, List<StopDto> stopList, UserDto user, ScheduleDto scheduleDto);
        public EventDto UpdateEvent(EventDto eventDto, ScheduleDto schedule);
        public bool CancelEvent(EventDto eventDto);
        public bool EventInvite(InviteDto invite);
        public bool AddEventMember(EventMemberDto eventMember);
        public bool DeleteEventMember(EventMemberDto eventMember);
        public bool AddStop(StopDto stop);
        public bool RemoveStops(StopDto stop);
        public EventDto GetEvent(EventDto eventDto);
        public List<EventDto> GetUserEventList(UserDto user);
        public List<EventDto> GetEventList(bool? filter);
        public void CalculateDistance(); //uses all stops 
        public void CalculateFuelConsumption(); //uses all stops and the car 
        public void CalculateTime(); //uses all stops
        public bool SendChatMessage(ChatMessageDto message);
        public List<ChatMessageDto> GetChatMessage(EventMemberDto eventMember);
        public List<UserDto> GetUserToInvite(EventDto eventDto);
    }
}
