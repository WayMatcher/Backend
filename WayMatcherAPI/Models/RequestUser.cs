namespace WayMatcherAPI.Models
{
    /// <summary>
    /// Represents a request model for user information.
    /// </summary>
    public class RequestUser
    {
        /// <summary>
        /// Gets or sets the unique identifier for the user.
        /// </summary>
        public int? UserId { get; set; }

        /// <summary>
        /// Gets or sets the username of the user.
        /// </summary>
        public string? Username { get; set; }

        /// <summary>
        /// Gets or sets the email of the user.
        /// </summary>
        public string? Email { get; set; }
    }
}
