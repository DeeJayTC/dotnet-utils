// TCDev.de 2022/03/18
// TCDev.Utilities.NetCore.CorsConfig.cs
// https://www.github.com/deejaytc/dotnet-utils

namespace TCDev.Utilities.NetCore.StartupExtension;

public class CorsConfig
{
   public string Name { get; set; }
   public bool AllowAllOrigins { get; set; } = true;
   public bool AllowAllMethods { get; set; } = true;
   public string[] AllowedOrigins { get; set; } = {"localhost"};
   public string[] AllowedMethods { get; set; } = {"GET"};

   public bool AllowCredentials { get; set; } = true;
   public string[] ExposedHeaders { get; set; } = { };
}