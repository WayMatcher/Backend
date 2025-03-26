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
                Body = $@"<html><head><meta http-equiv=""Content-Type"" content=""text/html; charset=utf-8"" /></head><body class=""bg-light""><div class=""container""><div class=""card my-10""><div class=""card-body""><h1 class=""h3 mb-2"">Multi-Factor Authentication (MFA) Verification</h1><h5 class=""text-teal-700"">Secure your account with an extra layer of protection</h5><hr><div class=""space-y-3""><p class=""text-gray-700"">Hello,</p><p class=""text-gray-700"">To complete your login process, please enter the verification code below:</p>
<h2 class=""text-teal-700"">{randomNumber}</h2>
<p class=""text-gray-700"">If you did not request this code, please disregard this email or contact our support team immediately.</p><p class=""text-gray-700"">For added security, this code will expire in 10 minutes.</p></div><hr><p class=""text-gray-700"">Thank you for helping us keep your account secure.</p><a class=""btn btn-primary"" href=""https://support.yourcompany.com"" target=""_blank"">Contact Support</a></div></div></div></body></html>",
                To = user.Email,
                IsHtml = true
            };
            _emailService.SendEmail(email);

            return HashString(randomNumber.ToString());
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
        /// Hashes the input string using SHA-256.
        /// </summary>
        /// <param name="input">The input string to hash.</param>
        /// <returns>The hashed string.</returns>
        private string HashString(string input)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(input);
                byte[] hashBytes = sha256.ComputeHash(inputBytes);
                return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
            }
        }

        /// <summary>
        /// Sends an email to the user to change their password.
        /// </summary>
        /// <param name="user">The user DTO.</param>
        public void SendChangePasswordMail(UserDto user)
        {
            var email = new EmailDto()
            {
                Subject = $"Change Password for {user.Username}",
                Body = $@"<html>
<head><meta http-equiv=""Content-Type"" content=""text/html; charset=utf-8"" /></head><body class=""bg-light""><div class=""container""><div class=""card my-10""><div class=""card-body""><h1 class=""h3 mb-2"">Password Reset Request</h1><h5 class=""text-teal-700"">We've received a request to reset your password</h5><hr><div class=""space-y-3""><p class=""text-gray-700"">Hello,</p><p class=""text-gray-700"">We received a request to reset your account password. If this was you, please click the link below to change your password.</p><p class=""text-gray-700"">If you did not request a password reset, you can safely ignore this email.</p></div><hr>
<a class=""btn btn-primary"" href=""{HashString(user.Username)}"" target=""_blank"">Reset Your Password</a>
</div></div></div></body></html>",
                To = user.Email,
                IsHtml = true
            };
            _emailService.SendEmail(email);
        }

        /// <summary>
        /// Changes the password for the specified user.
        /// </summary>
        /// <param name="user">The user DTO.</param>
        /// <returns>True if the password was changed successfully; otherwise, <c>false</c>.</returns>
        public bool ChangePassword(UserDto user)
        {
            if (user.Password == null)
                throw new ArgumentNullException("Password cannot be null");

            var userDto = _databaseService.GetActiveUsers().FirstOrDefault(u => HashString(u.Username) == user.Username);

            if (userDto == null)
                throw new ArgumentNullException("User cannot be null");

            userDto.Password = user.Password;
            return _databaseService.UpdateUser(userDto);
        }

        /// <summary>
        /// Configures the specified user.
        /// </summary>
        /// <param name="user">The user DTO.</param>
        /// <returns>True if the user was configured successfully; otherwise, false.</returns>
        public bool ConfigurateUser(UserDto user)
        {
            if (user == null)
                throw new ArgumentNullException("User cannot be null");

            user.Address.AddressId = GetAddressId(user.Address);

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
            if (user == null)
                throw new ArgumentNullException("User cannot be null");

            user.Address.AddressId = GetAddressId(user.Address);
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

            if (dbUser == null || dbUser.StatusId == (int)State.Inactive)
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

            var existingUser = _databaseService.GetUser(user);

            if (existingUser != null)
            {
                if (existingUser.StatusId == (int)State.Inactive)
                {
                    existingUser.StatusId = (int)State.Active;
                    _databaseService.UpdateUser(existingUser);
                    return true;
                }
                else
                    throw new Exception("User already exists and is active");
            }

            else if (_databaseService.InsertUser(user))
            {
                var userId = GetUser(user).UserId;

                for (int i = 0; i < vehicleList.Count; i++)
                {
                    var vehicleMapping = new VehicleMappingDto()
                    {
                        UserId = userId,
                        VehicleId = GetVehicleId(vehicleList[i]),
                        FuelMilage = vehicleMappingList[i].FuelMilage,
                        AdditionalInfo = vehicleMappingList[i].AdditionalInfo,
                        LicensePlate = vehicleMappingList[i].LicensePlate
                    };
                    _databaseService.InsertVehicleMapping(vehicleMapping);
                }

                return true;
            }
            throw new Exception("User could not be registered");
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
