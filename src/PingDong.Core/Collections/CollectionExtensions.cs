using System;
using System.Collections.Generic;
using System.Linq;

namespace PingDong.Collections
{
    public static class CollectionsExtensions
    {
        /// <summary>
        /// Is the source null or empty.
        /// </summary>
        /// <typeparam name="T">Type of element</typeparam>
        /// <param name="target">A list of elements</param>
        /// <returns>If the given list is null or empty, this method return True, otherwise False</returns>
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> target)
        {
            return target == null || !target.Any();
        }

        /// <summary>
        /// Throws ArgumentNullException if the parameter is null or default.
        /// </summary>
        /// <typeparam name="T">The data type of parameter</typeparam>
        /// <param name="parameter">The parameter value</param>
        /// <param name="parameterName">The name of parameter</param>
        /// <exception cref="ArgumentNullException">The parameter is null or doesn't have any item</exception>
        public static IEnumerable<T> EnsureNotNullOrEmpty<T>(this IEnumerable<T> parameter, string parameterName = null)
        {
            var collection = parameter as T[] ?? parameter.ToArray();

            if (parameter == null || !collection.Any())
                throw new ArgumentNullException(parameterName);

            return collection;
        }
    }
}
