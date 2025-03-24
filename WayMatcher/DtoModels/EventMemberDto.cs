using WayMatcherBL.Enums;
using WayMatcherBL.LogicModels;

namespace WayMatcherBL.DtoModels
{
    public class EventMemberDto
    {
        public int MemberId { get; set; }

        public EventRole EventRole { get; set; } //Pilot, Passenger

        public UserDto User { get; set; } //User

        public int EventId { get; set; }

        public int StatusId { get; set; } //StatusDto
    }
}
