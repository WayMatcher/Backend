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
        private string GenerateAndHashRandomNumber(string userMail)
        {
            Random random = new Random();
            int randomNumber = random.Next(1000, 10000); //4 digit number
            byte[] numberBytes = Encoding.UTF8.GetBytes(randomNumber.ToString());

            EmailDto email = new EmailDto()
            {
                Subject = "WayMatcher | MFA Code for User: " + GetUser(userMail).Username,
                Body = "<html>\r\n  <head>\r\n    <meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\" />\r\n    <style>\r\n      /* Add custom classes and styles that you want inlined here */\r\n    </style>\r\n  </head>\r\n  <body class=\"bg-light\">\r\n    <div class=\"container\">\r\n      <div class=\"card my-10\">\r\n        <div class=\"card-body\">\r\n          <h1 class=\"h3 mb-2\">Multi-Factor Authentication (MFA) Code</h1>\r\n          <h5 class=\"text-teal-700\">Your security code is below</h5>\r\n          <hr>\r\n          <div class=\"space-y-3\">\r\n            <p class=\"text-gray-700\">Use the following code to complete your sign-in process:</p>\r\n            <div class=\"text-center p-3 bg-gray-200 rounded text-xl font-bold\">" + randomNumber + " </div>\r\n            <p class=\"text-gray-700\">This code will expire in 10 minutes. Do not share this code with anyone.</p>\r\n            <p class=\"text-gray-700\">If you did not request this code, please ignore this email.</p>\r\n          </div>\r\n          <hr>\r\n          <p class=\"text-gray-700\">Need help? <a href=\"https://support.example.com\" target=\"_blank\">Contact Support</a></p>\r\n        </div>\r\n      </div>\r\n    </div>\r\n  </body>\r\n</html>",
                To = userMail,
                IsHtml = true
            };

            _emailService.SendEmail(email); //with the 4digit number for the user

            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashBytes = sha256.ComputeHash(numberBytes);
                return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
            }
        }
        public void SendChangePasswordMail(UserDto user)
        {
            //send change password email -> get new Password from user -> update user
            var email = new EmailDto()
            {
                Subject = "Change Password",
                Body = "Please click the link(https://deimama) to change your password", //with user information / id / token
                To = user.EMail,
                IsHtml = true
            };

            _emailService.SendEmail(email);
        }
        public bool ChangePassword(UserDto user)
        {
            user = _databaseService.GetUser(user.UserId);

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
            user = _databaseService.GetUser(user.UserId);

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
            return _databaseService.GetUser(id);
        }
        public UserDto GetUser(string email)
        {
            return _databaseService.GetUser(_databaseService.GetUserId(email));
        }

        public bool LoginUser(UserDto user)
        {
            if (user == null)
                return false;

            var dbUser = _databaseService.GetUser(user.UserId);

            if (dbUser == null)
                return false;

            if (dbUser.Password == user.Password)
            {
                var hashedMfA = GenerateAndHashRandomNumber(dbUser.EMail);

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
