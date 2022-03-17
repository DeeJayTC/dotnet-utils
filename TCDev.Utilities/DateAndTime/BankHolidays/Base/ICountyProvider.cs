using System.Collections.Generic;

namespace TCDev.Utilities.DateAndTime.BankHolidays.Base
{
	/// <summary>
	/// ICountyProvider
	/// </summary>
	public interface ICountyProvider
    {
        /// <summary>
        /// Get Counties
        /// </summary>
        /// <returns></returns>
        IDictionary<string, string> GetCounties();
    }
}
