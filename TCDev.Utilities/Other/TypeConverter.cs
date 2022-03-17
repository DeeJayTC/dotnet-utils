// TCDev 2022/03/17
// Apache 2.0 License
// https://www.github.com/deejaytc/dotnet-utils

using System;

namespace TCDev.Office.Core.Extensions;

public static class TCTypeConverter
{
   /// <summary>
   ///    converts one type to another
   ///    Example:
   ///    var age = "28";
   ///    var intAge = age.To
   ///    <int>
   ///       ();
   ///       var doubleAge = intAge.To
   ///       <double>
   ///          ();
   ///          var decimalAge = doubleAge.To<decimal>();
   /// </summary>
   /// <typeparam name="T"></typeparam>
   /// <param name="value"></param>
   /// <returns></returns>
   public static T To<T>(this IConvertible value)
   {
      try
      {
         var t = typeof(T);
         var u = Nullable.GetUnderlyingType(t);

         if (u != null)
         {
            if (value == null || value.Equals(""))
               return default;

            return (T) Convert.ChangeType(value, u);
         }

         if (value == null || value.Equals(""))
            return default;

         return (T) Convert.ChangeType(value, t);
      }

      catch
      {
         return default;
      }
   }

   public static T To<T>(this IConvertible value, IConvertible ifError)
   {
      try
      {
         var t = typeof(T);
         var u = Nullable.GetUnderlyingType(t);

         if (u != null)
         {
            if (value == null || value.Equals(""))
               return (T) ifError;

            return (T) Convert.ChangeType(value, u);
         }

         if (value == null || value.Equals(""))
            return ifError.To<T>();

         return (T) Convert.ChangeType(value, t);
      }
      catch
      {
         return (T) ifError;
      }
   }
}