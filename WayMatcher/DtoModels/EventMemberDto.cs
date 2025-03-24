using WayMatcherBL.Enums;
using WayMatcherBL.LogicModels;

namespace WayMatcherBL.DtoModels
{
    public class EventMemberDto
    {
        public int MemberId { get; set; }

        public EventRole EventRole { get; set; } 

        public UserDto User { get; set; } 

        public int EventId { get; set; }

        public StatusDto? Status { get; set; } 
    }
}
