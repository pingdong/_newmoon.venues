using System;
using System.Globalization;

namespace PingDong
{
    public static class DateTimeExtensions
    {
        #region Const

        private const string UtcStringFormat = "o";

        #endregion

        #region Extension Methods

        public static long ToTimestampWithSecond(this DateTime dateTime)
        {
            if (dateTime < DateTime.UnixEpoch)
                throw new ArgumentOutOfRangeException(nameof(dateTime), "The time is early than the epoch time.");

            return new DateTimeOffset(dateTime).ToUnixTimeSeconds();
        }

        public static long ToTimestampWithMillisecond(this DateTime dateTime)
        {
            if (dateTime < DateTime.UnixEpoch)
                throw new ArgumentOutOfRangeException(nameof(dateTime), "The time is early than the epoch time.");

            return new DateTimeOffset(dateTime).ToUnixTimeMilliseconds();
        }

        public static void EnsureUtc(this DateTime dateTime, string parameterName = null)
        {
            if (dateTime.Kind != DateTimeKind.Utc)
                throw new ArgumentException("The provided value must be a UTC datetime", parameterName);
        }

        public static DateTime ToLocalDateTime(this DateTime datetime, string timeZoneId)
        {
            timeZoneId.EnsureNotNullOrWhitespace(nameof(timeZoneId));

            return ToLocalDateTime(datetime, TimeZoneInfo.FindSystemTimeZoneById(timeZoneId));
        }

        public static DateTime ToLocalDateTime(this DateTime datetime, TimeZoneInfo timeZone)
        {
            timeZone.EnsureNotNull(nameof(timeZone));

            return datetime.Kind == DateTimeKind.Utc
                ? TimeZoneInfo.ConvertTimeFromUtc(datetime, timeZone)
                : TimeZoneInfo.ConvertTime(datetime, timeZone);
        }

        public static DateTime ToMidnight(this DateTime datetime)
        {
            return new DateTime(datetime.Year, datetime.Month, datetime.Day, 0, 0, 0, datetime.Kind);
        }

        public static string ToUtcString(this DateTime utc)
        {
            utc.EnsureUtc(nameof(utc));

            return utc.ToString(UtcStringFormat, CultureInfo.InvariantCulture);
        }

        #endregion

        #region Waiting for Extend Everything feature

        // Those methods are waiting for the feature 'Extend Everything' is in place to extend string and long object
        // This feature didn't make in the C# 8.0

        // TODO: Extend TimeZoneInfo
        public static TimeZoneInfo NewZealandStandardTime =>
            TimeZoneInfo.FindSystemTimeZoneById("New Zealand Standard Time");

        // TODO: Extend long
        public static DateTime FromTimestampWithSecond(long timestamp)
        {
            if (timestamp <= 0)
                throw new ArgumentException("Invalid timestamp", nameof(timestamp));

            return DateTimeOffset.FromUnixTimeSeconds(timestamp).UtcDateTime;
        }

        public static DateTime FromTimestampWithMillisecond(long timestamp)
        {
            if (timestamp <= 0)
                throw new ArgumentException("Invalid timestamp", nameof(timestamp));

            return DateTimeOffset.FromUnixTimeMilliseconds(timestamp).UtcDateTime;
        }

        // TODO: Extend string
        public static DateTime FromUtcString(string datetime)
        {
            datetime.EnsureNotNullOrWhitespace(nameof(datetime));

            var converted = DateTime.TryParseExact(datetime
                                                        , UtcStringFormat
                                                        , CultureInfo.InvariantCulture
                                                        , DateTimeStyles.None,
                                                        out DateTime result);

            return converted
                ? result.ToUniversalTime()
                : throw new ArgumentException($"The provided value, {datetime}, is not a valid UTC datetime string",
                    nameof(datetime));
        }

        // TODO: Extend DateTime
        public static DateTime GetCurrentTime(string timeZoneId)
        {
            timeZoneId.EnsureNotNullOrWhitespace(nameof(timeZoneId));

            return GetCurrentTime(TimeZoneInfo.FindSystemTimeZoneById(timeZoneId));
        }

        public static DateTime GetCurrentTime(TimeZoneInfo timeZone)
        {
            timeZone.EnsureNotNull(nameof(timeZone));

            return TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, timeZone);
        }

        #endregion
    }
}
