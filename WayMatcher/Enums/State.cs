using System.ComponentModel;

namespace WayMatcherBL.Enums
{
    public enum State
    {
        [Description("Active")]
        Active = 1,
        [Description("Inactive")]
        Inactive = 14,
        [Description("Banned")]
        Banned = 15,
        [Description("Expired")]
        Expired = 16,
        [Description("Completed")]
        Completed = 17,
        [Description("Cancelled")]
        Cancelled = 18,
        [Description("Unread")]
        Unread = 19,
    }
}
