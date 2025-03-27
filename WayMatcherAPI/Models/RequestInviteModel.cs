using WayMatcherBL.LogicModels;

namespace WayMatcherAPI.Models
{
    /// <summary>
    /// Represents a request model for inviting a user to an event.
    /// </summary>
    public class RequestInviteModel
    {
        /// <summary>
        /// Gets or sets the unique identifier for the user.
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier for the event.
        /// </summary>
        public int EventId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the user is a pilot.
        /// </summary>
        public bool IsPilot { get; set; }

        /// <summary>
        /// Gets or sets the message associated with the invite.
        /// </summary>
        public string Message { get; set; }
    }
}
