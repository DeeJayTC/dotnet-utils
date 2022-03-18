// TCDev 2022/03/17
// Apache 2.0 License
// https://www.github.com/deejaytc/dotnet-utils

using System.Collections.Generic;

namespace TCDev.Utilities;

public static class Counties
{
   public static Dictionary<string, string> GetCounties(CountryCode country)
   {
      switch (country)
      {
         case CountryCode.CH:
            return new Dictionary<string, string>
            {
               {"CH-AG", "Aargau"}, {"CH-AI", "Appenzell Innerrhoden"}, {"CH-AR", "Appenzell Ausserrhoden"}, {"CH-BL", "Basel-Landschaft"}, {"CH-BS", "Basel-Stadt"}, {"CH-BE", "Bern"}, {"CH-FR", "Freiburg"}, {"CH-GE", "Genf"}, {"CH-GL", "Glarus"}, {"CH-GR", "Graubünden"}, {"CH-JU", "Jura"}, {"CH-LU", "Luzern"}, {"CH-NE", "Neuenburg"}, {"CH-NW", "Nidwalden"}, {"CH-OW", "Obwalden"}, {"CH-SG", "St. Gallen"}, {"CH-SH", "Schaffhausen"}, {"CH-SZ", "Schwyz"}, {"CH-SO", "Solothurn"}, {"CH-TG", "Thurgau"}, {"CH-TI", "Tessin"}, {"CH-UR", "Uri"}, {"CH-VS", "Wallis"}, {"CH-VD", "Waadt"}, {"CH-ZG", "Zug"}, {"CH-ZH", "Zürich"}
            };
         case CountryCode.DE:
            return new Dictionary<string, string>
            {
               {"DE-BW", "Baden-Württemberg"}, {"DE-BY", "Bayern"}, {"DE-BE", "Berlin"}, {"DE-BB", "Brandenburg"}, {"DE-HB", "Bremen"}, {"DE-HH", "Hamburg"}, {"DE-HE", "Hessen"}, {"DE-MV", "Mecklenburg-Vorpommern"}, {"DE-NI", "Niedersachsen"}, {"DE-NW", "Nordrhein-Westfalen"}, {"DE-RP", "Rheinland-Pfalz"}, {"DE-SL", "Saarland"}, {"DE-SN", "Sachsen"}, {"DE-ST", "Sachsen-Anhalt"}, {"DE-SH", "Schleswig-Holstein"}, {"DE-TH", "Thüringen"}
            };
         case CountryCode.AT:
            return new Dictionary<string, string>
            {
               {"AT-1", "Burgenland"}, //Burgenland
               {"AT-2", "Kärnten"}
               , //Carinthia
               {"AT-3", "Niederösterreich"}
               , //Lower Austria
               {"AT-4", "Oberösterreich"}
               , //Upper Austria
               {"AT-5", "Salzburg"}
               , //Salzburg
               {"AT-6", "Steiermark"}
               , //Styria
               {"AT-7", "Tirol"}
               , //Tyrol
               {"AT-8", "Vorarlberg"}
               , //Vorarlberg
               {"AT-9", "Wien"} //Vienna
            };
         default:
            return new Dictionary<string, string>();
      }
   }
}