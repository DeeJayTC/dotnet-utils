// TCDev 2022/03/17
// Apache 2.0 License
// https://www.github.com/deejaytc/dotnet-utils

using System;
using System.ComponentModel;

namespace TCDev.Utilities.Enums;

public static class EnumHelper
{
   public static string DescriptionAttr<T>(this T source)
   {
      var fi = source.GetType().GetField(source.ToString());

      var attributes = (DescriptionAttribute[]) fi.GetCustomAttributes(
         typeof(DescriptionAttribute), false);

      if (attributes.Length > 0) return attributes[0].Description;
      return source.ToString();
   }
}

/// <summary>
///    Allows parsing specified string value into an enum type
/// </summary>
/// <typeparam name="T"></typeparam>
public static class Enum<T>
{
   public static T Parse(string value)
   {
      return Parse(value, true);
   }

   public static T Parse(string value, bool ignoreCase)
   {
      return (T) Enum.Parse(typeof(T), value, ignoreCase);
   }

   public static bool TryParse(string value, out T returnedValue)
   {
      return TryParse(value, true, out returnedValue);
   }

   public static bool TryParse(string value, bool ignoreCase, out T returnedValue)
   {
      try
      {
         returnedValue = (T) Enum.Parse(typeof(T), value, ignoreCase);
         return true;
      }
      catch
      {
         returnedValue = default;
         return false;
      }
   }
}