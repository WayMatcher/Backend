using WayMatcherBL.LogicModels;

namespace WayMatcherAPI.Models
{
    /// <summary>
    /// Represents a request model for event information.
    /// </summary>
    public class RequestEvent
    {
        /// <summary>
        /// Gets or sets the user information.
        /// </summary>
        public UserDto User { get; set; }

        /// <summary>
        /// Gets or sets the event information.
        /// </summary>
        public EventDto Event { get; set; }
    }
}
