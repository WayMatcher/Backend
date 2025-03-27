using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using WayMatcherBL.DtoModels;
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

        /// <summary>
        /// Gets or sets the list of stops associated with the event.
        /// </summary>
        public List<StopDto> StopList { get; set; }

        /// <summary>
        /// Gets or sets the schedule for the event.
        /// </summary>
        public string Schedule { get; set; }
    }
}
