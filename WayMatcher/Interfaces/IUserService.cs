using WayMatcherBL.LogicModels;

namespace WayMatcherBL.Interfaces
{
    public interface IUserService
    {
        public bool RegisterUser(UserDto user); //sends email
        public bool LoginUser(UserDto user); //sends email
        public string AcceptMfA(UserDto user);
        public bool DeleteUser(UserDto user);
        public bool ConfigurateUser(UserDto user);
        public void SendChangePasswordMail(UserDto user); 
        public bool ChangePassword(UserDto user); //gets called with email link
        public bool ConfigurateVehicle(UserDto user, VehicleDto vehicle);
        public bool ConfigurateAddress(UserDto user, AddressDto address);
        public UserDto GetUser(UserDto user);
        public List<UserDto> GetActiveUsers();

    }
}
