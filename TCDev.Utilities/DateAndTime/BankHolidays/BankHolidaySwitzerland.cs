﻿using TCDev.Utilities.DateAndTime.BankHolidays.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCDev.Utilities.DateAndTime.BankHolidays
{
    /// <summary>
    /// Switzerland
    /// </summary>
    public class SwitzerlandProvider : IPublicHolidayProvider, ICountyProvider
    {
        private readonly ICatholicProvider _catholicProvider;

        /// <summary>
        /// SwitzerlandProvider
        /// </summary>
        /// <param name="catholicProvider"></param>
        public SwitzerlandProvider(ICatholicProvider catholicProvider)
        {
            this._catholicProvider = catholicProvider;
        }

        ///<inheritdoc/>
        public IDictionary<string, string> GetCounties()
        {
            return Counties.GetCounties(CountryCode.CH);
        }
        ///<inheritdoc/>
        public IEnumerable<PublicHoliday> Get(int year)
        {
            var countryCode = CountryCode.CH;
            var easterSunday = this._catholicProvider.EasterSunday(year);

            var firstSundayOfSeptember = DateUtilities.FindDay(year, 9, DayOfWeek.Sunday, 1);
            var thirdMondayOfSeptember = DateUtilities.FindDay(year, 9, DayOfWeek.Monday, 3);

            var items = new List<PublicHoliday>();
            items.Add(new PublicHoliday(year, 1, 1, "Neujahr", "New Year's Day", countryCode, 1967));
            items.Add(new PublicHoliday(year, 1, 2, "Berchtoldstag", "St. Berchtold's Day", countryCode, null, new string[] { "CH-ZH", "CH-BE", "CH-LU", "CH-OW", "CH-GL", "CH-ZG", "CH-FR", "CH-SO", "CH-SH", "CH-TG", "CH-VD", "CH-NE", "CH-GE", "CH-JU" }));
            items.Add(new PublicHoliday(year, 1, 6, "Heilige Drei Könige", "Epiphany", countryCode, null, new string[] { "CH-UR", "CH-SZ", "CH-GR", "CH-TI" }));
            items.Add(new PublicHoliday(year, 3, 19, "Josefstag", "Saint Joseph's Day", countryCode, null, new string[] { "CH-LU", "CH-UR", "CH-SZ", "CH-NW", "CH-ZG", "CH-GR", "CH-TI", "CH-VS" }));
            items.Add(new PublicHoliday(easterSunday.AddDays(-2), "Karfreitag", "Good Friday", countryCode, null, new string[] { "CH-ZH", "CH-BE", "CH-LU", "CH-UR", "CH-SZ", "CH-OW", "CH-NW", "CH-GL", "CH-ZG", "CH-FR", "CH-SO", "CH-BS", "CH-BL", "CH-SH", "CH-AR", "CH-AI", "CH-SG", "CH-GR", "CH-AG", "CH-TG", "CH-VD", "CH-NE", "CH-GE", "CH-JU" }));
            items.Add(new PublicHoliday(easterSunday.AddDays(1), "Ostermontag", "Easter Monday", countryCode, 1642, new string[] { "CH-ZH", "CH-BE", "CH-LU", "CH-UR", "CH-SZ", "CH-OW", "CH-NW", "CH-GL", "CH-ZG", "CH-FR", "CH-SO", "CH-BS", "CH-BL", "CH-SH", "CH-AR", "CH-AI", "CH-SG", "CH-GR", "CH-AG", "CH-TG", "CH-TI", "CH-VD", "CH-NE", "CH-GE", "CH-JU" }));
            items.Add(new PublicHoliday(year, 5, 1, "Tag der Arbeit", "Labour Day", countryCode, null, new string[] { "CH-ZH", "CH-FR", "CH-SO", "CH-BS", "CH-BL", "CH-SH", "CH-AG", "CH-TG", "CH-TI", "CH-NE", "CH-JU" }));
            items.Add(new PublicHoliday(easterSunday.AddDays(39), "Auffahrt", "Ascension Day", countryCode));
            items.Add(new PublicHoliday(easterSunday.AddDays(50), "Pfingstmontag", "Whit Monday", countryCode, null, new string[] { "CH-ZH", "CH-BE", "CH-LU", "CH-UR", "CH-SZ", "CH-OW", "CH-NW", "CH-GL", "CH-ZG", "CH-FR", "CH-SO", "CH-BS", "CH-BL", "CH-SH", "CH-AR", "CH-AI", "CH-SG", "CH-GR", "CH-AG", "CH-TG", "CH-TI", "CH-VD", "CH-NE", "CH-GE", "CH-JU" }));
            items.Add(new PublicHoliday(easterSunday.AddDays(60), "Fronleichnam", "Corpus Christi", countryCode, null, new string[] { "CH-LU", "CH-UR", "CH-SZ", "CH-OW", "CH-NW", "CH-ZG", "CH-FR", "CH-SO", "CH-BL", "CH-AI", "CH-GR", "CH-AG", "CH-TI", "CH-VS", "CH-NE", "CH-JU" }));
            items.Add(new PublicHoliday(year, 8, 1, "Bundesfeier", "Swiss National Day", countryCode));
            items.Add(new PublicHoliday(year, 8, 15, "Maria Himmelfahrt", "Assumption of the Virgin Mary", countryCode, null, new string[] { "CH-LU", "CH-UR", "CH-SZ", "CH-OW", "CH-NW", "CH-ZG", "CH-FR", "CH-SO", "CH-BL", "CH-AI", "CH-GR", "CH-AG", "CH-TI", "CH-VS", "CH-JU" }));
            items.Add(new PublicHoliday(year, 11, 1, "Allerheiligen", "All Saints' Day", countryCode, null, new string[] { "CH-LU", "CH-UR", "CH-SZ", "CH-OW", "CH-NW", "CH-GL", "CH-ZG", "CH-FR", "CH-SO", "CH-AI", "CH-SG", "CH-GR", "CH-AG", "CH-TI", "CH-VS", "CH-JU" }));
            items.Add(new PublicHoliday(year, 12, 8, "Mariä Empfängnis", "Immaculate Conception", countryCode, null, new string[] { "CH-LU", "CH-UR", "CH-SZ", "CH-OW", "CH-NW", "CH-ZG", "CH-FR", "CH-SO", "CH-AI", "CH-GR", "CH-AG", "CH-TI", "CH-VS" }));
            items.Add(new PublicHoliday(year, 12, 25, "Weihnachten", "Christmas Day", countryCode));
            items.Add(new PublicHoliday(year, 12, 26, "Stephanstag", "St. Stephen's Day", countryCode, null, new string[] { "CH-AG", "CH-AI", "CH-AR", "CH-BL", "CH-BS", "CH-BE", "CH-FR", "CH-GL", "CH-GR", "CH-LU", "CH-NW", "CH-OW", "CH-SG", "CH-SH", "CH-SZ", "CH-SO", "CH-TG", "CH-TI", "CH-UR", "CH-ZG", "CH-ZH" }));

            items.Add(new PublicHoliday(firstSundayOfSeptember.AddDays(4), "Jeûne genevois", "Geneva Prayday", countryCode, null, new string[] { "CH-GE" }));
            items.Add(new PublicHoliday(thirdMondayOfSeptember, "Eidgenössischer Dank-, Buss- und Bettag", "Federal Day of Thanksgiving", countryCode, null, new string[] { "CH-ZH", "CH-BE", "CH-LU", "CH-UR", "CH-SZ", "CH-OW", "CH-NW", "CH-GL", "CH-ZG", "CH-FR", "CH-SO", "CH-BS", "CH-BL", "CH-SH", "CH-AR", "CH-AI", "CH-SG", "CH-GR", "CH-AG", "CH-TG", "CH-TI", "CH-VD", "CH-VS", "CH-NE", "CH-JU" }));

            return items.OrderBy(o => o.Date);
        }

        ///<inheritdoc/>
        public IEnumerable<string> GetSources()
        {
            return new string[]
            {
                "https://de.wikipedia.org/wiki/Feiertage_in_der_Schweiz",
                "https://en.wikipedia.org/wiki/Federal_Day_of_Thanksgiving,_Repentance_and_Prayer",
                "https://en.wikipedia.org/wiki/Je%C3%BBne_genevois"
            };
        }
    }
}
