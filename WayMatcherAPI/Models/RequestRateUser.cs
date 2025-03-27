namespace WayMatcherAPI.Models
{
    /// <summary>
    /// Represents a request model for rating a user.
    /// </summary>
    public class RequestRateUser
    {
        /// <summary>
        /// Gets or sets the unique identifier for the rating.
        /// </summary>
        public int? RatingId { get; set; }

        /// <summary>
        /// Gets or sets the text of the rating.
        /// </summary>
        public string? RatingText { get; set; }

        /// <summary>
        /// Gets or sets the value of the rating.
        /// </summary>
        public int RatingValue { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier of the user who is being rated.
        /// </summary>
        public int RatedUserId { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier of the user who provided the rating.
        /// </summary>
        public int UserWhoRatedId { get; set; }

        /// <summary>
        /// Gets or sets the status identifier for the rating.
        /// </summary>
        public int? StatusId { get; set; }
    }
}
