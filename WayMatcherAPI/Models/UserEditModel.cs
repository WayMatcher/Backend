using WayMatcherBL.LogicModels;

namespace WayMatcherAPI.Models
{
    public class UserEditModel
    {
        public UserDto User { get; set; }
        public AddressDto Address { get; set; }
        public VehicleDto Vehicle { get; set; }
    }
}
