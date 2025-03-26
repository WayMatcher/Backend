namespace WayMatcherBL.LogicModels
{
    /// <summary>
    /// Data Transfer Object (DTO) for vehicles.
    /// </summary>
    public class VehicleDto
    {
        /// <summary>
        /// Gets or sets the unique identifier for the vehicle.
        /// </summary>
        public int VehicleId { get; set; }

        /// <summary>
        /// Gets or sets the model of the vehicle.
        /// </summary>
        public string Model { get; set; }

        /// <summary>
        /// Gets or sets the number of seats in the vehicle.
        /// </summary>
        public int? Seats { get; set; }

        /// <summary>
        /// Gets or sets the year the vehicle was manufactured.
        /// </summary>
        public int? YearOfManufacture { get; set; }

        /// <summary>
        /// Gets or sets the name of the vehicle manufacturer.
        /// </summary>
        public string ManufacturerName { get; set; }
    }
}
