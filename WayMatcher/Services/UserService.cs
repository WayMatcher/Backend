using System.Security.Cryptography;
using System.Text;
using WayMatcherBL.DtoModels;
using WayMatcherBL.Enums;
using WayMatcherBL.Interfaces;
using WayMatcherBL.LogicModels;

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
        private string GenerateAndHashRandomNumber()
        {
            Random random = new Random();
            int randomNumber = random.Next(1000, 10000); //4 digit number
            byte[] numberBytes = Encoding.UTF8.GetBytes(randomNumber.ToString());

            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashBytes = sha256.ComputeHash(numberBytes);
                return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
            }

            //_emailService.SendEmail(email); with the 4digit number for the user
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

            foreach (var v in _databaseService.GetUserVehicles(user.UserId))
            {
                //check if user already has this vehicle
                if (v.VehicleId != vehicle.VehicleId)
                {
                    VehicleMappingDto vehicleMapping = new VehicleMappingDto()
                    {
                        UserId = user.UserId,
                        VehicleId = vehicle.VehicleId
                    };

                    _databaseService.InsertVehicleMapping(vehicleMapping);
                }
            }

            return false;
        }

        public bool DeleteUser(UserDto user)
        {
            user.StatusId = (int)State.Inactive;
            return _databaseService.UpdateUser(user);
        }

        public List<UserDto> GetActiveUsers()
        {
            return _databaseService.GetActiveUsers();
        }

        public UserDto GetUser(int id)
        {
            return _databaseService.GetUserById(id);
        }

        public bool LoginUser(UserDto user)
        {
            if (user == null)
                return false;

            var dbUser = _databaseService.GetUserById(user.UserId);

            if (dbUser == null)
                return false;

            if (dbUser.Password == user.Password)
            {
                var hashedMfA = GenerateAndHashRandomNumber();

                dbUser.MfAtoken = hashedMfA;
                _databaseService.UpdateUser(dbUser);

                return true;
            }
            return false;
        }

        public bool RegisterUser(UserDto user)
        {
            return _databaseService.InsertUser(user);
        }


    }
}
