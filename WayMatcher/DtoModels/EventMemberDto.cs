using WayMatcherBL.Enums;
using WayMatcherBL.LogicModels;

namespace WayMatcherBL.DtoModels
{
    /// <summary>
    /// Data Transfer Object (DTO) for event members.
    /// </summary>
    public class EventMemberDto
    {
        /// <summary>
        /// Gets or sets the unique identifier for the event member.
        /// </summary>
        public int MemberId { get; set; }

        /// <summary>
        /// Gets or sets the role of the event member.
        /// </summary>
        public EventRole EventRole { get; set; }

        /// <summary>
        /// Gets or sets the user associated with the event member.
        /// </summary>
        public UserDto User { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier for the event associated with the event member.
        /// </summary>
        public int EventId { get; set; }

        /// <summary>
        /// Gets or sets the status of the event member.
        /// </summary>
        public StatusDto? Status { get; set; }
    }
}
