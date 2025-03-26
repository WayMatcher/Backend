namespace WayMatcherAPI.Models
{
    public class RequestRateUser
    {
        public int? RatingId { get; set; }

        public string? RatingText { get; set; }

        public int RatingValue { get; set; }

        public int RatedUserId { get; set; }

        public int UserWhoRatedId { get; set; }

        public int? StatusId { get; set; }
    }
}
