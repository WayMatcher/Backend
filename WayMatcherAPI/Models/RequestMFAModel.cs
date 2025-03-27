namespace WayMatcherAPI.Models
{
    /// <summary>
    /// Represents a request model for MFA (Multi-Factor Authentication) information.
    /// </summary>
    public class RequestMFAModel
    {
        /// <summary>
        /// Gets or sets the MFA token.
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier for the user.
        /// </summary>
        public int UserId { get; set; }
    }
}
