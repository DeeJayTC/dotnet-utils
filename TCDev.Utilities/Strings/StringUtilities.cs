// TCDev 2022/03/17
// Apache 2.0 License
// https://www.github.com/deejaytc/dotnet-utils

using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace TCDev.Utilities;

public static class StringUtilities
{
   /// <summary>
   ///    Validate if the string is a valid Email
   /// </summary>
   /// <param name="input"></param>
   /// <returns></returns>
   public static bool IsEmail(this string input)
   {
      if (string.IsNullOrWhiteSpace(input))
         return false;

      try
      {
         // Normalize the domain
         input = Regex.Replace(input, @"(@)(.+)$", DomainMapper,
            RegexOptions.None, TimeSpan.FromMilliseconds(200));

         // Examines the domain part of the email and normalizes it.
         string DomainMapper(Match match)
         {
            // Use IdnMapping class to convert Unicode domain names.
            var idn = new IdnMapping();

            // Pull out and process domain name (throws ArgumentException on invalid)
            var domainName = idn.GetAscii(match.Groups[2].Value);

            return match.Groups[1].Value + domainName;
         }
      }
      catch (RegexMatchTimeoutException e)
      {
         return false;
      }
      catch (ArgumentException e)
      {
         return false;
      }

      try
      {
         return Regex.IsMatch(input,
            @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
            RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
      }
      catch (RegexMatchTimeoutException)
      {
         return false;
      }
   }

   /// <summary>
   ///    Convert the string into Uri
   /// </summary>
   /// <param name="input"></param>
   /// <returns></returns>
   public static Uri? ToUri(this string input)
   {
      try
      {
         return new Uri(input);
      }
      catch (Exception ex)
      {
         return default;
      }
   }

   /// <summary>
   ///    Validate if the string is a valid date
   /// </summary>
   /// <param name="input"></param>
   /// <returns></returns>
   public static bool IsDate(this string input)
   {
      DateTime temp;
      if (DateTime.TryParse(input, out temp)) return true;
      return false;
   }

   /// <summary>
   ///    Convert the string to a DateTime object
   /// </summary>
   /// <param name="input"></param>
   /// <returns></returns>
   public static DateTime ToDateTime(this string input)
   {
      DateTime temp;
      if (!DateTime.TryParse(input, out temp)) return default;
      return temp;
   }

   /// <summary>
   ///    Remove invalid characters from a string
   /// </summary>
   /// <param name="strIn"></param>
   /// <returns></returns>
   public static string Clean(this string strIn)
   {
      // Replace invalid characters with empty strings.
      try
      {
         return Regex.Replace(strIn, @"[^\w\.@-]", "",
            RegexOptions.None, TimeSpan.FromSeconds(1.5));
      }
      // If we timeout when replacing invalid characters,
      // we should return Empty.
      catch (RegexMatchTimeoutException)
      {
         return string.Empty;
      }
   }
}