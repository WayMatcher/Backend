namespace WayMatcherBL.LogicModels
{
    /// <summary>
    /// Data Transfer Object (DTO) for users.
    /// </summary>
    public class UserDto
    {
        /// <summary>
        /// Gets or sets the unique identifier for the user.
        /// </summary>
        public int? UserId { get; set; }

        /// <summary>
        /// Gets or sets the first name of the user.
        /// </summary>
        public string Firstname { get; set; }

        /// <summary>
        /// Gets or sets the last name of the user.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the username of the user.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets the password of the user.
        /// </summary>
        public string? Password { get; set; }

        /// <summary>
        /// Gets or sets the email of the user.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the telephone number of the user.
        /// </summary>
        public string Telephone { get; set; }

        /// <summary>
        /// Gets or sets additional description about the user.
        /// </summary>
        public string? AdditionalDescription { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the user's license is verified.
        /// </summary>
        public bool? LicenseVerified { get; set; }

        /// <summary>
        /// Gets or sets the profile picture of the user.
        /// </summary>
        public byte[]? ProfilePicture { get; set; }

        /// <summary>
        /// Gets or sets the creation date of the user.
        /// </summary>
        public DateTime? CreationDate { get; set; }

        /// <summary>
        /// Gets or sets the address of the user.
        /// </summary>
        public AddressDto Address { get; set; }

        /// <summary>
        /// Gets or sets the role identifier for the user.
        /// </summary>
        public int? RoleId { get; set; }

        /// <summary>
        /// Gets or sets the status identifier for the user.
        /// </summary>
        public int? StatusId { get; set; }

        /// <summary>
        /// Gets or sets the MFA token for the user.
        /// </summary>
        public string? MfAtoken { get; set; }

        /// <summary>
        /// Gets or sets the JWT for the user.
        /// </summary>
        public string? JWT { get; set; }
    }
}
