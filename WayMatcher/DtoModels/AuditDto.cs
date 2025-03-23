namespace WayMatcherBL.DtoModels
{
    public class AuditDto
    {
        public int AuditId { get; set; }

        public string Message { get; set; }

        public string EntityType { get; set; }

        public DateTime? Timestamp { get; set; }

        public int? EntityId { get; set; }

        public int? UserId { get; set; }
    }
}
