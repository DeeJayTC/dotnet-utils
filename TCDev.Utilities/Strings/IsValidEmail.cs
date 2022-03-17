// TCDev 2022/03/17
// Apache 2.0 License
// https://www.github.com/deejaytc/dotnet-utils

using System.Text.RegularExpressions;

namespace TeamWorkNet.Extensions.String;

public static partial class StringExtension
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