// TCDev.de 2022/03/17
// TCDev.Utilities.IsValidEmail.cs
// https://www.github.com/deejaytc/dotnet-utils

using System.Text.RegularExpressions;

namespace TCDev.Utilities.StringExtensions;

public static class StringExtension
{
   public static bool IsValidEmail(this string text)
   {
      const string pattern = @"^(?!\.)(""([^""\r\\]|\\[""\r\\])*""|"
                             + @"([-a-z0-9!#$%&'*+/=?^_`{|}~]|(?<!\.)\.)*)(?<!\.)"
                             + @"@[a-z0-9][\w\.-]*[a-z0-9]\.[a-z][a-z\.]*[a-z]$";

      var regex = new Regex(pattern, RegexOptions.IgnoreCase);
      return regex.IsMatch(text);
   }
}
