namespace WayMatcherBL.DtoModels
{
    public class ChatMessageDto
    {
        public int ChatMessageId { get; set; }

        public string Message { get; set; }

        public DateTime Timestamp { get; set; }

        public int UserId { get; set; }

        public int EventId { get; set; }
    }
}
