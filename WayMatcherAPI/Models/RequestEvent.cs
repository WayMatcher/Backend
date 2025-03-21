using WayMatcherBL.DtoModels;
using WayMatcherBL.LogicModels;

namespace WayMatcherAPI.Models
{
    public class RequestEvent
    {
        public UserDto User { get; set; }
        public EventDto Event { get; set; }
        public List<StopDto> StopList { get; set; }
        public ScheduleDto Schedule { get; set; }
    }
}
