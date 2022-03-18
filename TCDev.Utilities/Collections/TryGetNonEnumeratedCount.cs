// TCDev.de 2022/03/17
// TCDev.Utilities.TryGetNonEnumeratedCount.cs
// https://www.github.com/deejaytc/dotnet-utils

using System.Collections.Generic;

namespace TCDev.Utilities.Collections;

public static class IEnumerableExtension
{
   /// <summary>
   ///    Attempts to determine the number of elements in a sequence without forcing an enumeration. For those who are not yet
   ///    using .NET 6
   /// </summary>
   /// <typeparam name="T"></typeparam>
   /// <param name="enumerable"></param>
   /// <param name="count"></param>
   /// <returns></returns>
   public static bool TryGetNonEnumeratedCount<T>(this IEnumerable<T> enumerable, out int count)
   {
      count = 0;
      if (enumerable is ICollection<T> list)
      {
         count = list.Count;
         return true;
      }

      return false;
   }
}
