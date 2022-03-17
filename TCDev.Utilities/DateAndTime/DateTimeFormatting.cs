// TCDev 2022/03/17
// Apache 2.0 License
// https://www.github.com/deejaytc/dotnet-utils

namespace TCDev.Utilities.DateTimeUtils;

public static class DateTimeFormatting
{
   public static string ElapsedTime(System.DateTime dtEvent)
   {
      var TS = DateTime.Now - dtEvent;
      var intYears = DateTime.Now.Year - dtEvent.Year;
      var intMonths = DateTime.Now.Month - dtEvent.Month;
      var intDays = DateTime.Now.Day - dtEvent.Day;
      var intHours = DateTime.Now.Hour - dtEvent.Hour;
      var intMinutes = DateTime.Now.Minute - dtEvent.Minute;
      var intSeconds = DateTime.Now.Second - dtEvent.Second;
      if (intYears > 0) return $"{intYears} {(intYears == 1 ? "year" : "years")} ago";
      if (intMonths > 0) return $"{intMonths} {(intMonths == 1 ? "month" : "months")} ago";
      if (intDays > 0) return $"{intDays} {(intDays == 1 ? "day" : "days")} ago";
      if (intHours > 0) return $"{intHours} {(intHours == 1 ? "hour" : "hours")} ago";
      if (intMinutes > 0) return $"{intMinutes} {(intMinutes == 1 ? "minute" : "minutes")} ago";
      if (intSeconds > 0) return $"{intSeconds} {(intSeconds == 1 ? "second" : "seconds")} ago";
      return $"{dtEvent.ToShortDateString()} {dtEvent.ToShortTimeString()} ago";
   }

   public static string ToFriendlyDateString(this System.DateTime Date)
   {
      var FormattedDate = "";
      if (Date.Date == DateTime.Today)
         FormattedDate = "Today";
      else if (Date.Date == DateTime.Today.AddDays(-1))
         FormattedDate = "Yesterday";
      else if (Date.Date > DateTime.Today.AddDays(-6))
         // *** Show the Day of the week
         FormattedDate = Date.ToString("dddd");
      else
         FormattedDate = Date.ToString("MMMM dd, yyyy");

      //append the time portion to the output
      FormattedDate += " @ " + Date.ToString("t").ToLower();
      return FormattedDate;
   }
}