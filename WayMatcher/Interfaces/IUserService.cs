using WayMatcherBL.DtoModels;
using WayMatcherBL.Enums;
using WayMatcherBL.LogicModels;

namespace WayMatcherBL.Interfaces
{
    public interface IUserService
    {
        public RESTCode RegisterUser(UserDto user, VehicleDto vehicle);
        public UserDto LoginUser(UserDto user);
        public UserDto AcceptMfA(UserDto user);
        public bool DeleteUser(UserDto user);
        public bool ConfigurateUser(UserDto user);
        public void SendChangePasswordMail(UserDto user);
        public bool ChangePassword(UserDto user);
        public bool ConfigurateVehicle(UserDto user, VehicleDto vehicle);
        public bool ConfigurateAddress(UserDto user);
        public UserDto GetUser(UserDto user);
        public AddressDto GetAddress(UserDto user);
        public List<VehicleDto> GetUserVehicleList(UserDto user);
        public List<UserDto> GetActiveUsers();
        public bool RateUser(RatingDto rate);
        public double UserRating(RatingDto rate);
    }
}
