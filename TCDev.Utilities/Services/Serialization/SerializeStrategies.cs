// TCDev 2022/03/15
// Apache 2.0 License
// https://www.github.com/deejaytc/dotnet-utils

using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace TCDev.Utilities.Services.Serialization
{
   public static class SerializeStrategies
   {
      public static JsonSerializerSettings CamelCaseStrategy =>
         new()
         {
            ContractResolver = new DefaultContractResolver {NamingStrategy = new CamelCaseNamingStrategy()}, Formatting = Formatting.Indented
         };

      public static JsonSerializerSettings SnakeCaseStrategy =>
         new()
         {
            ContractResolver = new DefaultContractResolver {NamingStrategy = new SnakeCaseNamingStrategy()}, Formatting = Formatting.Indented
         };
   }
}