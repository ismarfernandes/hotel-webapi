using System;

namespace Hotel.Shared.Extensions
{
    public static class DateTimeExtension
    {
        public static DateTime StartOfDay(this DateTime date)
        {
            return date.Date;
        }

        public static DateTime EndOfDay(this DateTime date)
        {
            return date.Date.AddDays(1).AddSeconds(-1);
        }
    }
}
