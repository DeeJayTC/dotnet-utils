// TCDev 2022/03/17
// Apache 2.0 License
// https://www.github.com/deejaytc/dotnet-utils

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;

namespace Tente.Utilities.Data;

public class ColumnMapping : Attribute
{
   public string FieldName { get; set; }

   private List<T> ToObjectList<T>(DataTable dtSource) where T : new()
   {
      var returnList = new List<T>();

      for (var i = 0; i <= dtSource.Rows.Count - 1; i++)
      {
         var obj = new T();
         for (var j = 0; j <= dtSource.Columns.Count - 1; j++)
         {
            var col = dtSource.Columns[j];

            foreach (var pi in obj.GetType().GetProperties())
            {
               var fieldName =
                  ((ColumnMapping) pi.GetCustomAttributes(typeof(ColumnMapping), false)[0]).FieldName;
               if (fieldName == col.ColumnName)
               {
                  pi.SetValue(obj, dtSource.Rows[i][j], null);
                  break;
               }
            }
         }

         returnList.Add(obj);
      }

      return returnList;
   }
}

public static class DataTableExtensions
{
   public static IList<T> ToList<T>(this DataTable table) where T : new()
   {
      IList<PropertyInfo> properties = typeof(T).GetProperties().ToList();
      IList<T> result = new List<T>();

      foreach (var row in table.Rows)
      {
         var item = CreateItemFromRow<T>((DataRow) row, properties);
         result.Add(item);
      }

      return result;
   }

   public static IList<T> ToList<T>(this DataTable table, Dictionary<string, string> mappings) where T : new()
   {
      IList<PropertyInfo> properties = typeof(T).GetProperties().ToList();
      IList<T> result = new List<T>();

      foreach (var row in table.Rows)
      {
         var item = CreateItemFromRow<T>((DataRow) row, properties, mappings);
         result.Add(item);
      }

      return result;
   }

   private static T CreateItemFromRow<T>(DataRow row, IEnumerable<PropertyInfo> properties, Dictionary<string, string> mappings) where T : new()
   {
      var item = new T();
      foreach (var property in properties)
      {
         if (mappings.ContainsKey(property.Name))
            property.SetValue(item, row[mappings[property.Name]], null);
      }

      return item;
   }
}

/// <summary>
///    extension methods that allow dynamic population of data objects through reflection
/// </summary>
public static class DynamicDataExtensions
{
   /// <summary>
   ///    populate the public properties of this object from a data-row;
   /// </summary>
   /// <param name="obj"></param>
   /// <param name="row"></param>
   public static void SetPropertiesFrom(this object obj, DataRow row)
   {
      // enumerate the public properties of the object:
      foreach (var property in obj.GetType().GetProperties())
      {
         // does the property name appear as a column in the table?
         if (row.Table.Columns.Contains(property.Name))
         {
            // get the data-column:
            var column = row.Table.Columns[property.Name];

            // get the value of the column from the row:
            var value = row[column];

            // set the value on the property:
            if (!(value is DBNull))
               property.SetValue(obj, Convert.ChangeType(value, property.PropertyType), null);
         }
      }
   }
}