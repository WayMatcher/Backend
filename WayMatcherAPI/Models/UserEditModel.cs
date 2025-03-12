using WayMatcherBL.LogicModels;

namespace WayMatcherAPI.Models
{
    public class UserEditModel
    {
        public UserDto user { get; set; }
        public AddressDto address { get; set; }
        public VehicleDto vehicle { get; set; }
    }
}
