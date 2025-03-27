using WayMatcherBL.DtoModels;
using WayMatcherBL.LogicModels;

namespace WayMatcherAPI.Models
{
    /// <summary>
    /// Represents a request model for stop information.
    /// </summary>
    public class RequestStop
    {
        /// <summary>
        /// Gets or sets the unique identifier for the stop.
        /// </summary>
        public int StopId { get; set; }

        /// <summary>
        /// Gets or sets the sequence number of the stop.
        /// </summary>
        public int StopSequenceNumber { get; set; }

        /// <summary>
        /// Gets or sets the address of the stop.
        /// </summary>
        public AddressDto Address { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier for the event associated with the stop.
        /// </summary>
        public int EventId { get; set; }
    }
}
