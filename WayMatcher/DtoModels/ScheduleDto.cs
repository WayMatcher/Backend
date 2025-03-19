namespace WayMatcherBL.LogicModels
{
    public class ScheduleDto
    {
        public int ScheduleId { get; set; }
        public int? UserId { get; set; }

        public string CronSchedule { get; set; }
    }
}
