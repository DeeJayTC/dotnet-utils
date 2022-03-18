// TCDev.de 2022/03/18
// TCDev.Utilities.NetCore.SettingsHelper.cs
// https://www.github.com/deejaytc/dotnet-utils

using Microsoft.Extensions.Configuration;

namespace TCDev.Utilities.NetCore.StartupExtension;

public class AppSettings
{
   private static AppSettings _instance;
   private static readonly object ObjLocked = new();

   public static AppSettings Instance
   {
      get
      {
         if (null == _instance)
            lock (ObjLocked)
            {
               if (null == _instance)
                  _instance = new AppSettings();
            }

         return _instance;
      }
   }

   public IConfiguration _configuration;

   public void SetConfiguration(IConfiguration configuration)
   {
      this._configuration = configuration;
   }

   public string GetConnection(string key, string defaultValue = "")
   {
      try
      {
         return this._configuration.GetConnectionString(key);
      }
      catch
      {
         return defaultValue;
      }
   }

   public T Get<T>(string key = null)
   {
      if (string.IsNullOrWhiteSpace(key))
         return this._configuration.Get<T>();
      return this._configuration.GetSection(key).Get<T>();
   }

   public T Get<T>(string key, T defaultValue)
   {
      if (this._configuration.GetSection(key) == null)
         return defaultValue;

      if (string.IsNullOrWhiteSpace(key))
         return this._configuration.Get<T>();
      return this._configuration.GetSection(key).Get<T>();
   }

   public static T GetObject<T>(string key = null)
   {
      if (string.IsNullOrWhiteSpace(key)) return Instance._configuration.Get<T>();

      var section = Instance._configuration.GetSection(key);
      return section.Get<T>();
   }

   public static T GetObject<T>(string key, T defaultValue)
   {
      if (Instance._configuration.GetSection(key) == null)
         return defaultValue;

      if (string.IsNullOrWhiteSpace(key))
         return Instance._configuration.Get<T>();
      return Instance._configuration.GetSection(key).Get<T>();
   }
}