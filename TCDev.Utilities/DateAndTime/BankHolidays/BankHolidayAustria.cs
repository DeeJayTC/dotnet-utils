﻿using TCDev.Utilities.DateAndTime.BankHolidays.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCDev.Utilities.DateAndTime.BankHolidays
{
    /// <summary>
    /// Austria
    /// </summary>
    public class AustriaProvider : IPublicHolidayProvider, ICountyProvider
    {
        private readonly ICatholicProvider _catholicProvider;

        /// <summary>
        /// AustriaProvider
        /// </summary>
        /// <param name="catholicProvider"></param>
        public AustriaProvider(ICatholicProvider catholicProvider)
        {
            this._catholicProvider = catholicProvider;
        }

        ///<inheritdoc/>
        public IDictionary<string, string> GetCounties()
        {
            return Counties.GetCounties(CountryCode.AU);
        }

        ///<inheritdoc/>
        public IEnumerable<PublicHoliday> Get(int year)
        {
            var countryCode = CountryCode.AT;
            var easterSunday = this._catholicProvider.EasterSunday(year);

            var items = new List<PublicHoliday>();
            items.Add(new PublicHoliday(year, 1, 1, "Neujahr", "New Year's Day", countryCode, 1967));
            items.Add(new PublicHoliday(year, 1, 6, "Heilige Drei Könige", "Epiphany", countryCode));
            //items.Add(new PublicHoliday(year, 3, 19, "St. Josef", "Saint Joseph's Day", countryCode, type: PublicHolidayType.Authorities | PublicHolidayType.School, counties: new string[] { "AT-2", "AT-6", "AT-7", "AT-8" }));
            items.Add(new PublicHoliday(easterSunday.AddDays(1), "Ostermontag", "Easter Monday", countryCode, 1642));
            items.Add(new PublicHoliday(year, 5, 1, "Staatsfeiertag", "National Holiday", countryCode, 1955));
            //items.Add(new PublicHoliday(year, 5, 1, "St. Florian", "Saint Florian", countryCode, type: PublicHolidayType.School, counties: new string[] { "AT-4" }));
            items.Add(new PublicHoliday(easterSunday.AddDays(39), "Christi Himmelfahrt", "Ascension Day", countryCode));
            items.Add(new PublicHoliday(easterSunday.AddDays(50), "Pfingstmontag", "Whit Monday", countryCode));
            items.Add(new PublicHoliday(easterSunday.AddDays(60), "Fronleichnam", "Corpus Christi", countryCode));
            items.Add(new PublicHoliday(year, 8, 15, "Maria Himmelfahrt", "Assumption Day", countryCode));
            items.Add(new PublicHoliday(year, 10, 26, "Nationalfeiertag", "National Holiday", countryCode));
            items.Add(new PublicHoliday(year, 11, 1, "Allerheiligen", "All Saints' Day", countryCode));
            items.Add(new PublicHoliday(year, 12, 8, "Mariä Empfängnis", "Immaculate Conception", countryCode));
            items.Add(new PublicHoliday(year, 12, 25, "Weihnachten", "Christmas Day", countryCode));
            items.Add(new PublicHoliday(year, 12, 26, "Stefanitag", "St. Stephen's Day", countryCode));

            return items.OrderBy(o => o.Date);
        }

        ///<inheritdoc/>
        public IEnumerable<string> GetSources()
        {
            return new string[]
            {
                "https://en.wikipedia.org/wiki/Public_holidays_in_Austria"
            };
        }
    }
}
