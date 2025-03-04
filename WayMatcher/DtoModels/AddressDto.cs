using WayMatcherBL.Models;

namespace WayMatcherBL.LogicModels
{
    public class AddressDto
    {
        public int AddressId { get; set; }

        public string City { get; set; }

        public string PostalCode { get; set; }

        public string Street { get; set; }

        public string Country { get; set; }

        public string CountryCode { get; set; }

        public string Region { get; set; }

        public string State { get; set; }

        public double? Longitude { get; set; }

        public double? Latitude { get; set; }

        public string AddressLine1 { get; set; }

        public string AddressLine2 { get; set; }

        public int? StatusId { get; set; }

    }
}
