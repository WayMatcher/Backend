namespace WayMatcherBL.DtoModels
{
    /// <summary>
    /// Data Transfer Object (DTO) for status information.
    /// </summary>
    public class StatusDto
    {
        /// <summary>
        /// Gets or sets the unique identifier for the status.
        /// </summary>
        public int StatusId { get; set; }

        /// <summary>
        /// Gets or sets the description of the status.
        /// </summary>
        public string StatusDescription { get; set; }
    }
}
