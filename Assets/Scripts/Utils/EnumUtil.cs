using System;
using System.Collections.Generic;
using System.Linq;

namespace Utils
{
    /// <summary>
    /// Util class, for enums
    /// </summary>
    public static class EnumUtil
    {
        /// <summary>
        /// Get all values within Enum
        /// </summary>
        /// <typeparam name="T">Enum to get values from</typeparam>
        /// <returns></returns>
        public static IEnumerable<T> GetValues<T>() {
            return Enum.GetValues(typeof(T)).Cast<T>();
        }
    }
}
