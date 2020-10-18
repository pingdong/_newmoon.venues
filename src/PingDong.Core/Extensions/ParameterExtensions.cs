using System;
using System.Collections.Generic;

namespace PingDong
{
    public static class ParameterExtensions
    {
        /// <summary>
        /// Throws ArgumentNullException if the parameter is null.
        /// </summary>
        /// <typeparam name="T">The data type of parameter</typeparam>
        /// <param name="parameter">The parameter value</param>
        /// <param name="parameterName">The name of parameter</param>
        /// <exception cref="ArgumentNullException">The parameter is null</exception>
        public static T EnsureNotNull<T>(this T parameter, string parameterName = null)
        {
            if (parameter == null)
                throw new ArgumentNullException(parameterName);

            return parameter;
        }

        /// <summary>
        /// Throws ArgumentNullException if the parameter is null.
        /// </summary>
        /// <typeparam name="T">The data type of parameter</typeparam>
        /// <param name="parameter">The parameter value</param>
        /// <param name="parameterName">The name of parameter</param>
        /// <exception cref="ArgumentNullException">The parameter is null</exception>
        public static T EnsureNotNullOrDefault<T>(this T parameter, string parameterName = null)
        {
            EnsureNotNull(parameter, parameterName);

            if (EqualityComparer<T>.Default.Equals(parameter, default))
                throw new ArgumentNullException(parameterName);

            return parameter;
        }

        /// <summary>
        /// Throws ArgumentNullException if the parameter is null or default.
        /// </summary>
        /// <typeparam name="T">The data type of parameter</typeparam>
        /// <param name="parameter">The parameter value</param>
        /// <param name="parameterName">The name of parameter</param>
        /// <exception cref="ArgumentNullException">The parameter is null or empty</exception>
        public static T EnsureNotNullDefaultOrWhitespace<T>(this T parameter, string parameterName = null)
        {
            EnsureNotNullOrDefault(parameter, parameterName);

            if (parameter is string parameterString && string.IsNullOrWhiteSpace(parameterString))
                throw new ArgumentNullException(parameterName);

            return parameter;
        }

        /// <summary>
        /// Throws ArgumentNullException if the parameter is null or whitespace.
        /// </summary>
        /// <param name="parameter">The parameter value</param>
        /// <param name="parameterName">The name of parameter</param>
        /// <exception cref="ArgumentNullException">The parameter is null or whitespace</exception>
        public static string EnsureNotNullOrWhitespace(this string parameter, string parameterName = null)
        {
            if (string.IsNullOrWhiteSpace(parameter))
                throw new ArgumentNullException(parameterName);
            
            return parameter;
        }
        
        /// <summary>
        /// Convert string to Guid, throws exception if fails.
        /// </summary>
        /// <param name="parameter">The parameter string</param>
        /// <param name="parameterName">The name of parameter</param>
        /// <returns>Guid value</returns>
        /// <exception cref="ArgumentNullException">The parameter is null</exception>
        /// <exception cref="ArgumentException">The parameter was unable to convert to Guid</exception>
        public static Guid EnsureAndConvertToGuid(this string parameter, string parameterName = null)
        {
            parameter.EnsureNotNullOrWhitespace(parameterName);

            if (!Guid.TryParse(parameter, out Guid idInGuid))
                throw new ArgumentException(parameterName);

            return idInGuid;
        }
    }
}
