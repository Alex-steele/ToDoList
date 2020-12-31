using System;

namespace ToDoList.Core
{
    /// <summary>
    /// Contains a set of argument checking functions
    /// </summary>
    public static class Check
    {
        /// <summary>
        /// Check if argument is null
        /// </summary>
        /// <param name="prop">Property to check</param>
        /// <param name="propertyName">Name of the property being checked</param>
        public static void NotNull(object prop, string propertyName)
        {
            if (prop == null)
            {
                throw new ArgumentNullException(propertyName);
            }
        }
        /// <summary>
        /// Check if string argument is null or white space
        /// </summary>
        /// <param name="prop">Property to check</param>
        /// <param name="propertyName">Name of the property being checked</param>
        public static void NotNullOrWhiteSpace(string prop, string propertyName)
        {
            if (string.IsNullOrWhiteSpace(prop))
            {
                throw new ArgumentNullException(propertyName);
            }
        }
    }
}
