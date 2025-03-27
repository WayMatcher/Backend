using WayMatcherBL.DtoModels;
using WayMatcherBL.Enums;
using WayMatcherBL.LogicModels;

namespace WayMatcherBL.Interfaces
{
    /// <summary>
    /// Defines the contract for user-related operations.
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Registers a new user with the specified vehicles and vehicle mappings.
        /// </summary>
        /// <param name="user">The user to register.</param>
        /// <param name="vehicleList">The list of vehicles associated with the user.</param>
        /// <param name="vehicleMappingList">The list of vehicle mappings associated with the user.</param>
        /// <returns>True if the user was successfully registered; otherwise, false.</returns>
        public bool RegisterUser(UserDto user, List<VehicleDto> vehicleList, List<VehicleMappingDto> vehicleMappingList);

        /// <summary>
        /// Logs in a user.
        /// </summary>
        /// <param name="user">The user to log in.</param>
        /// <returns>The logged-in user DTO.</returns>
        public UserDto LoginUser(UserDto user);

        /// <summary>
        /// Accepts multi-factor authentication for a user.
        /// </summary>
        /// <param name="user">The user to authenticate.</param>
        /// <returns>The authenticated user DTO.</returns>
        public UserDto AcceptMfA(UserDto user);

        /// <summary>
        /// Deletes a user.
        /// </summary>
        /// <param name="user">The user to delete.</param>
        /// <returns>True if the user was successfully deleted; otherwise, false.</returns>
        public bool DeleteUser(UserDto user);

        /// <summary>
        /// Configures a user.
        /// </summary>
        /// <param name="user">The user to configure.</param>
        /// <returns>True if the user was successfully configured; otherwise, false.</returns>
        public bool ConfigurateUser(UserDto user);

        /// <summary>
        /// Sends a change password email to a user.
        /// </summary>
        /// <param name="user">The user to send the email to.</param>
        public void SendChangePasswordMail(UserDto user);

        /// <summary>
        /// Changes the password of a user.
        /// </summary>
        /// <param name="user">The user whose password is to be changed.</param>
        /// <returns>True if the password was successfully changed; otherwise, false.</returns>
        public bool ChangePassword(UserDto user);

        /// <summary>
        /// Configures vehicles and vehicle mappings for a user.
        /// </summary>
        /// <param name="user">The user to configure the vehicles for.</param>
        /// <param name="vehicleList">The list of vehicles to configure.</param>
        /// <param name="vehicleMappingList">The list of vehicle mappings to configure.</param>
        /// <returns>True if the vehicles and vehicle mappings were successfully configured; otherwise, false.</returns>
        public bool ConfigurateVehicle(UserDto user, List<VehicleDto> vehicleList, List<VehicleMappingDto> vehicleMappingList);

        /// <summary>
        /// Gets a user.
        /// </summary>
        /// <param name="user">The user to get.</param>
        /// <returns>The user DTO.</returns>
        public UserDto GetUser(UserDto user);

        /// <summary>
        /// Gets the address of a user.
        /// </summary>
        /// <param name="user">The user whose address is to be retrieved.</param>
        /// <returns>The address DTO.</returns>
        public AddressDto GetAddress(UserDto user);

        /// <summary>
        /// Gets the list of vehicles associated with a user.
        /// </summary>
        /// <param name="user">The user whose vehicles are to be retrieved.</param>
        /// <returns>The list of vehicle DTOs.</returns>
        public List<VehicleDto> GetUserVehicleList(UserDto user);

        /// <summary>
        /// Gets the list of active users.
        /// </summary>
        /// <returns>The list of active user DTOs.</returns>
        public List<UserDto> GetActiveUsers();

        /// <summary>
        /// Rates a user.
        /// </summary>
        /// <param name="rate">The rating to be given.</param>
        /// <returns>True if the user was successfully rated; otherwise, false.</returns>
        public bool RateUser(RatingDto rate);

        /// <summary>
        /// Gets the rating of a user.
        /// </summary>
        /// <param name="rate">The rating DTO containing the user to be rated.</param>
        /// <returns>The rating value.</returns>
        public double UserRating(RatingDto rate);

        /// <summary>
        /// Sends a notification to a user.
        /// </summary>
        /// <param name="notification">The notification to be sent.</param>
        /// <returns>True if the notification was successfully sent; otherwise, false.</returns>
        public bool SendNotification(NotificationDto notification);

        /// <summary>
        /// Gets the list of notifications for a user.
        /// </summary>
        /// <param name="user">The user whose notifications are to be retrieved.</param>
        /// <returns>The list of notification DTOs.</returns>
        public List<NotificationDto> GetNotification(UserDto user);

        /// <summary>
        /// Updates the notification object.
        /// </summary>
        /// <param name="notification">The notification to update.</param>"
        /// <returns>True if the notification status was successfully updated; otherwise, false.</returns>
        public bool UpdateNotification(NotificationDto notification);
    }
}
