// TCDev.de 2021/08/30
// TCDev.Utilities.DateTimeRenderExtension.cs
// https://www.github.com/deejaytc/dotnet-utils

using System;
using System.Text;

namespace TCDev.Utilities.DateTimeUtils;

public static class DateTimeRenderExtension
{
   public static string ToTimeAgo(this DateTime dt)
   {
      var span = DateTime.Now - dt;
      if (span.Days > 365)
      {
         var years = span.Days / 365;
         if (span.Days % 365 != 0)
            years += 1;
         return string.Format("about {0} {1} ago",
            years, years == 1 ? "year" : "years");
      }

      if (span.Days > 30)
      {
         var months = span.Days / 30;
         if (span.Days % 31 != 0)
            months += 1;
         return string.Format("about {0} {1} ago",
            months, months == 1 ? "month" : "months");
      }

      if (span.Days > 0)
         return string.Format("about {0} {1} ago",
            span.Days, span.Days == 1 ? "day" : "days");
      if (span.Hours > 0)
         return string.Format("about {0} {1} ago",
            span.Hours, span.Hours == 1 ? "hour" : "hours");
      if (span.Minutes > 0)
         return string.Format("about {0} {1} ago",
            span.Minutes, span.Minutes == 1 ? "minute" : "minutes");
      if (span.Seconds > 5)
         return string.Format("about {0} seconds ago", span.Seconds);
      if (span.Seconds <= 5)
         return "now";
      return string.Empty;
   }

   public static string ToLongFullDate(this DateTime Date)
   {
      var sb = new StringBuilder();


      if (Date.Date == DateTime.Today)
         sb.Append("Today ");
      else if (Date.Date == DateTime.Today.AddDays(-1))
         sb.Append("Yesterday ");
      else
         sb.Append("");

      sb.Append(Date.ToShortTimeString() + " - ");
      sb.Append(Date.ToShortDateString());
      return sb.ToString();
   }

   public static string ToContractDateWithWarnings(this DateTime Date)
   {
      var sb = new StringBuilder();

      if (Date > DateTime.Now.AddDays(30))
         sb.Append(Date.ToString("d"));
      else if (Date < DateTime.Now.AddDays(30))
         sb.Append(Date.ToString("d") + "&nbsp; <i style='color:yellow' class='fa fa-warning'></i>");

      else if (Date < DateTime.Now) sb.Append(Date.ToString("d") + "&nbsp; <i style='color:red' class='fa fa-warning'></i>");

      return sb.ToString();
   }

   public static string ToRenderedNumber(this DateTime date)
   {
      return date.ToString("yyymmdd");
   }
}
