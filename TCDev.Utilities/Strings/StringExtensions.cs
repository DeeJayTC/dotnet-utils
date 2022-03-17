// TCDev 2022/03/17
// Apache 2.0 License
// https://www.github.com/deejaytc/dotnet-utils

using System;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace TCDev.Utilities.Strings;

/// <summary>
///    This class contain extension functions for string objects
/// </summary>
public static class StringExtension
{
   public static bool ContainsAny(this string theString, char[] characters)
   {
      return characters.Any(character => theString.Contains(character.ToString()));
   }


   /// <summary>
   ///    Checks string object's value to array of string values
   /// </summary>
   /// <param name="stringValues">Array of string values to compare</param>
   /// <returns>Return true if any string value matches</returns>
   public static bool In(this string value, params string[] stringValues)
   {
      return stringValues.Any(otherValue => string.CompareOrdinal(value, otherValue) == 0);
   }

   /// <summary>
   ///    Converts string to enum object
   /// </summary>
   /// <typeparam name="T">Type of enum</typeparam>
   /// <param name="value">String value to convert</param>
   /// <returns>Returns enum object</returns>
   public static T ToEnum<T>(this string value)
      where T : struct
   {
      return (T) Enum.Parse(typeof(T), value, true);
   }

   /// <summary>
   ///    Returns characters from right of specified length
   /// </summary>
   /// <param name="value">String value</param>
   /// <param name="length">Max number of charaters to return</param>
   /// <returns>Returns string from right</returns>
   public static string? Right(this string? value, int length)
   {
      return value != null && value.Length > length ? value[^length..] : value;
   }

   /// <summary>
   ///    Returns characters from left of specified length
   /// </summary>
   /// <param name="value">String value</param>
   /// <param name="length">Max number of charaters to return</param>
   /// <returns>Returns string from left</returns>
   public static string? Left(this string? value, int length)
   {
      return value != null && value.Length > length ? value[..length] : value;
   }

   /// <summary>
   ///    Replaces the format item in a specified System.String with the text equivalent
   ///    of the value of a specified System.Object instance.
   /// </summary>
   /// <param name="value">A composite format string</param>
   /// <param name="arg0">An System.Object to format</param>
   /// <returns>
   ///    A copy of format in which the first format item has been replaced by the
   ///    System.String equivalent of arg0
   /// </returns>
   public static string Format(this string value, object arg0)
   {
      return string.Format(value, arg0);
   }

   /// <summary>
   ///    Replaces the format item in a specified System.String with the text equivalent
   ///    of the value of a specified System.Object instance.
   /// </summary>
   /// <param name="value">A composite format string</param>
   /// <param name="args">An System.Object array containing zero or more objects to format.</param>
   /// <returns>
   ///    A copy of format in which the format items have been replaced by the System.String
   ///    equivalent of the corresponding instances of System.Object in args.
   /// </returns>
   public static string Format(this string value, params object[] args)
   {
      return string.Format(value, args);
   }
}

public static class StringExtensions2
{
   public static bool IsNumeric(this string zahl)
   {
      var isNum = double.TryParse(Convert.ToString(zahl), NumberStyles.Any, NumberFormatInfo.InvariantInfo, out _);

      return isNum;
   }

   public static bool IsValidEmailAddress(this string s)
   {
      var regex = new Regex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$");
      return regex.IsMatch(s);
   }

   public static bool IsValidIpAddress(this string s)
   {
      return Regex.IsMatch(s,
         @"\b(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\b");
   }


   /// <summary>
   ///    Parse a strying to a runtime generated class
   /// </summary>
   /// <typeparam name="T"></typeparam>
   /// <param name="value"></param>
   /// <returns></returns>
   public static T Parse<T>(this string value)
   {
      // Get default value for type so if string
      // is empty then we can return default value.
      var result = default(T);
      if (!string.IsNullOrEmpty(value))
      {
         // we are not going to handle exception here
         // if you need SafeParse then you should create
         // another method specially for that.
         var tc = TypeDescriptor.GetConverter(typeof(T));
         result = (T) tc.ConvertFrom(value)!;
      }

      return result;
   }


   /// <summary>
   ///    Convert a input string to a byte array and compute the hash.
   /// </summary>
   /// <param name="value">Input string.</param>
   /// <returns>Hexadecimal string.</returns>
   public static string ToMd5Hash(this string value)
   {
      if (string.IsNullOrEmpty(value)) return value;

      using MD5 md5 = new MD5CryptoServiceProvider();
      var originalBytes = Encoding.Default.GetBytes(value);
      var encodedBytes = md5.ComputeHash(originalBytes);
      return BitConverter.ToString(encodedBytes).Replace("-", string.Empty);
   }
}