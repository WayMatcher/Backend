using System.ComponentModel;
using System.Reflection;

namespace WayMatcherBL.Enums
{
    public enum State
    {
        [Description("Active")]
        Active,
        [Description("Inactive")]
        Inactive,
        [Description("Pending")]
        Pending
    }
}
