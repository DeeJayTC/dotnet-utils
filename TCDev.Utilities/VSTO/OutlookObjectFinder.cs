// TCDev 2022/03/18
// Apache 2.0 License
// https://www.github.com/deejaytc/dotnet-utils

using System;
using System.Reflection;

namespace TCDev.Office.Core;

/// <summary>
///    Find a specific object in VSTO by ID and Folder
/// </summary>
public static class OutlookObjectFinder
{
   public static T FindExistingItem<T>(MAPIFolder folder, object searchObjectWithID, string idPropertyName)
   {
      var assembly = Assembly.GetExecutingAssembly();
      var objID = searchObjectWithID.GetType().GetProperty(idPropertyName).GetValue(searchObjectWithID);
      foreach (var item in folder.Items)
      {
         try
         {
            var oTask = (T) item;

            if (typeof(T).Name == typeof(ContactItem).Name)
            {
               var element = oTask as ContactItem;
               var id = element.GetPropertyValue("fb-id");
               if (objID != null && id.ToString() == objID.ToString()) return oTask;
            }
            else if (typeof(T).Name == typeof(AppointmentItem).Name)
            {
               var element = oTask as AppointmentItem;
               var id = element.GetPropertyValue("fb-id");
               if (objID != null && id.ToString() == objID.ToString()) return oTask;
            }
            else if (typeof(T).Name == typeof(TaskItem).Name)
            {
               var element = oTask as TaskItem;
               var id = element.GetPropertyValue("fb-id");
               if (objID != null && id.ToString() == objID.ToString()) return oTask;
            }
            else if (typeof(T).Name == typeof(JournalItem).Name)
            {
               var element = oTask as ContactItem;
               var id = element.GetPropertyValue("fb-id");
               if (objID != null && id.ToString() == objID.ToString()) return oTask;
            }

            if (typeof(T).Name == typeof(NoteItem).Name)
            {
               var element = oTask as NoteItem;
               var id = element.GetPropertyValue("fb-id");
               if (objID != null && id.ToString() == objID.ToString()) return oTask;
            }
         }
         catch (SystemException ex)
         {
         }
      }

      return default;
   }
}