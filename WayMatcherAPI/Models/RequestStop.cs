using WayMatcherBL.DtoModels;
using WayMatcherBL.LogicModels;

namespace WayMatcherAPI.Models
{
    public class RequestStop
    {
        public int StopId { get; set; }

        public int StopSequenceNumber { get; set; }

        public AddressDto Address { get; set; }
        public int EventId { get; set; }
    }
}
