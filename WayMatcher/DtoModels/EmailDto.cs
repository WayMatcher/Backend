namespace WayMatcherBL.DtoModels
{
    /// <summary>
    /// Data Transfer Object (DTO) for email messages.
    /// </summary>
    public class EmailDto
    {
        /// <summary>
        /// Gets or sets the username of the sender.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets the recipient email address.
        /// </summary>
        public string To { get; set; }

        /// <summary>
        /// Gets or sets the subject of the email.
        /// </summary>
        public string Subject { get; set; }

        /// <summary>
        /// Gets or sets the body of the email.
        /// </summary>
        public string Body { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the email body is in HTML format.
        /// </summary>
        public bool IsHtml { get; set; }
    }
}
