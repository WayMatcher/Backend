namespace WayMatcherBL.DtoModels
{
    public class NotificationDto
    {
        public int NotificationId { get; set; }

        public bool Read { get; set; }

        public string Message { get; set; }

        public string EntityType { get; set; }

        public int? EntityId { get; set; }

        public int UserId { get; set; }
    }
}
