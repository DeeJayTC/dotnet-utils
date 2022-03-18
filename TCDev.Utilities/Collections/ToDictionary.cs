// TCDev.de 2022/03/18
// TCDev.Utilities.ToDictionary.cs
// https://www.github.com/deejaytc/dotnet-utils

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace TCDev.Utilities.Conversion;

/// <summary>
///    Methods converting something to a dictionary type
/// </summary>
public static class ToDictionaryExtensions
{
   /// <summary>
   ///    Convert the object properties to a dictionary
   /// </summary>
   /// <param name="source"></param>
   /// <returns></returns>
   public static IDictionary<string, object> ToDictionary(this object source)
   {
      return source.ToDictionary<object>();
   }

   /// <summary>
   ///    Converts the object properties to a dictionary
   /// </summary>
   /// <typeparam name="T"></typeparam>
   /// <param name="source"></param>
   /// <returns></returns>
   public static IDictionary<string, T> ToDictionary<T>(this object source)
   {
      if (source == null)
         ThrowExceptionWhenSourceArgumentIsNull();

      var dictionary = new Dictionary<string, T>();
      foreach (PropertyDescriptor property in TypeDescriptor.GetProperties(source))
         AddPropertyToDictionary(property, source, dictionary);

      return dictionary;
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


   private static void AddPropertyToDictionary<T>(PropertyDescriptor property, object source, Dictionary<string, T> dictionary)
   {
      var value = property.GetValue(source);
      if (IsOfType<T>(value))
      {
         dictionary.Add(property.Name, (T) value);
      }
      else
      {
         var newValue = (T) Convert.ChangeType(value, typeof(T));
         dictionary.Add(property.Name, newValue);
      }
   }

   private static bool IsOfType<T>(object value)
   {
      return value is T;
   }

   private static void ThrowExceptionWhenSourceArgumentIsNull()
   {
      throw new ArgumentNullException("source", "Unable to convert object to a dictionary. The source object is null.");
   }
}