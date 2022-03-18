// TCDev.de 2022/03/18
// TCDev.Utilities.VSTO.OutlookObjectExtensions.cs
// https://www.github.com/deejaytc/dotnet-utils

using System.Runtime.InteropServices;
using Microsoft.Office.Interop.Outlook;

namespace TCDev.Utilities.VSTO;

/// <summary>
///    helps saving custom properties on VSTO Items in Outlook
/// </summary>
public static class OutlookObjectExtensions
{
   public static void SetProperty(this NoteItem note, string propertyName, object propertyValue, OlUserPropertyType propertyType)
   {
      ItemProperties itemProperties = null;
      ItemProperty itemProperty = null;

      try
      {
         itemProperties = note.ItemProperties;
         itemProperty = itemProperties[propertyName];
         if (itemProperty != null)
         {
            itemProperty.Value = propertyValue;
         }
         else
         {
            itemProperty = itemProperties.Add(propertyName, propertyType, true);
            itemProperty.Value = propertyValue;
         }
      }
      finally
      {
         if (itemProperty != null) Marshal.ReleaseComObject(itemProperty);
         if (itemProperties != null) Marshal.ReleaseComObject(itemProperties);
      }
   }

   public static void SetProperty(this JournalItem journal, string propertyName, object propertyValue, OlUserPropertyType propertyType)
   {
      ItemProperties itemProperties = null;
      ItemProperty itemProperty = null;

      try
      {
         itemProperties = journal.ItemProperties;
         itemProperty = itemProperties[propertyName];
         if (itemProperty != null)
         {
            itemProperty.Value = propertyValue;
         }
         else
         {
            itemProperty = itemProperties.Add(propertyName, propertyType, true);
            itemProperty.Value = propertyValue;
         }
      }
      finally
      {
         if (itemProperty != null) Marshal.ReleaseComObject(itemProperty);
         if (itemProperties != null) Marshal.ReleaseComObject(itemProperties);
      }
   }

   public static void SetProperty(this TaskItem task, string propertyName, object propertyValue, OlUserPropertyType propertyType)
   {
      ItemProperties itemProperties = null;
      ItemProperty itemProperty = null;

      try
      {
         itemProperties = task.ItemProperties;
         itemProperty = itemProperties[propertyName];
         if (itemProperty != null)
         {
            itemProperty.Value = propertyValue;
         }
         else
         {
            itemProperty = itemProperties.Add(propertyName, propertyType, true);
            itemProperty.Value = propertyValue;
         }
      }
      finally
      {
         if (itemProperty != null) Marshal.ReleaseComObject(itemProperty);
         if (itemProperties != null) Marshal.ReleaseComObject(itemProperties);
      }
   }


   public static object GetPropertyValue(this JournalItem journal, string propertyName)
   {
      ItemProperties itemProperties = null;
      ItemProperty itemProperty = null;
      object returnValue = null;

      try
      {
         itemProperties = journal.ItemProperties;
         itemProperty = itemProperties[propertyName];
         if (itemProperty != null)
         {
            returnValue = itemProperty.Value;
            return returnValue;
         }

         return returnValue;
      }
      finally
      {
         if (itemProperty != null) Marshal.ReleaseComObject(itemProperty);
         if (itemProperties != null) Marshal.ReleaseComObject(itemProperties);
      }
   }


   public static object GetPropertyValue(this TaskItem task, string propertyName)
   {
      ItemProperties itemProperties = null;
      ItemProperty itemProperty = null;
      object returnValue = null;

      try
      {
         itemProperties = task.ItemProperties;
         itemProperty = itemProperties[propertyName];
         if (itemProperty != null)
         {
            returnValue = itemProperty.Value;
            return returnValue;
         }

         return returnValue;
      }
      finally
      {
         if (itemProperty != null) Marshal.ReleaseComObject(itemProperty);
         if (itemProperties != null) Marshal.ReleaseComObject(itemProperties);
      }
   }

   public static object GetPropertyValue(this NoteItem task, string propertyName)
   {
      ItemProperties itemProperties = null;
      ItemProperty itemProperty = null;
      object returnValue = null;

      try
      {
         itemProperties = task.ItemProperties;
         itemProperty = itemProperties[propertyName];
         if (itemProperty != null)
         {
            returnValue = itemProperty.Value;
            return returnValue;
         }

         return returnValue;
      }
      finally
      {
         if (itemProperty != null) Marshal.ReleaseComObject(itemProperty);
         if (itemProperties != null) Marshal.ReleaseComObject(itemProperties);
      }
   }

   public static void SetProperty(this ContactItem contact, string propertyName, object propertyValue, OlUserPropertyType propertyType)
   {
      ItemProperties itemProperties = null;
      ItemProperty itemProperty = null;

      try
      {
         itemProperties = contact.ItemProperties;
         itemProperty = itemProperties[propertyName];
         if (itemProperty != null)
         {
            itemProperty.Value = propertyValue;
         }
         else
         {
            itemProperty = itemProperties.Add(propertyName, propertyType, true);
            itemProperty.Value = propertyValue;
         }
      }
      finally
      {
         if (itemProperty != null) Marshal.ReleaseComObject(itemProperty);
         if (itemProperties != null) Marshal.ReleaseComObject(itemProperties);
      }
   }

   public static object GetPropertyValue(this ContactItem contact, string propertyName)
   {
      ItemProperties itemProperties = null;
      ItemProperty itemProperty = null;
      object returnValue = null;

      try
      {
         itemProperties = contact.ItemProperties;
         itemProperty = itemProperties[propertyName];
         if (itemProperty != null)
         {
            returnValue = itemProperty.Value;
            return returnValue;
         }

         return returnValue;
      }
      finally
      {
         if (itemProperty != null) Marshal.ReleaseComObject(itemProperty);
         if (itemProperties != null) Marshal.ReleaseComObject(itemProperties);
      }
   }

   public static void SetProperty(this AppointmentItem appointment, string propertyName, object propertyValue, OlUserPropertyType propertyType)
   {
      ItemProperties itemProperties = null;
      ItemProperty itemProperty = null;

      try
      {
         itemProperties = appointment.ItemProperties;
         itemProperty = itemProperties[propertyName];
         if (itemProperty != null)
         {
            itemProperty.Value = propertyValue;
         }
         else
         {
            itemProperty = itemProperties.Add(propertyName, propertyType, true);
            itemProperty.Value = propertyValue;
         }
      }
      finally
      {
         if (itemProperty != null) Marshal.ReleaseComObject(itemProperty);
         if (itemProperties != null) Marshal.ReleaseComObject(itemProperties);
      }
   }

   public static object GetPropertyValue(this AppointmentItem appointment, string propertyName)
   {
      ItemProperties itemProperties = null;
      ItemProperty itemProperty = null;
      object returnValue = null;

      try
      {
         itemProperties = appointment.ItemProperties;
         itemProperty = itemProperties[propertyName];
         if (itemProperty != null)
         {
            returnValue = itemProperty.Value;
            return returnValue;
         }

         return returnValue;
      }
      finally
      {
         if (itemProperty != null) Marshal.ReleaseComObject(itemProperty);
         if (itemProperties != null) Marshal.ReleaseComObject(itemProperties);
      }
   }
}