using System.ComponentModel;

namespace WayMatcherBL.Enums
{
    public enum RESTCode
    {
        [Description("Success")]
        Success = 1,
        [Description("DbObjectNotFound")]
        DbObjectNotFound = 2,
        [Description("InternalServerError")]
        InternalServerError = 500,
        [Description("ObjectNull")]
        ObjectNull = 4
    }
}
