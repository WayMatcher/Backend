using System.ComponentModel;

namespace WayMatcherBL.Enums
{
    /// <summary>
    /// Represents the various states that an entity can be in.
    /// </summary>
    public enum State
    {
        /// <summary>
        /// The entity is active.
        /// </summary>
        [Description("Active")]
        Active = 1,

        /// <summary>
        /// The entity is inactive.
        /// </summary>
        [Description("Inactive")]
        Inactive = 14,

        /// <summary>
        /// The entity is banned.
        /// </summary>
        [Description("Banned")]
        Banned = 15,

        /// <summary>
        /// The entity is expired.
        /// </summary>
        [Description("Expired")]
        Expired = 16,

        /// <summary>
        /// The entity is completed.
        /// </summary>
        [Description("Completed")]
        Completed = 17,

        /// <summary>
        /// The entity is cancelled.
        /// </summary>
        [Description("Cancelled")]
        Cancelled = 18,

        /// <summary>
        /// The entity is pending.
        /// </summary>
        [Description("Pending")]
        Pending = 19,

        /// <summary>
        /// The entity is unread.
        /// </summary>
        [Description("Unread")]
        Unread = 20,
    }
}
