using System.ComponentModel;

namespace WayMatcherBL.Enums
{
    /// <summary>
    /// Represents the various REST response codes.
    /// </summary>
    public enum RESTCode
    {
        /// <summary>
        /// The request was successful.
        /// </summary>
        [Description("Success")]
        Success = 200,

        /// <summary>
        /// The requested database object was not found.
        /// </summary>
        [Description("DbObjectNotFound")]
        DbObjectNotFound = 2,

        /// <summary>
        /// An internal server error occurred.
        /// </summary>
        [Description("InternalServerError")]
        InternalServerError = 500,

        /// <summary>
        /// The provided object is null.
        /// </summary>
        [Description("ObjectNull")]
        ObjectNull = 4
    }
}
