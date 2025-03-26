using WayMatcherBL.Models;

namespace WayMatcherBL.LogicModels
{
    /// <summary>
    /// Data Transfer Object (DTO) for addresses.
    /// </summary>
    public class AddressDto
    {
        /// <summary>
        /// Gets or sets the unique identifier for the address.
        /// </summary>
        public int AddressId { get; set; }

        /// <summary>
        /// Gets or sets the city of the address.
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// Gets or sets the postal code of the address.
        /// </summary>
        public string PostalCode { get; set; }

        /// <summary>
        /// Gets or sets the street of the address.
        /// </summary>
        public string Street { get; set; }

        /// <summary>
        /// Gets or sets the country of the address.
        /// </summary>
        public string Country { get; set; }

        /// <summary>
        /// Gets or sets the country code of the address.
        /// </summary>
        public string CountryCode { get; set; }

        /// <summary>
        /// Gets or sets the region of the address.
        /// </summary>
        public string Region { get; set; }

        /// <summary>
        /// Gets or sets the state of the address.
        /// </summary>
        public string State { get; set; }

        /// <summary>
        /// Gets or sets the longitude of the address.
        /// </summary>
        public double? Longitude { get; set; }

        /// <summary>
        /// Gets or sets the latitude of the address.
        /// </summary>
        public double? Latitude { get; set; }

        /// <summary>
        /// Gets or sets the first address line.
        /// </summary>
        public string AddressLine1 { get; set; }

        /// <summary>
        /// Gets or sets the second address line.
        /// </summary>
        public string? AddressLine2 { get; set; }

        /// <summary>
        /// Gets or sets the status identifier for the address.
        /// </summary>
        public int? StatusId { get; set; }
    }
}
