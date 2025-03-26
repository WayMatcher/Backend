namespace WayMatcherBL.LogicModels
{
    /// <summary>
    /// Data Transfer Object (DTO) for schedules.
    /// </summary>
    public class ScheduleDto
    {
        /// <summary>
        /// Gets or sets the unique identifier for the schedule.
        /// </summary>
        public int ScheduleId { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier for the user associated with the schedule.
        /// </summary>
        public int? UserId { get; set; }

        /// <summary>
        /// Gets or sets the cron expression for the schedule.
        /// </summary>
        public string CronSchedule { get; set; }
    }
}
