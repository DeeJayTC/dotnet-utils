// TCDev 2022/03/17
// Apache 2.0 License
// https://www.github.com/deejaytc/dotnet-utils

using System;

namespace TCDev.Utilities.DateAndTime;

public static class DatetimeCalculations
{
   public static int GetDaysBetween(this DateTime from, DateTime to)
   {
      var ts = new TimeSpan(from.Ticks - to.Ticks);
      return ts.Days;
   }


   public static int GetMonthsBetween(this DateTime from, DateTime to)
   {
      if (from > to) return GetMonthsBetween(to, from);

      var monthDiff = Math.Abs(to.Year * 12 + (to.Month - 1) - (from.Year * 12 + (from.Month - 1)));

      if (from.AddMonths(monthDiff) > to || to.Day < from.Day)
         return monthDiff - 1;
      return monthDiff;
   }
}