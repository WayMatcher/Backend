using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Resources;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using WayMatcherBL.DtoModels;
using WayMatcherBL.Enums;
using WayMatcherBL.Interfaces;
using WayMatcherBL.LogicModels;
using WayMatcherBL.Models;

namespace WayMatcherBL.Services
{
    public class UserService : IUserService
    {
        IDatabaseService _databaseService;
        IEmailService _emailService;
        ConfigurationService _configuration;
        public UserService(IDatabaseService databaseService, IEmailService emailService, ConfigurationService configuration)
        {
            _databaseService = databaseService;
            _emailService = emailService;
            _configuration = configuration;
        }
        private string GenerateMfA(UserDto user)
        {
            Random random = new Random();
            int randomNumber = random.Next(1000, 10000); //4 digit number
            byte[] numberBytes = Encoding.UTF8.GetBytes(randomNumber.ToString());

            EmailDto email = new EmailDto()
            {
                Subject = "WayMatcher | MFA Code for User: " + GetUser(user),
                Body = "<html>\r\n  <head>\r\n    <meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\" />\r\n    <style>\r\n      /* Add custom classes and styles that you want inlined here */\r\n    </style>\r\n  </head>\r\n  <body class=\"bg-light\">\r\n    <div class=\"container\">\r\n      <div class=\"card my-10\">\r\n        <div class=\"card-body\">\r\n          <h1 class=\"h3 mb-2\">Multi-Factor Authentication (MFA) Code</h1>\r\n          <h5 class=\"text-teal-700\">Your security code is below</h5>\r\n          <hr>\r\n          <div class=\"space-y-3\">\r\n            <p class=\"text-gray-700\">Use the following code to complete your sign-in process:</p>\r\n            <div class=\"text-center p-3 bg-gray-200 rounded text-xl font-bold\">" + randomNumber + " </div>\r\n            <p class=\"text-gray-700\">This code will expire in 10 minutes. Do not share this code with anyone.</p>\r\n            <p class=\"text-gray-700\">If you did not request this code, please ignore this email.</p>\r\n          </div>\r\n          <hr>\r\n          <p class=\"text-gray-700\">Need help? <a href=\"https://support.example.com\" target=\"_blank\">Contact Support</a></p>\r\n        </div>\r\n      </div>\r\n    </div>\r\n  </body>\r\n</html>",
                To = user.Email,
                IsHtml = true
            };
            _emailService.SendEmail(email);

            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashBytes = sha256.ComputeHash(numberBytes);
                return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
            }
        }
        private string GenerateJWT(UserDto user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSecretKey()));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Username),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(
                issuer: "yourdomain.com",
                audience: "yourdomain.com",
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private int GetVehicleId(VehicleDto vehicle)
        {
            vehicle.VehicleId = _databaseService.GetVehicleId(vehicle);

            if (vehicle.VehicleId == -1)
            {
                _databaseService.InsertVehicle(vehicle);
                vehicle.VehicleId = _databaseService.GetVehicleId(vehicle);
            }

            return vehicle.VehicleId;
        }

        private int GetAddressId(AddressDto address)
        {
            address.AddressId = _databaseService.GetAddressId(address);

            if (address.AddressId == -1)
            {
                _databaseService.InsertAddress(address);
                return _databaseService.GetAddressId(address);
            }

            return address.AddressId;
        }

        public void SendChangePasswordMail(UserDto user)
        {
            var email = new EmailDto()
            {
                Subject = "Change Password",
                Body = "<html>\r\n  <head>\r\n    <meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\" />\r\n    <style>\r\n      body {\r\n        font-family: Arial, sans-serif;\r\n        background-color: #f4f4f4;\r\n        margin: 0;\r\n        padding: 0;\r\n      }\r\n      .container {\r\n        max-width: 600px;\r\n        margin: 20px auto;\r\n        background: #ffffff;\r\n        padding: 20px;\r\n        border-radius: 8px;\r\n        box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);\r\n      }\r\n      .header {\r\n        text-align: center;\r\n        font-size: 24px;\r\n        font-weight: bold;\r\n        color: #333;\r\n      }\r\n      .content {\r\n        font-size: 16px;\r\n        color: #555;\r\n        line-height: 1.6;\r\n      }\r\n      .button {\r\n        display: inline-block;\r\n        padding: 12px 20px;\r\n        margin-top: 20px;\r\n        background: #007bff;\r\n        color: #ffffff;\r\n        text-decoration: none;\r\n        border-radius: 5px;\r\n        font-weight: bold;\r\n      }\r\n      .footer {\r\n        margin-top: 20px;\r\n        font-size: 14px;\r\n        color: #888;\r\n        text-align: center;\r\n      }\r\n    </style>\r\n  </head>\r\n  <body>\r\n    <div class=\"container\">\r\n      <div class=\"header\">Reset Your Password</div>\r\n      <hr>\r\n      <div class=\"content\">\r\n        <p>Hello,</p>\r\n        <p>We received a request to reset your password. Click the button below to proceed:</p>\r\n        <p style=\"text-align: center;\">\r\n          <a href=\"{{ RESET_LINK with user id or something to identify again }}\" class=\"button\">Reset Password</a>\r\n        </p>\r\n        <br>\r\n        <p>If you did not request this, please ignore this email.</p>\r\n      </div>\r\n      <hr>\r\n    </div>\r\n  </body>\r\n</html>\r\n", //send https with hashedUser/jwttoken? and create template for email #TODO
                To = user.Email,
                IsHtml = true
            };
            _emailService.SendEmail(email);
        }
        public bool ChangePassword(UserDto user)
        {
            user = _databaseService.GetUser(user);

            if (user.Password == null)
                return false;

            return _databaseService.UpdateUser(user);
        }

        public bool ConfigurateAddress(UserDto user)
        {
            if (user == null || user.Address == null)
                return false;

            user.Address.AddressId = GetAddressId(user.Address);

            return _databaseService.UpdateUser(user);
        }

        public bool ConfigurateUser(UserDto user)
        {
            if (user == null)
                return false;

            return _databaseService.UpdateUser(user);
        }

        public bool ConfigurateVehicle(UserDto user, VehicleDto vehicle)
        {
            if (user == null || vehicle == null)
                return false;

            vehicle.VehicleId = GetVehicleId(vehicle);

            var userId = GetUser(user).UserId;

            foreach (var v in _databaseService.GetUserVehicles(userId))
            {
                if (v.VehicleId != vehicle.VehicleId)
                {
                    VehicleMappingDto vehicleMapping = new VehicleMappingDto()
                    {
                        UserId = userId,
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

        public UserDto GetUser(UserDto user)
        {
            return _databaseService.GetUser(user);
        }

        public UserDto LoginUser(UserDto user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user), "User cannot be null");

            var dbUser = _databaseService.GetUser(user);

            if (dbUser == null)
                throw new ArgumentNullException(nameof(dbUser), "Database user cannot be null");

            if ((dbUser.Username == user.Username || dbUser.Email == user.Email) && dbUser.Password == user.Password)
            {
                dbUser.MfAtoken = GenerateMfA(dbUser);
                _databaseService.UpdateUser(dbUser);

                return dbUser;
            }
            return null;
        }
        public UserDto AcceptMfA(UserDto user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user), "User cannot be null");

            var dbUser = _databaseService.GetUser(user);

            if (dbUser == null)
                throw new ArgumentNullException(nameof(dbUser), "Database user cannot be null");

            if (dbUser.MfAtoken == user.MfAtoken)
            {
                dbUser.MfAtoken = null;
                dbUser.JWT = GenerateJWT(dbUser);
                _databaseService.UpdateUser(dbUser);

                return dbUser;
            }

            return null;
        }

        public RESTCode RegisterUser(UserDto user, VehicleDto vehicle)
        {
            user.Address.AddressId = GetAddressId(user.Address);

            if (_databaseService.InsertUser(user))
            {
                var userId = GetUser(user).UserId;
                VehicleMappingDto vehicleMapping = new VehicleMappingDto()
                {
                    UserId = userId,
                    VehicleId = GetVehicleId(vehicle)
                };
                _databaseService.InsertVehicleMapping(vehicleMapping);
                return RESTCode.Success;
            }

            return RESTCode.InternalServerError;
        }

        public bool RateUser(RatingDto rate)
        {
            if (rate == null)
                throw new ArgumentNullException("Rating cannot be null");

            if (_databaseService.GetRating(rate).RatingId == rate.RatingId)
                return _databaseService.UpdateRating(rate);
            else
                return _databaseService.InsertRating(rate);
        }
        public double UserRating(RatingDto rate)
        {
            if (rate == null)
                throw new ArgumentNullException("Rating cannot be null");

            UserDto user = new UserDto()
            {
                UserId = rate.RatedUserId
            };

            var ratings = _databaseService.GetRatingList(user);

            if (ratings == null || ratings.Count == 0)
                throw new ArgumentNullException("No ratings found for user");

            return ratings.Average(r => r.RatingValue);
        }
    }
}
