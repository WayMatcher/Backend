using WayMatcherBL.LogicModels;

namespace WayMatcherBL.DtoModels
{
    /// <summary>
    /// Data Transfer Object (DTO) for filtering criteria.
    /// </summary>
    public class FilterDto
    {
        /// <summary>
        /// Gets or sets the start time schedule for the filter.
        /// </summary>
        public ScheduleDto? StartTime { get; set; }

        /// <summary>
        /// Gets or sets the stop location address for the filter.
        /// </summary>
        public AddressDto? StopLocation { get; set; }

        /// <summary>
        /// Gets or sets the destination location address for the filter.
        /// </summary>
        public AddressDto? DestinationLocation { get; set; }
    }
}
