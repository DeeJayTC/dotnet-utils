// TCDev.de 2022/03/17
// TCDev.Utilities.DictionaryExtensions.cs
// https://www.github.com/deejaytc/dotnet-utils

using System;
using System.Collections.Generic;
using System.Linq;

namespace TCDev.Utilities.Collections;

public static class DictionaryExtensions
{
   /// <summary>
   ///    An alternative way to get a value from a dictionary.
   ///    The return value is a Lazy object containing the value if the value exists in the dictionary.
   ///    If it doesn't exist, the Lazy object isn't initialized.
   ///    Therefore, you can use the .IsValueCreated property of Lazy to determine if the object has a value.
   ///    In addition, if the dictionary did not have the key, .Value property of Lazy will be return the default value of the
   ///    type.
   ///    such as null for string and 0 for int.
   ///    In some cases, simply using the .Value directly of or testing for null may be preferable to testing IsValueCreated
   ///    Source: www.extensionmethod.net
   /// </summary>
   public static Lazy<TValue> GetValue<TValue, TKey>(this IDictionary<TKey, TValue> dictionary, TKey key)
   {
      if (!dictionary.TryGetValue(key, out var retVal)) return new Lazy<TValue>(() => default);
      var lazy = new Lazy<TValue>(() => retVal);
      return lazy;
   }

   /// <summary>
   ///    Increment a dictionary with integers at the given index
   /// </summary>
   /// <typeparam name="T"></typeparam>
   /// <param name="dictionary"></param>
   /// <param name="index"></param>
   public static void IncrementAt<T>(this Dictionary<T, int> dictionary, T index)
   {
      dictionary.TryGetValue(index, out var count);

      dictionary[index] = ++count;
   }

   /// <summary>
   ///    Uses the specified functions to add a key/value pair to the IDictionary&lt;TKey, TValue&gt; if the key does
   ///    not already exist, or to update a key/value pair in the IDictionary&lt;TKey, TValue&gt;> if the key already
   ///    exists.
   /// </summary>
   /// <typeparam name="TKey">Type of the key.</typeparam>
   /// <typeparam name="TValue">Type of the value.</typeparam>
   /// <param name="this">The @this to act on.</param>
   /// <param name="key">The key to be added or whose value should be updated.</param>
   /// <param name="addValue">The value to be added for an absent key.</param>
   /// <param name="updateValueFactory">
   ///    The function used to generate a new value for an existing key based on the key's
   ///    existing value.
   /// </param>
   /// <returns>
   ///    The new value for the key. This will be either be addValue (if the key was absent) or the result of
   ///    updateValueFactory (if the key was present).
   /// </returns>
   public static TValue AddOrUpdate<TKey, TValue>(this IDictionary<TKey, TValue> @this, TKey key, TValue addValue, Func<TKey, TValue, TValue> updateValueFactory)
   {
      if (!@this.ContainsKey(key))
         @this.Add(new KeyValuePair<TKey, TValue>(key, addValue));
      else
         @this[key] = updateValueFactory(key, @this[key]);

      return @this[key];
   }

   /// <summary>
   ///    Uses the specified functions to add a key/value pair to the IDictionary&lt;TKey, TValue&gt; if the key does
   ///    not already exist, or to update a key/value pair in the IDictionary&lt;TKey, TValue&gt;> if the key already
   ///    exists.
   /// </summary>
   /// <typeparam name="TKey">Type of the key.</typeparam>
   /// <typeparam name="TValue">Type of the value.</typeparam>
   /// <param name="this">The @this to act on.</param>
   /// <param name="key">The key to be added or whose value should be updated.</param>
   /// <param name="addValueFactory">The function used to generate a value for an absent key.</param>
   /// <param name="updateValueFactory">
   ///    The function used to generate a new value for an existing key based on the key's
   ///    existing value.
   /// </param>
   /// <returns>
   ///    The new value for the key. This will be either be the result of addValueFactory (if the key was absent) or
   ///    the result of updateValueFactory (if the key was present).
   /// </returns>
   public static TValue AddOrUpdate<TKey, TValue>(this IDictionary<TKey, TValue> @this, TKey key, Func<TKey, TValue> addValueFactory, Func<TKey, TValue, TValue> updateValueFactory)
   {
      if (!@this.ContainsKey(key))
         @this.Add(new KeyValuePair<TKey, TValue>(key, addValueFactory(key)));
      else
         @this[key] = updateValueFactory(key, @this[key]);

      return @this[key];
   }

   /// <summary>
   ///    Uses the specified functions to add a key/value pair to the IDictionary&lt;TKey, TValue&gt; if the key does
   ///    not already exist, or to update a key/value pair in the IDictionary&lt;TKey, TValue&gt;> if the key already
   ///    exists.
   /// </summary>
   /// <typeparam name="TKey">Type of the key.</typeparam>
   /// <typeparam name="TValue">Type of the value.</typeparam>
   /// <param name="this">The @this to act on.</param>
   /// <param name="key">The key to be added or whose value should be updated.</param>
   /// <param name="value">The value to be added or updated.</param>
   /// <returns>The new value for the key.</returns>
   public static TValue AddOrUpdate<TKey, TValue>(this IDictionary<TKey, TValue> @this, TKey key, TValue value)
   {
      if (!@this.ContainsKey(key))
         @this.Add(new KeyValuePair<TKey, TValue>(key, value));
      else
         @this[key] = value;

      return @this[key];
   }


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