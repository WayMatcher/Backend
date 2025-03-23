namespace WayMatcherBL.LogicModels
{
    public class EventDto
    {
        public int EventId { get; set; }

        public int? EventTypeId { get; set; }

        public int? FreeSeats { get; set; }

        public string? Description { get; set; }

        public DateTime? StartTimestamp { get; set; }

        public int? ScheduleId { get; set; }

        public int? StatusId { get; set; }
    }
}
