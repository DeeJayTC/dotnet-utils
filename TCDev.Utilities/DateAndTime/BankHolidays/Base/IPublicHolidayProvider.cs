using System.Collections.Generic;

namespace TCDev.Utilities.DateAndTime.BankHolidays.Base
{
    /// <summary>
    /// Public Holiday Provider Interface
    /// </summary>
    public interface IPublicHolidayProvider
    {
        /// <summary>
        /// Get Holidays of the given year
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        IEnumerable<PublicHoliday> Get(int year);

        /// <summary>
        ///Get the Holiday Sources
        /// </summary>
        /// <returns></returns>
        IEnumerable<string> GetSources();
    }
}
