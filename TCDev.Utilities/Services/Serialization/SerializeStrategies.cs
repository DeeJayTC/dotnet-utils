// TCDev.de 2021/08/30
// TCDev.Utilities.SerializeStrategies.cs
// https://www.github.com/deejaytc/dotnet-utils

using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace TCDev.Utilities.Services.Serialization;

public static class SerializeStrategies
{
   public static JsonSerializerSettings CamelCaseStrategy =>
      new()
      {
         ContractResolver = new DefaultContractResolver
         {
            NamingStrategy = new CamelCaseNamingStrategy()
         },
         Formatting = Formatting.Indented
      };

   public static JsonSerializerSettings SnakeCaseStrategy =>
      new()
      {
         ContractResolver = new DefaultContractResolver
         {
            NamingStrategy = new SnakeCaseNamingStrategy()
         },
         Formatting = Formatting.Indented
      };
}
