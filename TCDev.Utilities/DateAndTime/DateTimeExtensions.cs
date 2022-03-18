// TCDev.de 2022/03/17
// TCDev.Utilities.DateTimeExtensions.cs
// https://www.github.com/deejaytc/dotnet-utils

using System;
using System.Globalization;

namespace TCDev.Utilities.DateTimeExtensions;

public static class DateTimeExtensions
{
   /// <summary>
   ///    Is a given Date on a Weekend?
   /// </summary>
   /// <param name="value"></param>
   /// <returns></returns>
   public static bool IsWeekend(this DateTime value)
   {
      return value.DayOfWeek == DayOfWeek.Sunday || value.DayOfWeek == DayOfWeek.Saturday;
   }

   /// <summary>
   ///    Is a given Date a Weekday?
   /// </summary>
   /// <param name="d"></param>
   /// <returns></returns>
   public static bool IsWeekday(this DayOfWeek d)
   {
      switch (d)
      {
         case DayOfWeek.Sunday:
         case DayOfWeek.Saturday:
            return false;

         default:
            return true;
      }
   }

   /// <summary>
   ///    Add a given amount of Workdays to a date (weekends are not counted)
   /// </summary>
   /// <param name="d"></param>
   /// <param name="days"></param>
   /// <returns></returns>
   public static DateTime AddWorkdays(this DateTime d, int days)
   {
      // start from a weekday
      while (d.DayOfWeek.IsWeekday()) d = d.AddDays(1.0);
      for (var i = 0; i < days; ++i)
      {
         d = d.AddDays(1.0);
         while (d.DayOfWeek.IsWeekday()) d = d.AddDays(1.0);
      }

      return d;
   }

   /// <summary>
   ///    Check if a given Date is between two dates. Ignore Time on demand
   /// </summary>
   /// <param name="dt"></param>
   /// <param name="startDate"></param>
   /// <param name="endDate"></param>
   /// <param name="compareTime"></param>
   /// <returns></returns>
   public static bool IsBetween(this DateTime dt, DateTime startDate, DateTime endDate, bool compareTime = false)
   {
      return compareTime
         ? dt >= startDate && dt <= endDate
         : dt.Date >= startDate.Date && dt.Date <= endDate.Date;
   }

   /// <summary>
   ///    Is the Given year a Leap Year
   /// </summary>
   /// <param name="value"></param>
   /// <returns></returns>
   public static bool IsLeapYear(this DateTime value)
   {
      return DateTime.DaysInMonth(value.Year, 2) == 29;
   }


   /// <summary>
   ///    Last day of a given Month
   /// </summary>
   /// <param name="dateTime"></param>
   /// <returns></returns>
   public static DateTime GetLastDayOfMonth(this DateTime dateTime)
   {
      return new DateTime(dateTime.Year, dateTime.Month, 1).AddMonths(1)
         .AddDays(-1);
   }

   /// <summary>
   ///    Converts a System.DateTime object to Unix timestamp
   /// </summary>
   /// <returns>The Unix timestamp</returns>
   public static long ToUnixTimestamp(this DateTime date)
   {
      var unixEpoch = new DateTime(1970, 1, 1, 0, 0, 0);
      var unixTimeSpan = date - unixEpoch;

      return (long)unixTimeSpan.TotalSeconds;
   }

   /// <summary>
   ///    Convert a string to Nullable DateTime
   /// </summary>
   /// <param name="s"></param>
   /// <returns></returns>
   public static DateTime? ToDateTime(this string s)
   {
      DateTime dtr;
      var tryDtr = DateTime.TryParse(s, out dtr);
      return tryDtr ? dtr : new DateTime?();
   }


   public static DateTime ToDateTimeExactMax(this string s, string format)
   {
      DateTime date;
      if (DateTime.TryParseExact(s,
             format,
             CultureInfo.InvariantCulture,
             DateTimeStyles.None,
             out date))
         return date;
      return DateTime.MaxValue;
   }

   /// <summary>
   ///    Convert a string to DateTime
   /// </summary>
   /// <param name="s"></param>
   /// <returns></returns>
   public static DateTime ToDateTimeExactMax(this string s)
   {
      string[] format =
      {
         "yyyyMMdd"
      };
      DateTime date;
      if (DateTime.TryParseExact(s,
             format,
             CultureInfo.InvariantCulture,
             DateTimeStyles.None,
             out date))
         return date;
      return DateTime.MaxValue;
   }


   /// <summary>
   ///    Convert a string to DateTime
   /// </summary>
   /// <param name="s"></param>
   /// <returns></returns>
   public static DateTime ToDateTimeExactMin(this string s)
   {
      string[] format =
      {
         "yyyyMMdd"
      };
      DateTime date;
      if (DateTime.TryParseExact(s,
             format,
             CultureInfo.InvariantCulture,
             DateTimeStyles.None,
             out date))
         return date;
      return DateTime.MinValue;
   }

   public static DateTime ToDateTimeExactMin(this string s, string format)
   {
      DateTime date;
      if (DateTime.TryParseExact(s,
             format,
             CultureInfo.InvariantCulture,
             DateTimeStyles.None,
             out date))
         return date;
      return DateTime.MinValue;
   }

   /// <summary>
   ///    Gest the elapsed seconds since the input DateTime
   /// </summary>
   /// <param name="input">Input DateTime</param>
   /// <returns>Returns a Double value with the elapsed seconds since the input DateTime</returns>
   /// <example>
   ///    Double elapsed = dtStart.ElapsedSeconds();
   /// </example>
   /// <seealso cref="Elapsed()" />
   public static double ElapsedSeconds(this DateTime input)
   {
      return DateTime.Now.Subtract(input)
         .TotalSeconds;
   }

   /// <summary>
   ///    Return a Date as Oracle SQL Date
   /// </summary>
   /// <param name="dateTime"></param>
   /// <returns></returns>
   public static string ToOracleSqlDate(this DateTime dateTime)
   {
      return string.Format("to_date('{0}','dd.mm.yyyy hh24.mi.ss')", dateTime.ToString("dd.MM.yyyy HH:mm:ss"));
   }

   /// <summary>
   ///    Get the beginning of a Month
   /// </summary>
   /// <param name="date"></param>
   /// <returns></returns>
   public static DateTime BeginningOfTheMonth(this DateTime date)
   {
      return new DateTime(date.Year, date.Month, 1);
   }
}
