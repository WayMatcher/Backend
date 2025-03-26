using System.ComponentModel;

namespace WayMatcherBL.Enums
{
    /// <summary>
    /// Represents the roles that a user can have in an event.
    /// </summary>
    public enum EventRole
    {
        /// <summary>
        /// The user is a pilot.
        /// </summary>
        [Description("Pilot")]
        Pilot = 1,

        /// <summary>
        /// The user is a passenger.
        /// </summary>
        [Description("Passenger")]
        Passenger = 2
    }
}
