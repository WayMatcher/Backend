namespace WayMatcherAPI.Models
{
    /// <summary>
    /// Represents a request model for vehicle information.
    /// </summary>
    public class RequestVehicleModel
    {
        /// <summary>
        /// Gets or sets the unique identifier for the vehicle.
        /// </summary>
        public int? VehicleId { get; set; }

        /// <summary>
        /// Gets or sets the model of the vehicle.
        /// </summary>
        public string? Model { get; set; }

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
        public string? ManufacturerName { get; set; }

        /// <summary>
        /// Gets or sets the license plate of the vehicle.
        /// </summary>
        public string? LicensePlate { get; set; }

        /// <summary>
        /// Gets or sets additional information about the vehicle.
        /// </summary>
        public string? AdditionalInfo { get; set; }

        /// <summary>
        /// Gets or sets the fuel mileage of the vehicle.
        /// </summary>
        public decimal? FuelMilage { get; set; }
    }
}