namespace WayMatcherAPI.Models
{
    /// <summary>
    /// Represents a request model for user login information.
    /// </summary>
    public class RequestUserLoginModel
    {
        /// <summary>
        /// Gets or sets the username of the user.
        /// </summary>
        public string? Username { get; set; }

        /// <summary>
        /// Gets or sets the email of the user.
        /// </summary>
        public string? Email { get; set; }

        /// <summary>
        /// Gets or sets the password of the user.
        /// </summary>
        public string Password { get; set; }
    }
}
