using WayMatcherBL.LogicModels;

namespace WayMatcherAPI.Models
{
    public class RequestEventMember
    {
        public EventDto Event { get; set; }
        public UserDto User { get; set; }
        public int EventRole { get; set; }
    }
}
