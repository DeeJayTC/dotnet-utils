// TCDev.de 2021/08/30
// TCDev.Utilities.GenericSerializer.cs
// https://www.github.com/deejaytc/dotnet-utils

using Newtonsoft.Json;

namespace TCDev.Utilities.Services.Serialization;

/// <summary>
///    Generic Serialization
/// </summary>
public static class GenericSerializer
{
   public static string ToJson(this object data, JsonSerializerSettings settings)
   {
      try
      {
         return JsonConvert.SerializeObject(data, (Formatting)System.Xml.Formatting.Indented, settings);
      }
      catch (JsonSerializationException ex)
      {
         throw new JsonSerializationException("Error converting object to string", ex);
      }
   }

   public static T ToObject<T>(this string data, JsonSerializerSettings settings)
   {
      try
      {
         return JsonConvert.DeserializeObject<T>(data, settings);
      }
      catch (JsonSerializationException ex)
      {
         throw new JsonSerializationException("Error converting string to object", ex);
      }
   }
}
