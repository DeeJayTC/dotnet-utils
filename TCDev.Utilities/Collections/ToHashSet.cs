// TCDev.de 2022/03/18
// TCDev.Utilities.ToHashSet.cs
// https://www.github.com/deejaytc/dotnet-utils

using System.Collections.Generic;

namespace TCDev.Utilities.Collections;

public static class ToHashSetExtension
{
   public static HashSet<T> ToHashSet<T>(this IEnumerable<T> source)
   {
      return new HashSet<T>(source);
   }
}