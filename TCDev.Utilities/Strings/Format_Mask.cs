// TCDev.de 2022/03/17
// TCDev.Utilities.Format_Mask.cs
// https://www.github.com/deejaytc/dotnet-utils

namespace TCDev.Utilities.StringExtensions;

public static class StringExtension
{
   /// <summary>
   ///    Formats the string according to the specified mask
   /// </summary>
   /// <param name="input">The input string.</param>
   /// <param name="mask">The mask for formatting. Like "A##-##-T-###Z"</param>
   /// <returns>The formatted string</returns>
   public static string FormatWithMask(this string input, string mask)
   {
      if (string.IsNullOrEmpty(input)) return input;
      var output = string.Empty;
      var index = 0;
      foreach (var m in mask)
      {
         if (m == '#')
         {
            if (index < input.Length)
            {
               output += input[index];
               index++;
            }
         }
         else
         {
            output += m;
         }
      }

      return output;
   }
}
