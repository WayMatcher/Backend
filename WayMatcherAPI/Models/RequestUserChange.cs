using WayMatcherBL.LogicModels;

namespace WayMatcherAPI.Models
{
    /// <summary>
    /// Represents a request model for changing user information.
    /// </summary>
    public class RequestUserChange
    {
        /// <summary>
        /// Gets or sets the user information.
        /// </summary>
        public UserDto User { get; set; }

        /// <summary>
        /// Gets or sets the list of vehicles associated with the user.
        /// </summary>
        public List<RequestVehicleModel>? VehicleList { get; set; }

        /// <summary>
        /// Gets or sets the password of the user.
        /// </summary>
        public string Password { get; set; }
    }
}
