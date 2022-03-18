// TCDev.de 2022/03/17
// TCDev.Utilities.MemoryEnumerableCache.cs
// https://www.github.com/deejaytc/dotnet-utils

using System;
using System.Collections.Generic;

namespace TCDev.Utilities.Collections;

public static class IEnumerableCached
{
   public static IEnumerable<T> Cache<T>(this IEnumerable<T> source)
   {
      return CacheHelper(source.GetEnumerator());
   }

   private static IEnumerable<T> CacheHelper<T>(IEnumerator<T> source)
   {
      var isEmpty = new Lazy<bool>(() => !source.MoveNext());
      var head = new Lazy<T>(() => source.Current);
      var tail = new Lazy<IEnumerable<T>>(() => CacheHelper(source));

      return CacheHelper(isEmpty, head, tail);
   }

   private static IEnumerable<T> CacheHelper<T>(
      Lazy<bool> isEmpty,
      Lazy<T> head,
      Lazy<IEnumerable<T>> tail)
   {
      if (isEmpty.Value)
         yield break;

      yield return head.Value;
      foreach (var value in tail.Value)
         yield return value;
   }
}
