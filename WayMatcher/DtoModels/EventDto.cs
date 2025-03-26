using WayMatcherBL.DtoModels;

namespace WayMatcherBL.LogicModels
{
    /// <summary>
    /// Data Transfer Object (DTO) for events.
    /// </summary>
    public class EventDto
    {
        /// <summary>
        /// Gets or sets the unique identifier for the event.
        /// </summary>
        public int EventId { get; set; }

        /// <summary>
        /// Gets or sets the type identifier for the event.
        /// </summary>
        public int? EventTypeId { get; set; }

        /// <summary>
        /// Gets or sets the number of free seats available for the event.
        /// </summary>
        public int? FreeSeats { get; set; }

        /// <summary>
        /// Gets or sets the description of the event.
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Gets or sets the start timestamp of the event.
        /// </summary>
        public DateTime? StartTimestamp { get; set; }

        /// <summary>
        /// Gets or sets the list of event members.
        /// </summary>
        public List<EventMemberDto>? EventMembers { get; set; }

        /// <summary>
        /// Gets or sets the list of invites for the event.
        /// </summary>
        public List<InviteDto>? InviteList { get; set; }

        /// <summary>
        /// Gets or sets the list of stops for the event.
        /// </summary>
        public List<StopDto>? StopList { get; set; }

        /// <summary>
        /// Gets or sets the schedule for the event.
        /// </summary>
        public ScheduleDto? Schedule { get; set; }

        /// <summary>
        /// Gets or sets the schedule identifier for the event.
        /// </summary>
        public int? ScheduleId { get; set; }

        /// <summary>
        /// Gets or sets the status of the event.
        /// </summary>
        public StatusDto? Status { get; set; }
    }
}
