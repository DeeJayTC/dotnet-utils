// TCDev.de 2022/03/17
// TCDev.Utilities.DateTimeFormat.cs
// https://www.github.com/deejaytc/dotnet-utils

using System;

namespace TCDev.Utilities.DateTimeExtensions;

public static class DateTimeFormat
{
   private static readonly DateTime UnixRefereceDataTime = new DateTime(1970, 1, 1, 0, 0, 0, 0).ToUniversalTime();

   /// <summary>
   ///    Time passed since 1st, January 1970
   /// </summary>
   /// <param name="System.DateTime">Value from to which calculate unix reference</param>
   /// <returns>Total seconds password since 1st, January 1970</returns>
   public static long GetUnixTime(this DateTime DateTime)
   {
      return (long)(DateTime.ToUniversalTime() - UnixRefereceDataTime).TotalSeconds;
   }

   /// <summary>
   ///    Time passed since specified value in user friendly string e.g '3 mins ago'
   /// </summary>
   /// <param name="System.DateTime">Value to convert in user friendly string</param>
   /// <returns>User friendly System.DateTime string e.g '3 mins ago'</returns>
   public static string When(this DateTime DateTime)
   {
      return DateTime.When(DateTime.Now);
   }

   /// <summary>
   ///    Time passed since specified value in user friendly string e.g '3 days ago'
   /// </summary>
   /// <param name="System.DateTime">Value to convert in user friendly string</param>
   /// <param name="currentTime">Value to take reference as current time when converting to user friendly string</param>
   /// <returns>User friendly System.DateTime string e.g '3 days ago'</returns>
   public static string When(this DateTime DateTime, DateTime currentTime)
   {
      var timespan = currentTime - DateTime;

      if (timespan.Days > 365)
         return string.Format("{0} year{1} ago", timespan.Days / 365, timespan.Days / 365 > 1 ? "s" : "");

      if (timespan.Days > 30)
         return string.Format("{0} month{1} ago", timespan.Days / 30, timespan.Days / 30 > 1 ? "s" : "");

      if (timespan.Days > 0)
         return string.Format("{0} day{1} ago", timespan.Days, timespan.Days > 1 ? "s" : "");

      if (timespan.Hours > 0)
         return string.Format("{0} hour{1} ago", timespan.Hours, timespan.Hours > 1 ? "s" : "");

      if (timespan.Minutes > 0)
         return string.Format("{0} minute{1} ago", timespan.Minutes, timespan.Minutes > 1 ? "s" : "");

      return "A moment ago";
   }

   /// <summary>
   ///    Time passed since specified value in user friendly string e.g '3 days ago'
   /// </summary>
   /// <param name="System.DateTime">Value to convert in user friendly string</param>
   /// <param name="currentTime">Value to take reference as current time when converting to user friendly string</param>
   /// <returns>User friendly System.DateTime string e.g '3 days ago'</returns>
   public static string When_DE(this DateTime DateTime, DateTime currentTime)
   {
      var timespan = currentTime - DateTime;

      if (timespan.Days > 365)
         return string.Format("vor {0} Jahr{1}", timespan.Days / 365, timespan.Days / 365 > 1 ? "e" : "");

      if (timespan.Days > 30)
         return string.Format("vor {0} month{1}", timespan.Days / 30, timespan.Days / 30 > 1 ? "e" : "");

      if (timespan.Days > 0)
         return string.Format("vor {0} day{1}", timespan.Days, timespan.Days > 1 ? "en" : "");

      if (timespan.Hours > 0)
         return string.Format("vor {0} hour{1}", timespan.Hours, timespan.Hours > 1 ? "en" : "");

      if (timespan.Minutes > 0)
         return string.Format("vor {0} minute{1}", timespan.Minutes, timespan.Minutes > 1 ? "en" : "");

      return "einen kurzen Moment";
   }
}
