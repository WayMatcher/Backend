using WayMatcherBL.LogicModels;

namespace WayMatcherAPI.Models
{
    /// <summary>
    /// Represents a request model for event member information.
    /// </summary>
    public class RequestEventMember
    {
        /// <summary>
        /// Gets or sets the event information.
        /// </summary>
        public EventDto Event { get; set; }

        /// <summary>
        /// Gets or sets the user information.
        /// </summary>
        public UserDto User { get; set; }

        /// <summary>
        /// Gets or sets the role of the user in the event.
        /// </summary>
        public int EventRole { get; set; }
    }
}
