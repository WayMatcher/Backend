using WayMatcherBL.LogicModels;

namespace WayMatcherAPI.Models
{
    public class RequestInviteModel
    {
        public UserDto User { get; set; }
        public EventDto Event { get; set; }
    }
}
