using WayMatcherBL.Enums;
using WayMatcherBL.LogicModels;

namespace WayMatcherBL.Interfaces
{
    public interface IUserService
    {
        public bool RegisterUser(UserDto user, VehicleDto vehicle); //sends email #TODO
        public RESTCode LoginUser(UserDto user); //sends email #TODO
        public string AcceptMfA(UserDto user);
        public bool DeleteUser(UserDto user);
        public bool ConfigurateUser(UserDto user);
        public void SendChangePasswordMail(UserDto user); 
        public bool ChangePassword(UserDto user); 
        public bool ConfigurateVehicle(UserDto user, VehicleDto vehicle);
        public bool ConfigurateAddress(UserDto user);
        public UserDto GetUser(UserDto user);
        public List<UserDto> GetActiveUsers();
        //public bool RateUser(); #TODO
    }
}
