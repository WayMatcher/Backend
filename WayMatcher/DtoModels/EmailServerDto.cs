namespace WayMatcherBL.DtoModels
{
    /// <summary>
    /// Data Transfer Object (DTO) for email server configuration.
    /// </summary>
    public class EmailServerDto
    {
        /// <summary>
        /// Gets or sets the host address of the email server.
        /// </summary>
        public string Host { get; set; }

        /// <summary>
        /// Gets or sets the port number of the email server.
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        /// Gets or sets the username for the email server authentication.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets the password for the email server authentication.
        /// </summary>
        public string Password { get; set; }
    }
}
