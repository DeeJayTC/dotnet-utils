// TCDev.de 2021/08/30
// TCDev.Utilities.DateTime.cs
// https://www.github.com/deejaytc/dotnet-utils

using System;
using System.Collections.Generic;
using System.Linq;
using TCDev.Utilities.DateAndTime.BankHolidays;
using TCDev.Utilities.DateAndTime.BankHolidays.Base;

namespace TCDev.Utilities.DateAndTime;

[Flags]
public enum DaysOfWeek
{
   sunday = 1, monday = 2, tuesday = 4, wednesday = 8, thursday = 16, friday = 32, Saturday = 64
}

public static class DateTimeExtensions
{
   /// <summary>
   ///    Return Age in Years for a given Birthdate
   /// </summary>
   /// <param name="dateOfBirth"></param>
   /// <returns></returns>
   public static int Age(this DateTime dateOfBirth)
   {
      if (DateTime.Today.Month < dateOfBirth.Month
          || DateTime.Today.Month == dateOfBirth.Month && DateTime.Today.Day < dateOfBirth.Day)
         return DateTime.Today.Year - dateOfBirth.Year - 1;
      return DateTime.Today.Year - dateOfBirth.Year;
   }


   /// <summary>
   ///    Validate if given days intersect
   /// </summary>
   /// <param name="startDate"></param>
   /// <param name="endDate"></param>
   /// <param name="intersectingStartDate"></param>
   /// <param name="intersectingEndDate"></param>
   /// <returns></returns>
   public static bool Intersects(this DateTime startDate, DateTime endDate, DateTime intersectingStartDate, DateTime intersectingEndDate)
   {
      return intersectingEndDate >= startDate && intersectingStartDate <= endDate;
   }

   /// <summary>
   ///    Validate if a given day is a weekday
   /// </summary>
   /// <param name="d"></param>
   /// <returns></returns>
   public static bool IsWeekday(this DayOfWeek d)
   {
      switch (d)
      {
         case DayOfWeek.Sunday:
         case DayOfWeek.Saturday: return false;

         default: return true;
      }
   }

   /// <summary>
   ///    Validate if the given day is a weekday
   /// </summary>
   /// <param name="date"></param>
   /// <returns></returns>
   public static bool IsWeekend(this DateTime date)
   {
      return !IsWeekday(date.DayOfWeek);
   }

   /// <summary>
   ///    Adds the amount of working days to a date, ignores saturday+wednesday, can also ignore BankHolidays
   /// </summary>
   /// <param name="d"></param>
   /// <param name="days"></param>
   /// <returns></returns>
   public static DateTime AddWorkdays(this DateTime d, int days, List<PublicHoliday> bankHolidays)
   {
      // start from a weekday
      while (!d.IsWeekend()
             && bankHolidays.All(x => x.Date != d.Date)) d = d.AddDays(1.0);
      for (var i = 0; i < days; ++i)
      {
         d = d.AddDays(1.0);
         while (!d.IsWeekend()
                && bankHolidays.All(x => x.Date != d.Date)) d = d.AddDays(1.0);
      }

      return d;
   }


   /// <summary>
   ///    Calculates number of business days, taking into account:
   ///    - weekends (Saturdays and Sundays)
   ///    - bank holidays in the middle of the week
   /// </summary>
   /// <param name="firstDay">First day in the time interval</param>
   /// <param name="lastDay">Last day in the time interval</param>
   /// <param name="bankHolidays">List of bank holidays excluding weekends</param>
   /// <returns>Number of business days during the 'span'</returns>
   public static int BusinessDaysUntil(DateTime firstDay, DateTime lastDay, IEnumerable<PublicHoliday> bankHolidays)
   {
      return GetDateRange(firstDay, lastDay)
         .Where(a => !a.IsWeekend())
         .Count(a => bankHolidays.All(x => x.Date != a.Date));
   }

   private static IEnumerable<DateTime> GetDateRange(DateTime startDate, DateTime endDate)
   {
      if (endDate < startDate)
         throw new ArgumentException("endDate must be greater than or equal to startDate");

      while (startDate <= endDate)
      {
         yield return startDate;
         startDate = startDate.AddDays(1);
      }
   }

   /// <summary>
   ///    If the given date on this Weekday it can be shifted
   /// </summary>
   /// <param name="value">The date</param>
   /// <param name="saturday">shift for Saturday</param>
   /// <param name="sunday">shift for Sunday</param>
   /// <param name="monday">shift for Monday</param>
   /// <returns></returns>
   public static DateTime Shift(this DateTime value, Func<DateTime, DateTime> saturday, Func<DateTime, DateTime> sunday, Func<DateTime, DateTime> monday = null)
   {
      switch (value.DayOfWeek)
      {
         case DayOfWeek.Saturday:
            return saturday.Invoke(value);

         case DayOfWeek.Sunday:
            return sunday.Invoke(value);

         case DayOfWeek.Monday:
            if (monday != null) return monday.Invoke(value);
            break;
      }

      return value;
   }

   /// <summary>
   ///    If the given date on this Weekday it can be shifted
   /// </summary>
   /// <param name="value">The date</param>
   /// <param name="dayOfWeek">Weekday</param>
   /// <param name="shift"></param>
   /// <returns></returns>
   public static DateTime Shift(this DateTime value, DayOfWeek dayOfWeek, Func<DateTime, DateTime> shift)
   {
      if (shift != null
          && value.DayOfWeek == dayOfWeek) return shift.Invoke(value);

      return value;
   }

   /// <summary>
   ///    ShiftWeekdays
   /// </summary>
   /// <param name="value"></param>
   /// <param name="monday"></param>
   /// <param name="tuesday"></param>
   /// <param name="wednesday"></param>
   /// <param name="thursday"></param>
   /// <param name="friday"></param>
   /// <returns></returns>
   public static DateTime ShiftWeekdays(this DateTime value, Func<DateTime, DateTime> monday = null, Func<DateTime, DateTime> tuesday = null, Func<DateTime, DateTime> wednesday = null, Func<DateTime, DateTime> thursday = null, Func<DateTime, DateTime> friday = null)
   {
      switch (value.DayOfWeek)
      {
         case DayOfWeek.Monday:
            if (monday != null) return monday.Invoke(value);
            break;

         case DayOfWeek.Tuesday:
            if (tuesday != null) return tuesday.Invoke(value);
            break;

         case DayOfWeek.Wednesday:
            if (wednesday != null) return wednesday.Invoke(value);
            break;

         case DayOfWeek.Thursday:
            if (thursday != null) return thursday.Invoke(value);
            break;

         case DayOfWeek.Friday:
            if (friday != null) return friday.Invoke(value);
            break;
      }

      return value;
   }


   /// <summary>
   ///    Calculate the amount of working days in the week of a given date based on bankHolidays array
   /// </summary>
   /// <param name="weekNum"></param>
   /// <param name="bankHolidays"></param>
   /// <returns></returns>
   public static int WorkingDaysInWeek(DateTime date, CountryCode code)
   {
      var start = date.StartOfWeek();
      var end = date.EndOfWeek();
      var bankHols = DateUtilities.GetPublicHolidays(start, end, code);
      return 5 - bankHols.Count();
   }


   /// <summary>
   ///    Returns the start of week (ie monday) of a given date
   /// </summary>
   /// <param name="dt"></param>
   /// <param name="startOfWeek"></param>
   /// <returns></returns>
   public static DateTime StartOfWeek(this DateTime dt)
   {
      var diff = 1 - (int)dt.DayOfWeek;
      return dt.AddDays(diff);
   }


   /// <summary>
   ///    Returns the end of week (ie friday) of a given date
   /// </summary>
   /// <param name="dt"></param>
   /// <param name="startOfWeek"></param>
   /// <returns></returns>
   public static DateTime EndOfWeek(this DateTime dt)
   {
      var diff = 5 - (int)dt.DayOfWeek;
      return dt.AddDays(diff);
   }
}
