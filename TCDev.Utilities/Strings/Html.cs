// TCDev 2022/03/17
// Apache 2.0 License
// https://www.github.com/deejaytc/dotnet-utils

using System.Text.RegularExpressions;

namespace TCDev.Utilities.Strings;

public static class Html
{
   /// <summary>
   ///    Removes most common Html Tags from the given string
   /// </summary>
   /// <param name="t"></param>
   /// <returns></returns>
   public static string RemoveHtml(this string t)
   {
      var strippedBody = Regex.Replace(t, "<[/]?span[^>]*?>", " ");
      strippedBody = Regex.Replace(strippedBody, "<[/]?br[^>]*?>", "    ");
      strippedBody = Regex.Replace(strippedBody, "<[/]?div[^>]*?>", "  ");
      strippedBody = Regex.Replace(strippedBody, "<strong[^>]*?>", "**");
      strippedBody = Regex.Replace(strippedBody, "<[/]?strong[^>]*?>", "**");
      strippedBody = Regex.Replace(strippedBody, "<[/]?ul[^>]*?>", "");

      // Replace html lists
      strippedBody = Regex.Replace(strippedBody, "<li[^>]*?>", "- ");
      strippedBody = Regex.Replace(strippedBody, "<[/]?li[^>]*?>", "-  ");

      // Deal with hyperlinks
      Regex.Replace(strippedBody, "<a href=\"(.+?)\"\\s*>(.+?)</a>", m => $"[{m.Groups[2].Value}]({m.Groups[1].Value})");

      //last replace all left over html tags with nothing
      strippedBody = Regex.Replace(strippedBody, @"<\s*([^ >]+)[^>]*>.*?<\s*/\s*\1\s*>", "");
      //last replace all left over html tags with nothing
      strippedBody = Regex.Replace(strippedBody, @"&nbsp;", "");
      return strippedBody;
   }
}