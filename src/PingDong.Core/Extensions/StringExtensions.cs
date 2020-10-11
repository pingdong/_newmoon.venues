namespace PingDong
{
    public static class StringExtensions
    {
        /// <summary>
        /// Change to first character in the given string to upper case
        /// </summary>
        /// <param name="value">the given string</param>
        /// <returns></returns>
        public static string FirstCharToUpper(this string value)
        {
            value.EnsureNotNullOrWhitespace(nameof(value));

            return char.ToUpper(value[0]) + value.Substring(1);
        }

        /// <summary>
        /// Change to first character in the given string to upper case
        /// </summary>
        /// <param name="value">the given string</param>
        /// <returns></returns>
        public static string FirstCharToLower(this string value)
        {
            value.EnsureNotNullOrWhitespace(nameof(value));

            return char.ToLower(value[0]) + value.Substring(1);
        }
    }
}
