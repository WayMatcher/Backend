using System.ComponentModel;
using System.Reflection;

namespace WayMatcherBL.Enums
{
    /// <summary>
    /// Utility class for working with enums.
    /// </summary>
    public static class EnumUtils
    {
        /// <summary>
        /// Gets the description attribute of an enum value.
        /// </summary>
        /// <param name="value">The enum value.</param>
        /// <returns>The description of the enum value, or the enum value as a string if no description is found.</returns>
        public static string GetDescription(this Enum value)
        {
            FieldInfo field = value.GetType().GetField(value.ToString());
            DescriptionAttribute attribute = field.GetCustomAttribute<DescriptionAttribute>();
            return attribute == null ? value.ToString() : attribute.Description;
        }
    }
}
