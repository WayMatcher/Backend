using WayMatcherBL.LogicModels;

namespace WayMatcherBL.DtoModels
{
    public class GetEventDetailsDto
    {
        public EventDto Event { get; set; }
        public List<EventMemberDto> EventMembers { get; set; }
        public List<StopDto> StopList { get; set; }
        public ScheduleDto Schedule { get; set; }
    }
}
