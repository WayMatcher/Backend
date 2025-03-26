namespace WayMatcherBL.DtoModels
{
    /// <summary>
    /// Data Transfer Object (DTO) for chat messages.
    /// </summary>
    public class ChatMessageDto
    {
        /// <summary>
        /// Gets or sets the unique identifier for the chat message.
        /// </summary>
        public int ChatMessageId { get; set; }

        /// <summary>
        /// Gets or sets the content of the chat message.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets the timestamp of when the chat message was sent.
        /// </summary>
        public DateTime Timestamp { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier of the user who sent the chat message.
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier of the event associated with the chat message.
        /// </summary>
        public int? EventId { get; set; }
    }
}
