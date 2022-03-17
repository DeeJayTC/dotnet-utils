// TCDev 2022/03/17
// Apache 2.0 License
// https://www.github.com/deejaytc/dotnet-utils

namespace TCDev.Utilities.DateTimeUtils;

public static class ExcelSerialDateConverter
{
   public static int ToExcelSerialDate(this System.DateTime value)
   {
      if (value.Day == 29 && value.Month == 02 && value.Year == 1900) return 60;
      long nSerialDate = 1461 * (value.Year + 4800 + (value.Month - 14) / 12) / 4 +
         367 * (value.Month - 2 - 12 * ((value.Month - 14) / 12)) / 12 -
         3 * (value.Year + 4900 + (value.Month - 14) / 12) / 100 / 4 +
         value.Day - 2415019 - 32075;

      if (nSerialDate < 60) nSerialDate--;
      return (int) nSerialDate;
   }

   public static int ToExcelSerialDate(long nDay, long nMonth, long nYear)
   {
      // Excel/Lotus 123 have a bug with 29-02-1900. 1900 is not a
      // leap year, but Excel/Lotus 123 think it is...
      if (nDay == 29 && nMonth == 02 && nYear == 1900)
         return 60;

      // DMY to Modified Julian calculatie with an extra substraction of 2415019.
      var nSerialDate = 1461 * (nYear + 4800 + (nMonth - 14) / 12) / 4 +
         367 * (nMonth - 2 - 12 * ((nMonth - 14) / 12)) / 12 -
         3 * (nYear + 4900 + (nMonth - 14) / 12) / 100 / 4 +
         nDay - 2415019 - 32075;

      if (nSerialDate < 60)
         // Because of the 29-02-1900 bug, any <B style="COLOR: black; BACKGROUND-COLOR: #a0ffff">serial date</B> 
         // under 60 is one off... Compensate.
         nSerialDate--;

      return (int) nSerialDate;
   }
}

public static class ExcelSerialDate
{
   public static System.DateTime ToDateTime(this int pSerialDate)
   {
      int nDay;
      int nMonth;
      int nYear;

      // Excel/Lotus 123 have a bug with 29-02-1900. 1900 is not a
      // leap year, but Excel/Lotus 123 think it is...
      if (pSerialDate == 60)
      {
         nDay = 29;
         nMonth = 2;
         nYear = 1900;
      }
      else
      {
         if (pSerialDate < 60)
            // Because of the 29-02-1900 bug, any serial date 
            // under 60 is one off... Compensate.
            pSerialDate++;

         // Modified Julian to DMY calculation with an addition of 2415019
         var l = pSerialDate + 68569 + 2415019;
         var n = 4 * l / 146097;
         l = l - (146097 * n + 3) / 4;
         var i = 4000 * (l + 1) / 1461001;
         l = l - 1461 * i / 4 + 31;
         var j = 80 * l / 2447;
         nDay = l - 2447 * j / 80;
         l = j / 11;
         nMonth = j + 2 - 12 * l;
         nYear = 100 * (n - 49) + i + l;
      }

      // Datum aus den Integer Werten erstellen und im Format TT.MM.JJJJ zurückgeben
      return new System.DateTime(nYear, nMonth, nDay);
   }
}