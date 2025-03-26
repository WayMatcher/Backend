namespace WayMatcherBL.DtoModels
{
    /// <summary>
    /// Data Transfer Object (DTO) for vehicle mappings.
    /// </summary>
    public class VehicleMappingDto
    {
        /// <summary>
        /// Gets or sets the unique identifier for the vehicle mapping.
        /// </summary>
        public int VehicleMappingId { get; set; }

        /// <summary>
        /// Gets or sets the fuel mileage of the vehicle.
        /// </summary>
        public decimal? FuelMilage { get; set; }

        /// <summary>
        /// Gets or sets additional information about the vehicle.
        /// </summary>
        public string AdditionalInfo { get; set; }

        /// <summary>
        /// Gets or sets the license plate of the vehicle.
        /// </summary>
        public string LicensePlate { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier for the vehicle.
        /// </summary>
        public int? VehicleId { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier for the user associated with the vehicle.
        /// </summary>
        public int? UserId { get; set; }

        /// <summary>
        /// Gets or sets the status identifier for the vehicle mapping.
        /// </summary>
        public int? StatusId { get; set; }
    }
}
