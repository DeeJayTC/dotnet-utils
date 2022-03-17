// TCDev 2022/03/17
// Apache 2.0 License
// https://www.github.com/deejaytc/dotnet-utils

using System;
using System.Globalization;

namespace TeamWorkNet.Extensions.DateTime;

public static partial class DateTimeExtensions
{
   /// <summary>
   ///    Is a given Date on a Weekend?
   /// </summary>
   /// <param name="value"></param>
   /// <returns></returns>
   public static bool IsWeekend(this System.DateTime value)
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
   public static System.DateTime AddWorkdays(this System.DateTime d, int days)
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
   public static bool IsBetween(this System.DateTime dt, System.DateTime startDate, System.DateTime endDate,
      bool compareTime = false)
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
   public static bool IsLeapYear(this System.DateTime value)
   {
      return System.DateTime.DaysInMonth(value.Year, 2) == 29;
   }


   /// <summary>
   ///    Last day of a given Month
   /// </summary>
   /// <param name="dateTime"></param>
   /// <returns></returns>
   public static System.DateTime GetLastDayOfMonth(this System.DateTime dateTime)
   {
      return new System.DateTime(dateTime.Year, dateTime.Month, 1).AddMonths(1).AddDays(-1);
   }

   /// <summary>
   ///    Converts a System.DateTime object to Unix timestamp
   /// </summary>
   /// <returns>The Unix timestamp</returns>
   public static long ToUnixTimestamp(this System.DateTime date)
   {
      var unixEpoch = new System.DateTime(1970, 1, 1, 0, 0, 0);
      var unixTimeSpan = date - unixEpoch;

      return (long) unixTimeSpan.TotalSeconds;
   }

   /// <summary>
   ///    Convert a string to Nullable DateTime
   /// </summary>
   /// <param name="s"></param>
   /// <returns></returns>
   public static System.DateTime? ToDateTime(this string s)
   {
      System.DateTime dtr;
      var tryDtr = System.DateTime.TryParse(s, out dtr);
      return tryDtr ? dtr : new System.DateTime?();
   }


   public static System.DateTime ToDateTimeExactMax(this string s, string format)
   {
      System.DateTime date;
      if (System.DateTime.TryParseExact(s,
             format,
             CultureInfo.InvariantCulture,
             DateTimeStyles.None,
             out date))
         return date;
      return System.DateTime.MaxValue;
   }

   /// <summary>
   ///    Convert a string to DateTime
   /// </summary>
   /// <param name="s"></param>
   /// <returns></returns>
   public static System.DateTime ToDateTimeExactMax(this string s)
   {
      string[] format = {"yyyyMMdd"};
      System.DateTime date;
      if (System.DateTime.TryParseExact(s,
             format,
             CultureInfo.InvariantCulture,
             DateTimeStyles.None,
             out date))
         return date;
      return System.DateTime.MaxValue;
   }


   /// <summary>
   ///    Convert a string to DateTime
   /// </summary>
   /// <param name="s"></param>
   /// <returns></returns>
   public static System.DateTime ToDateTimeExactMin(this string s)
   {
      string[] format = {"yyyyMMdd"};
      System.DateTime date;
      if (System.DateTime.TryParseExact(s,
             format,
             CultureInfo.InvariantCulture,
             DateTimeStyles.None,
             out date))
         return date;
      return System.DateTime.MinValue;
   }

   public static System.DateTime ToDateTimeExactMin(this string s, string format)
   {
      System.DateTime date;
      if (System.DateTime.TryParseExact(s,
             format,
             CultureInfo.InvariantCulture,
             DateTimeStyles.None,
             out date))
         return date;
      return System.DateTime.MinValue;
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
   public static double ElapsedSeconds(this System.DateTime input)
   {
      return System.DateTime.Now.Subtract(input).TotalSeconds;
   }

   /// <summary>
   ///    Return a Date as Oracle SQL Date
   /// </summary>
   /// <param name="dateTime"></param>
   /// <returns></returns>
   public static string ToOracleSqlDate(this System.DateTime dateTime)
   {
      return string.Format("to_date('{0}','dd.mm.yyyy hh24.mi.ss')", dateTime.ToString("dd.MM.yyyy HH:mm:ss"));
   }

   /// <summary>
   ///    Get the beginning of a Month
   /// </summary>
   /// <param name="date"></param>
   /// <returns></returns>
   public static System.DateTime BeginningOfTheMonth(this System.DateTime date)
   {
      return new System.DateTime(date.Year, date.Month, 1);
   }
}