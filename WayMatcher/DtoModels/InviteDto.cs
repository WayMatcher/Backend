using WayMatcherBL.Enums;
using WayMatcherBL.LogicModels;

namespace WayMatcherBL.DtoModels
{
    /// <summary>
    /// Data Transfer Object (DTO) for invites.
    /// </summary>
    public class InviteDto
    {
        /// <summary>
        /// Gets or sets the unique identifier for the invite.
        /// </summary>
        public int InviteId { get; set; }

        /// <summary>
        /// Gets or sets the confirmation status identifier for the invite.
        /// </summary>
        public int? ConfirmationStatusId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the invite is a request.
        /// </summary>
        public bool IsRequest { get; set; }

        /// <summary>
        /// Gets or sets the role of the user in the event.
        /// </summary>
        public EventRole eventRole { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier for the event associated with the invite.
        /// </summary>
        public int? EventId { get; set; }

        /// <summary>
        /// Gets or sets the user associated with the invite.
        /// </summary>
        public UserDto? User { get; set; }

        /// <summary>
        /// Gets or sets the message associated with the invite.
        /// </summary>
        public string? Message { get; set; }
    }
}
