using WayMatcherBL.LogicModels;

namespace WayMatcherBL.DtoModels
{
    public class StopDto
    {
        public int StopId { get; set; }

        public int StopSequenceNumber { get; set; }

        public AddressDto Address { get; set; }
        public int EventId { get; set; }
    }
}
