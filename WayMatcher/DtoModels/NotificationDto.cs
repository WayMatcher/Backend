namespace WayMatcherBL.DtoModels
{
    /// <summary>
    /// Data Transfer Object (DTO) for notifications.
    /// </summary>
    public class NotificationDto
    {
        /// <summary>
        /// Gets or sets the unique identifier for the notification.
        /// </summary>
        public int NotificationId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the notification has been read.
        /// </summary>
        public bool Read { get; set; }

        /// <summary>
        /// Gets or sets the message of the notification.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets the type of the entity associated with the notification.
        /// </summary>
        public string EntityType { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier of the entity associated with the notification.
        /// </summary>
        public int? EntityId { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier of the user associated with the notification.
        /// </summary>
        public int UserId { get; set; }
    }
}
