using WayMatcherBL.DtoModels;
using WayMatcherBL.Interfaces;
using WayMatcherBL.LogicModels;
using WayMatcherBL.Models;

namespace WayMatcherBL.Services
{
    public class UserService : IUserService
    {
        IDatabaseService _databaseService;
        IEmailService _emailService;
        public UserService(IDatabaseService databaseService, IEmailService emailService)
        {
            _databaseService = databaseService;
            _emailService = emailService;
        }
        public void SendChangePasswordMail(UserDto user)
        {
            //send change password email -> get new Password from user -> update user
            var email = new EmailDto()
            {
                To = user.EMail,
                Subject = "Change Password",
                Body = "Please click the link(https://deimama) to change your password",
                IsHtml = true
            };

            _emailService.SendEmail(email);
        }
        public bool ChangePassword(UserDto user)
        {
            user = _databaseService.GetUserById(user.UserId);

            if (user.Password == null)
                return false;

            return _databaseService.UpdateUser(user);
        }

        public bool ConfigurateAddress(UserDto user, AddressDto address)
        {
            if (user == null || address == null)
                return false;

            address.AddressId = _databaseService.GetAddressId(address);

            if (address.AddressId == -1)
            {
                _databaseService.InsertAddress(address);
                address.AddressId = _databaseService.GetAddressId(address);
            }

            user.AddressId = address.AddressId;
            return _databaseService.UpdateUser(user);
        }

        public bool ConfigurateUser(UserDto user)
        {
            user = _databaseService.GetUserById(user.UserId);

            if (user == null)
                return false;

            return _databaseService.UpdateUser(user);
        }

        public bool ConfigurateVehicle(UserDto user, VehicleDto vehicle)
        {
            if (user == null || vehicle == null)
                return false;

            vehicle.VehicleId = _databaseService.GetVehicleId(vehicle);

            if (vehicle.VehicleId == -1)
            {
                _databaseService.InsertVehicle(vehicle);
                vehicle.VehicleId = _databaseService.GetVehicleId(vehicle);
            }

            foreach (var v in _databaseService.GetUserVehicles(user.UserId)) //list of users vehicles GETUserVehicles
            {
                //check if user already has this vehicle
                if (v.VehicleId != vehicle.VehicleId)
                {
                    _databaseService.InsertVehicleMapping(); //insert new vehicle into the user
                }
            }

            return false;
        }

        public bool DeleteUser(UserDto user)
        {
            throw new NotImplementedException();
        }

        public List<UserDto> GetActiveUsers()
        {
            throw new NotImplementedException();
        }

        public UserDto GetUser(int id)
        {
            throw new NotImplementedException();
        }

        public bool LoginUser(UserDto user)
        {
            throw new NotImplementedException();
        }

        public bool RegisterUser(UserDto user)
        {
            throw new NotImplementedException();
        }


    }
}
