// TCDev 2022/03/17
// Apache 2.0 License
// https://www.github.com/deejaytc/dotnet-utils

using System;
using System.Collections.Generic;
using System.Linq;

namespace TCDev.Utilities.Collections;

public static class DictionaryExtensions
{
   /// <summary>
   ///    Groups the elements of a sequence according to a specified firstKey selector function and rotates the unique values
   ///    from the secondKey selector function into multiple values in the output, and performs aggregations.
   /// </summary>
   /// <example>
   ///    l.Pivot(emp => emp.Department, emp2 => emp2.Function, lst => lst.Sum(emp => emp.Salary));
   /// </example>
   /// <typeparam name="TSource"></typeparam>
   /// <typeparam name="TFirstKey"></typeparam>
   /// <typeparam name="TSecondKey"></typeparam>
   /// <typeparam name="TValue"></typeparam>
   /// <param name="source"></param>
   /// <param name="firstKeySelector"></param>
   /// <param name="secondKeySelector"></param>
   /// <param name="aggregate"></param>
   /// <returns></returns>
   public static Dictionary<TFirstKey, Dictionary<TSecondKey, TValue>>
      Pivot<TSource, TFirstKey, TSecondKey, TValue>(this IEnumerable<TSource> source,
         Func<TSource, TFirstKey> firstKeySelector, Func<TSource, TSecondKey> secondKeySelector, Func<IEnumerable<TSource>, TValue> aggregate)
   {
      var retVal = new Dictionary<TFirstKey, Dictionary<TSecondKey, TValue>>();

      var l = source.ToLookup(firstKeySelector);
      foreach (var item in l)
      {
         var dict = new Dictionary<TSecondKey, TValue>();
         retVal.Add(item.Key, dict);
         var subdict = item.ToLookup(secondKeySelector);
         foreach (var subitem in subdict) dict.Add(subitem.Key, aggregate(subitem));
      }

      return retVal;
   }

   /// <summary>
   ///    Sort a dictionary by key or value
   /// </summary>
   /// <example>
   ///    dict.Sort(); // By Key ascending
   ///    dict.Sort(true,true); // By Value descending
   /// </example>
   /// <typeparam name="TKey"></typeparam>
   /// <typeparam name="TValue"></typeparam>
   /// <param name="dict"></param>
   /// <param name="byValue"></param>
   /// <param name="descending"></param>
#pragma warning disable CS8714
   public static void Sort<TKey, TValue>(this Dictionary<TKey, TValue> dict, bool byValue = false, bool descending = false)
   {
      Dictionary<TKey, TValue> temp;
      if (descending)
         temp = byValue
            ? dict.OrderByDescending(x => x.Value)
               .ToDictionary(x => x.Key, x => x.Value)
            : dict.OrderByDescending(x => x.Key)
               .ToDictionary(x => x.Key, x => x.Value);
      else
         temp = byValue
            ? dict.OrderBy(x => x.Value)
               .ToDictionary(x => x.Key, x => x.Value)
            : dict.OrderBy(x => x.Key).ToDictionary(x => x.Key, x => x.Value);
      dict.Clear();
      foreach (var pair in temp) dict.Add(pair.Key, pair.Value);
   }
#pragma warning restore CS8714
}