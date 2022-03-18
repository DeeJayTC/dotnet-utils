// TCDev.de 2022/03/17
// TCDev.Utilities.RegistryHandler.cs
// https://www.github.com/deejaytc/dotnet-utils

using System;
using Microsoft.Win32;
using Newtonsoft.Json;
using TCDev.Utilities.Cryptography;

namespace TCDev.Utilities.Windows;

/// <summary>
///    Save and read data from the Windows Registry, encrypted
/// </summary>
public static class RegistryHandler
{
   private const string EncryptionCode = "<ENCRYPTIONCODE>";

   public static void SaveRegistryData(string data, string path, string entry)
   {
      var key = Registry.CurrentUser.OpenSubKey(path, true);
      if (key == null)
         Registry.CurrentUser.CreateSubKey(path, RegistryKeyPermissionCheck.ReadWriteSubTree, RegistryOptions.None);
      if (key != null)
      {
         var crypt = data.Encrypt(EncryptionCode);
         key.SetValue(entry, crypt);
      }
   }


   public static t ReadRegistryData<t>(string path, string entry)
   {
      try
      {
         var key = Registry.CurrentUser.OpenSubKey(path, true);
         if (key != null)
         {
            var serial = key.GetValue(entry) != null
               ? key.GetValue(entry)
                  .ToString()
               : "";
            if (!string.IsNullOrEmpty(serial))
            {
               var data = serial.Decrypt(EncryptionCode);
               return JsonConvert.DeserializeObject<t>(data);
            }
         }

         return default;
      }
      catch (Exception ex)
      {
         return default;
      }
   }


   public static void SaveRegistryDataNoEncryption(string data, string path, string entry)
   {
      var key = Registry.CurrentUser.OpenSubKey(path, true);
      if (key == null)
         Registry.CurrentUser.CreateSubKey(path, RegistryKeyPermissionCheck.ReadWriteSubTree, RegistryOptions.None);
      if (key != null) key.SetValue(entry, data);
   }


   public static void DeleteRegistryyData(string path, string entry)
   {
      var key = Registry.CurrentUser.OpenSubKey(path, true);
      if (key != null) key.DeleteValue(entry);
   }


   public static bool AddRegistryData(string path, string entry, string data)
   {
      try
      {
         var key = Registry.CurrentUser.OpenSubKey(path, true);
         if (key != null)
         {
            var serial = key.GetValue(entry) != null
               ? key.GetValue(entry)
                  .ToString()
               : "";
            if (string.IsNullOrEmpty(serial)) key.SetValue(entry, data);
         }

         return false;
      }
      catch (Exception)
      {
         return false;
      }
   }


   public static bool CheckRegistryData(string path, string entry)
   {
      try
      {
         var key = Registry.CurrentUser.OpenSubKey(path, true);
         if (key != null)
         {
            var serial = key.GetValue(entry) != null
               ? key.GetValue(entry)
                  .ToString()
               : "";
            if (!string.IsNullOrEmpty(serial)) return true;
         }

         return false;
      }
      catch (Exception)
      {
         return false;
      }
   }
}
