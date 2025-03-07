namespace WayMatcherBL.DtoModels
{
    public class VehicleMappingDto
    {
        public int VehicleMappingId { get; set; }

        public decimal? FuelMilage { get; set; }

        public string AdditionalInfo { get; set; }

        public string LicensePlate { get; set; }

        public int? VehicleId { get; set; }

        public int? UserId { get; set; }

        public int? StatusId { get; set; }
    }
}
