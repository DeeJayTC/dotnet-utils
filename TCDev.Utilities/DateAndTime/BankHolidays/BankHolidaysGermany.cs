// TCDev.de 2021/08/30
// TCDev.Utilities.BankHolidaysGermany.cs
// https://www.github.com/deejaytc/dotnet-utils

using System;
using System.Collections.Generic;
using System.Linq;
using TCDev.Utilities.DateAndTime.BankHolidays.Base;

namespace TCDev.Utilities.DateAndTime.BankHolidays;

public class GermanyProvider : IPublicHolidayProvider,
   ICountyProvider
{
   private readonly ICatholicProvider _catholicProvider;

   /// <summary>
   ///    GermanyProvider
   /// </summary>
   /// <param name="catholicProvider"></param>
   public GermanyProvider(ICatholicProvider catholicProvider)
   {
      this._catholicProvider = catholicProvider;
   }

   /// <inheritdoc />
   public IDictionary<string, string> GetCounties()
   {
      return Counties.GetCounties(CountryCode.DE);
   }

   /// <inheritdoc />
   public IEnumerable<PublicHoliday> Get(int year)
   {
      var countryCode = CountryCode.DE;
      var easterSunday = this._catholicProvider.EasterSunday(year);

      var items = new List<PublicHoliday>
      {
         new(year, 1, 1, "Neujahr", "New Year's Day", countryCode, 1967),
         new(year, 1, 6, "Heilige Drei Könige", "Epiphany", countryCode, 1967, new[]
         {
            "DE-BW",
            "DE-BY",
            "DE-ST"
         }),
         new(year, 3, 8, "Internationaler Frauentag", "International Women's Day", countryCode, 2019, new[]
         {
            "DE-BE"
         }),
         new(easterSunday.AddDays(-2), "Karfreitag", "Good Friday", countryCode),
         new(easterSunday.AddDays(1), "Ostermontag", "Easter Monday", countryCode, 1642),
         new(year, 5, 1, "Tag der Arbeit", "Labour Day", countryCode),
         new(easterSunday.AddDays(39), "Christi Himmelfahrt", "Ascension Day", countryCode),
         new(easterSunday.AddDays(50), "Pfingstmontag", "Whit Monday", countryCode),
         new(easterSunday.AddDays(60), "Fronleichnam", "Corpus Christi", countryCode, null, new[]
         {
            "DE-BW",
            "DE-BY",
            "DE-HE",
            "DE-NW",
            "DE-RP",
            "DE-SL"
         }),
         new(year, 8, 15, "Mariä Himmelfahrt", "Assumption Day", countryCode, null, new[]
         {
            "DE-SL"
         }),
         new(year, 9, 20, "Weltkindertag", "World Children's Day", countryCode, 2019, new[]
         {
            "DE-TH"
         }),
         new(year, 10, 3, "Tag der Deutschen Einheit", "German Unity Day", countryCode),
         new(year, 11, 1, "Allerheiligen", "All Saints' Day", countryCode, null, new[]
         {
            "DE-BW",
            "DE-BY",
            "DE-NW",
            "DE-RP",
            "DE-SL"
         }),
         new(year, 12, 25, "Erster Weihnachtstag", "Christmas Day", countryCode),
         new(year, 12, 26, "Zweiter Weihnachtstag", "St. Stephen's Day", countryCode)
      };

      var prayerDay = GetPrayerDay(year, countryCode);
      if (prayerDay != null) items.Add(prayerDay);

      var liberationDay = GetLiberationDay(year, countryCode);
      if (liberationDay != null) items.Add(liberationDay);

      items.Add(GetReformationDay(year, CountryCode.DE));

      return items.OrderBy(o => o.Date);
   }

   /// <inheritdoc />
   public IEnumerable<string> GetSources()
   {
      return new[]
      {
         "https://de.wikipedia.org/wiki/Gesetzliche_Feiertage_in_Deutschland"
      };
   }

   private PublicHoliday GetReformationDay(int year, CountryCode countryCode)
   {
      const string localName = "Reformationstag";
      const string englishName = "Reformation Day";

      if (year == 2017)
         //In commemoration of the 500th anniversary of the beginning of the Reformation, it was unique as a whole German holiday
         return new PublicHoliday(year, 10, 31, localName, englishName, countryCode);

      var counties = new List<string>
      {
         "DE-BB",
         "DE-MV",
         "DE-SN",
         "DE-ST",
         "DE-TH"
      };

      if (year >= 2018)
         counties.AddRange(new[]
         {
            "DE-HB",
            "DE-HH",
            "DE-NI",
            "DE-SH"
         });

      return new PublicHoliday(year, 10, 31, localName, englishName, countryCode, null, counties.ToArray());
   }

   private PublicHoliday GetPrayerDay(int year, CountryCode countryCode)
   {
      var dayOfPrayer = this._catholicProvider.AdventSunday(year)
         .AddDays(-11);
      var localName = "Buß- und Bettag";
      var englishName = "Repentance and Prayer Day";

      if (year is >= 1934 and < 1939)
         return new PublicHoliday(dayOfPrayer, localName, englishName, countryCode);

      if (year is >= 1945 and <= 1980)
         return new PublicHoliday(dayOfPrayer, localName, englishName, countryCode, null, new[]
         {
            "DE-BW",
            "DE-BE",
            "DE-HB",
            "DE-HH",
            "DE-HE",
            "DE-NI",
            "DE-NW",
            "DE-RP",
            "DE-SL",
            "DE-SH"
         });

      if (year is >= 1981 and <= 1989)
         return new PublicHoliday(dayOfPrayer, localName, englishName, countryCode, null, new[]
         {
            "DE-BW",
            "DE-BY",
            "DE-BE",
            "DE-HB",
            "DE-HH",
            "DE-HE",
            "DE-NI",
            "DE-NW",
            "DE-RP",
            "DE-SL",
            "DE-SH"
         });

      if (year is >= 1990 and <= 1994)
         return new PublicHoliday(dayOfPrayer, localName, englishName, countryCode);

      if (year >= 1995)
         return new PublicHoliday(dayOfPrayer, localName, englishName, countryCode, null, new[]
         {
            "DE-SN"
         });

      return null;
   }

   private static PublicHoliday GetLiberationDay(int year, CountryCode countryCode)
   {
      if (year == 2020)
         return new PublicHoliday(new DateTime(2020, 5, 8), "Tag der Befreiung", "Liberation Day", countryCode, null, new[]
         {
            "DE-BE"
         });

      return null;
   }
}
