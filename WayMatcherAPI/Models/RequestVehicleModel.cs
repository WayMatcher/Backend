namespace WayMatcherAPI.Models
{
    public class RequestVehicleModel
    {
        public int? VehicleId { get; set; }
        public string? Model { get; set; }
        public int? Seats { get; set; }
        public int? YearOfManufacture { get; set; }
        public string? ManufacturerName { get; set; }
        public string? LicensePlate { get; set; }
        public string? AdditionalInfo { get; set; }
        public decimal? FuelMilage { get; set; }
    }
}