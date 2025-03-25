﻿using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using WayMatcherBL.DtoModels;
using WayMatcherBL.Enums;
using WayMatcherBL.Interfaces;
using WayMatcherBL.LogicModels;

namespace WayMatcherBL.Services
{
    /// <summary>
    /// Service class for managing user-related operations.
    /// </summary>
    public class UserService : IUserService
    {
        private readonly IDatabaseService _databaseService;
        private readonly IEmailService _emailService;
        private readonly ConfigurationService _configuration;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserService"/> class.
        /// </summary>
        /// <param name="databaseService">The database service.</param>
        /// <param name="emailService">The email service.</param>
        /// <param name="configuration">The configuration service.</param>
        public UserService(IDatabaseService databaseService, IEmailService emailService, ConfigurationService configuration)
        {
            _databaseService = databaseService;
            _emailService = emailService;
            _configuration = configuration;
        }

        /// <summary>
        /// Generates a Multi-Factor Authentication (MFA) code for the user and sends it via email.
        /// </summary>
        /// <param name="user">The user DTO.</param>
        /// <returns>The hashed MFA code.</returns>
        private string GenerateMfA(UserDto user)
        {
            Random random = new Random();
            int randomNumber = random.Next(1000, 10000); // 4 digit number
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

        /// <summary>
        /// Generates a JSON Web Token (JWT) for the user.
        /// </summary>
        /// <param name="user">The user DTO.</param>
        /// <returns>The generated JWT.</returns>
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

        /// <summary>
        /// Gets the vehicle ID for the specified vehicle.
        /// </summary>
        /// <param name="vehicle">The vehicle DTO.</param>
        /// <returns>The vehicle ID.</returns>
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

        /// <summary>
        /// Gets the address ID for the specified address.
        /// </summary>
        /// <param name="address">The address DTO.</param>
        /// <returns>The address ID.</returns>
        private int GetAddressId(AddressDto address)
        {
            address.AddressId = _databaseService.GetAddress(address).AddressId;

            if (address.AddressId == -1)
            {
                _databaseService.InsertAddress(address);
                return _databaseService.GetAddress(address).AddressId;
            }

            return address.AddressId;
        }

        /// <summary>
        /// Sends an email to the user to change their password.
        /// </summary>
        /// <param name="user">The user DTO.</param>
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

        /// <summary>
        /// Changes the password for the specified user.
        /// </summary>
        /// <param name="user">The user DTO.</param>
        /// <returns>True if the password was changed successfully; otherwise, false.</returns>
        public bool ChangePassword(UserDto user)
        {
            user = _databaseService.GetUser(user);

            if (user.Password == null)
                return false;

            return _databaseService.UpdateUser(user);
        }

        /// <summary>
        /// Configures the address for the specified user.
        /// </summary>
        /// <param name="user">The user DTO.</param>
        /// <returns>True if the address was configured successfully; otherwise, false.</returns>
        public bool ConfigurateAddress(UserDto user)
        {
            if (user == null || user.Address == null)
                return false;

            user.Address.AddressId = GetAddressId(user.Address);

            return _databaseService.UpdateUser(user);
        }

        /// <summary>
        /// Configures the specified user.
        /// </summary>
        /// <param name="user">The user DTO.</param>
        /// <returns>True if the user was configured successfully; otherwise, false.</returns>
        public bool ConfigurateUser(UserDto user)
        {
            if (user == null)
                return false;

            return _databaseService.UpdateUser(user);
        }

        /// <summary>
        /// Configures vehicles and vehicle mappings for the specified user.
        /// </summary>
        /// <param name="user">The user DTO.</param>
        /// <param name="vehicleList">The list of vehicles to configure.</param>
        /// <param name="vehicleMappingList">The list of vehicle mappings to configure.</param>
        /// <returns>True if the vehicles and vehicle mappings were successfully configured; otherwise, false.</returns>
        public bool ConfigurateVehicle(UserDto user, List<VehicleDto> vehicleList, List<VehicleMappingDto> vehicleMappingList)
        {
            if (user == null || vehicleList == null)
                throw new ArgumentNullException("User or vehicle cannot be null");

            foreach (var vehicle in vehicleList)
            {
                vehicle.VehicleId = GetVehicleId(vehicle);

                var dbUser = GetUser(user);

                var vehicleListDb = _databaseService.GetUserVehicles(dbUser);
                for (int i = 0; i < vehicleListDb.Count; i++)
                {
                    if (vehicleListDb[i].VehicleId != vehicle.VehicleId)
                    {
                        var vehicleMapping = new VehicleMappingDto()
                        {
                            UserId = dbUser.UserId,
                            VehicleId = vehicle.VehicleId,
                            FuelMilage = vehicleMappingList[i].FuelMilage,
                            AdditionalInfo = vehicleMappingList[i].AdditionalInfo,
                            LicensePlate = vehicleMappingList[i].LicensePlate
                        };
                        _databaseService.InsertVehicleMapping(vehicleMapping);
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// Deletes the specified user.
        /// </summary>
        /// <param name="user">The user DTO.</param>
        /// <returns>True if the user was deleted successfully; otherwise, false.</returns>
        public bool DeleteUser(UserDto user)
        {
            user.StatusId = (int)State.Inactive;
            return _databaseService.UpdateUser(user);
        }

        /// <summary>
        /// Gets a list of active users.
        /// </summary>
        /// <returns>A list of active users.</returns>
        public List<UserDto> GetActiveUsers()
        {
            return _databaseService.GetActiveUsers();
        }

        /// <summary>
        /// Gets the specified user.
        /// </summary>
        /// <param name="user">The user DTO.</param>
        /// <returns>The user DTO.</returns>
        public UserDto GetUser(UserDto user)
        {
            return _databaseService.GetUser(user);
        }
        /// <summary>
        /// Gets the address for the specified user.
        /// </summary>
        /// <param name="user">The user DTO.</param>
        /// <returns>The address DTO.</returns>
        public AddressDto GetAddress(UserDto user)
        {
            return _databaseService.GetAddress(user);
        }

        /// <summary>
        /// Gets a list of vehicles for the specified user.
        /// </summary>
        /// <param name="user">The user DTO.</param>
        /// <returns>A list of vehicle DTOs.</returns>
        public List<VehicleDto> GetUserVehicleList(UserDto user)
        {
            return _databaseService.GetUserVehicles(user);
        }

        /// <summary>
        /// Logs in the specified user.
        /// </summary>
        /// <param name="user">The user DTO.</param>
        /// <returns>The logged-in user DTO.</returns>
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

        /// <summary>
        /// Accepts the MFA code for the specified user.
        /// </summary>
        /// <param name="user">The user DTO.</param>
        /// <returns>The user DTO with the generated JWT.</returns>
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

        /// <summary>
        /// Registers a new user and their vehicles and vehicle mappings.
        /// </summary>
        /// <param name="user">The user DTO.</param>
        /// <param name="vehicleList">A list of vehicle DTOs.</param>
        /// <param name="vehicleMappingList">A list of vehicle mappings associated with the user.</param>
        /// <returns>True if the user was successfully registered; otherwise, false.</returns>
        public bool RegisterUser(UserDto user, List<VehicleDto> vehicleList, List<VehicleMappingDto> vehicleMappingList)
        {
            user.Address.AddressId = GetAddressId(user.Address);

            if (_databaseService.InsertUser(user))
            {
                var userId = GetUser(user).UserId;

                for (int i = 0; i < vehicleList.Count; i++)
                {
                    var vehicleMapping = new VehicleMappingDto()
                    {
                        UserId = userId,
                        VehicleId = vehicleList[i].VehicleId,
                        FuelMilage = vehicleMappingList[i].FuelMilage,
                        AdditionalInfo = vehicleMappingList[i].AdditionalInfo,
                        LicensePlate = vehicleMappingList[i].LicensePlate
                    };
                    _databaseService.InsertVehicleMapping(vehicleMapping);
                }

                return true;
            }
            throw new ArgumentNullException("User cannot be null");
        }

        /// <summary>
        /// Rates the specified user.
        /// </summary>
        /// <param name="rate">The rating DTO.</param>
        /// <returns>True if the rating was successful; otherwise, false.</returns>
        public bool RateUser(RatingDto rate)
        {
            if (rate == null)
                throw new ArgumentNullException("Rating cannot be null");

            if (_databaseService.GetRating(rate).RatingId == rate.RatingId)
                return _databaseService.UpdateRating(rate);
            else
                return _databaseService.InsertRating(rate);
        }

        /// <summary>
        /// Gets the average rating for the specified user.
        /// </summary>
        /// <param name="rate">The rating DTO.</param>
        /// <returns>The average rating value.</returns>
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

        /// <summary>
        /// Sends a notification to the specified user.
        /// </summary>
        /// <param name="notification">The notification DTO.</param>
        /// <returns>True if the notification was sent successfully; otherwise, false.</returns>
        public bool SendNotification(NotificationDto notification)
        {
            if (notification == null)
                throw new ArgumentNullException("Notification cannot be null");

            return _databaseService.InsertNotification(notification);
        }

        /// <summary>
        /// Gets a list of notifications for the specified user.
        /// </summary>
        /// <param name="user">The user DTO.</param>
        /// <returns>A list of notification DTOs.</returns>
        public List<NotificationDto> GetNotification(UserDto user)
        {
            if (user == null)
                throw new ArgumentNullException("User cannot be null");

            return _databaseService.GetNotificationList(user);
        }
    }
}
