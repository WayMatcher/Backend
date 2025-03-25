using WayMatcherBL.DtoModels;

namespace WayMatcherBL.LogicModels
{
    public class EventDto
    {
        public int EventId { get; set; }

        public int? EventTypeId { get; set; }

        public int? FreeSeats { get; set; }

        public string? Description { get; set; }

        public DateTime? StartTimestamp { get; set; }
        public List<EventMemberDto>? EventMembers { get; set; }
        public List<InviteDto>? InviteList { get; set; }
        public List<StopDto>? StopList { get; set; }
        public ScheduleDto? Schedule { get; set; }
        public int? ScheduleId { get; set; }
        public StatusDto? Status { get; set; }

    }
}
