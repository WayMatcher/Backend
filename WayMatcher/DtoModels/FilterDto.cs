using WayMatcherBL.LogicModels;

namespace WayMatcherBL.DtoModels
{
    public class FilterDto
    {
        public ScheduleDto? StartTime { get; set; }
        public AddressDto? StopLocation { get; set; }
        public AddressDto? DestinationLocation { get; set; }
    }
}
