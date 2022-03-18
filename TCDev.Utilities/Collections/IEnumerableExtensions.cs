// TCDev.de 2022/03/17
// TCDev.Utilities.IEnumerableExtensions.cs
// https://www.github.com/deejaytc/dotnet-utils

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TCDev.Utilities.Collections;

public static class IEnumerableExtensions
{
   /// <summary>
   ///    Iterate through any list and call an action delegate to each
   /// </summary>
   /// <typeparam name="T"></typeparam>
   /// <param name="items"></param>
   /// <param name="action"></param>
   public static void Each<T>(this IEnumerable<T> items, Action<T> action)
   {
      if (items == null) return;

      var cached = items;

      foreach (var item in cached)
         action(item);
   }

   /// <summary>
   ///    Shuffles any IEnumerable List
   /// </summary>
   /// <typeparam name="T"></typeparam>
   /// <param name="list"></param>
   /// <returns></returns>
   public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> list)
   {
      var r = new Random((int)DateTime.Now.Ticks);
      var shuffledList = list.Select(x => new
         {
            Number = r.Next(), Item = x
         })
         .OrderBy(x => x.Number)
         .Select(x => x.Item);
      return shuffledList.ToList();
   }

   /// <summary>
   ///    Flattens an <see cref="IEnumerable" /> of <see cref="String" /> objects to a single string, seperated by an optional
   ///    seperator and with optional head and tail.
   /// </summary>
   /// <param name="strings">The string objects to be flattened.</param>
   /// <param name="seperator">The seperator to be used between each string object.</param>
   /// <param name="head">The string to be used at the beginning of the flattened string. Defaulted to an empty string.</param>
   /// <param name="tail">The string to be used at the end of the flattened string. Defaulted to an empty string.</param>
   /// <returns>Single string containing all elements seperated by the given seperator, with optionally a head and/or tail.</returns>
   public static string Flatten(this IEnumerable<string> strings, string seperator, string head, string tail)
   {
      // If the collection is null, or if it contains zero elements,
      // then return an empty string.
      if (strings == null || strings.Count() == 0)
         return string.Empty;

      // Build the flattened string
      var flattenedString = new StringBuilder();

      flattenedString.Append(head);
      foreach (var s in strings)
         flattenedString.AppendFormat("{0}{1}", s, seperator); // Add each element with the given seperator.
      flattenedString.Remove(flattenedString.Length - seperator.Length, seperator.Length); // Remove the last seperator
      flattenedString.Append(tail);

      // Return the flattened string
      return flattenedString.ToString();
   }

   /// <summary>
   ///    Shortcut: Filter any IEnumerable and return a new filtered list
   /// </summary>
   /// <typeparam name="T"></typeparam>
   /// <param name="list"></param>
   /// <param name="filterParam"></param>
   /// <returns></returns>
   public static IEnumerable<T> Filter<T>(this IEnumerable<T> list, Func<T, bool> filterParam)
   {
      return list.Where(filterParam);
   }

   /// <summary>
   ///    Converts an IEnumerable to a HashSet
   /// </summary>
   /// <typeparam name="T">The IEnumerable type</typeparam>
   /// <param name="enumerable">The IEnumerable</param>
   /// <returns>A new HashSet</returns>
   public static HashSet<T> ToHashSet<T>(this IEnumerable<T> enumerable)
   {
      var hs = new HashSet<T>();
      foreach (var item in enumerable)
         hs.Add(item);
      return hs;
   }

   /// <summary>
   ///    Converts an enumeration of groupings into a Dictionary of those groupings.
   /// </summary>
   /// <typeparam name="TKey">Key type of the grouping and dictionary.</typeparam>
   /// <typeparam name="TValue">Element type of the grouping and dictionary list.</typeparam>
   /// <param name="groupings">The enumeration of groupings from a GroupBy() clause.</param>
   /// <returns>
   ///    A dictionary of groupings such that the key of the dictionary is TKey type and the value is List of TValue
   ///    type.
   /// </returns>
   public static Dictionary<TKey, List<TValue>> ToDictionary<TKey, TValue>(this IEnumerable<IGrouping<TKey, TValue>> groupings)
   {
      return groupings.ToDictionary(group => group.Key, group => group.ToList());
   }

   /// <summary>
   ///    Slice a list of a given type and return a new collection
   /// </summary>
   /// <typeparam name="T"></typeparam>
   /// <param name="collection"></param>
   /// <param name="start"></param>
   /// <param name="count"></param>
   /// <returns></returns>
   public static IEnumerable<T> Slice<T>(this IEnumerable<T> collection, int start, int count)
   {
      if (collection == null)
         throw new ArgumentNullException("collection");

      return collection.Skip(start)
         .Take(count);
   }

   /// <summary>
   ///    Order a list by the given sort expression
   ///    https://extensionmethod.net/csharp/ienumerable-t/orderby-string-sortexpression
   /// </summary>
   /// <example>list.OrderBy("Name desc");</example>
   /// <typeparam name="T"></typeparam>
   /// <param name="list"></param>
   /// <param name="sortExpression"></param>
   /// <returns></returns>
   public static IEnumerable<T> OrderBy<T>(this IEnumerable<T> list, string sortExpression)
   {
      sortExpression += "";
      var parts = sortExpression.Split(' ');
      var descending = false;
      var property = "";

      if (parts.Length > 0 && parts[0] != "")
      {
         property = parts[0];

         if (parts.Length > 1)
            descending = parts[1]
               .ToLower()
               .Contains("esc");

         var prop = typeof(T).GetProperty(property);

         if (prop == null) throw new Exception("No property '" + property + "' in + " + typeof(T).Name + "'");

         if (descending)
            return list.OrderByDescending(x => prop.GetValue(x, null));
         return list.OrderBy(x => prop.GetValue(x, null));
      }

      return list;
   }

   /// <summary>
   ///    https://extensionmethod.net/csharp/ienumerable-t/whereif
   ///    When building a LINQ query, you may need to involve optional filtering criteria.
   ///    Avoids if statements when building predicates & lambdas for a query. Useful when you don't know at compile time
   ///    whether a filter should apply
   /// </summary>
   /// <example>
   ///    .WhereIf(boolean, c=>c.AcctBalance < 5000).ToList()
   /// </example>
   /// <typeparam name="TSource"></typeparam>
   /// <param name="source"></param>
   /// <param name="condition"></param>
   /// <param name="predicate"></param>
   /// <returns></returns>
   public static IEnumerable<TSource> WhereIf<TSource>(this IEnumerable<TSource> source, bool condition, Func<TSource, bool> predicate)
   {
      if (condition)
         return source.Where(predicate);
      return source;
   }

   /// <summary>
   ///    https://extensionmethod.net/csharp/ienumerable-t/whereif
   ///    When building a LINQ query, you may need to involve optional filtering criteria.
   ///    Avoids if statements when building predicates & lambdas for a query. Useful when you don't know at compile time
   ///    whether a filter should apply
   /// </summary>
   /// <example>
   ///    .WhereIf(boolean, c=>c.AcctBalance < 5000).ToList()
   /// </example>
   /// <typeparam name="TSource"></typeparam>
   /// <param name="source"></param>
   /// <param name="condition"></param>
   /// <param name="predicate"></param>
   /// <returns></returns>
   public static IEnumerable<TSource> WhereIf<TSource>(this IEnumerable<TSource> source, bool condition, Func<TSource, int, bool> predicate)
   {
      if (condition)
         return source.Where(predicate);
      return source;
   }

   /// <summary>
   ///    Foreach Shortcut and create a new list
   /// </summary>
   /// <typeparam name="T"></typeparam>
   /// <param name="array"></param>
   /// <param name="act"></param>
   /// <returns></returns>
   public static IEnumerable<T> ForEach<T>(this IEnumerable<T> array, Action<T> act)
   {
      foreach (var i in array)
         act(i);
      return array;
   }

   /// <summary>
   ///    Foreach Shortcut and create a new list
   /// </summary>
   /// <typeparam name="T"></typeparam>
   /// <param name="array"></param>
   /// <param name="act"></param>
   /// <returns></returns>
   public static IEnumerable<RT> ForEach<T, RT>(this IEnumerable<T> array, Func<T, RT> func)
   {
      var list = new List<RT>();
      foreach (var i in array)
      {
         var obj = func(i);
         if (obj != null)
            list.Add(obj);
      }

      return list;
   }
}
