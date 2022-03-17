using System.Collections.Generic;

namespace TCDev.Utilities.DateAndTime.BankHolidays.Base
{

    internal class AvailableDayResult
    {
        internal int DayCount { get; set; }
        internal bool BridgeDayRequired { get; set; }
    }

    /// <summary>
    /// ILongWeekendCalculator
    /// </summary>
    public interface ILongWeekendCalculator
    {
        /// <summary>
        /// Calculate Long weekends
        /// </summary>
        /// <param name="publicHolidays"></param>
        /// <returns></returns>
        IEnumerable<LongWeekend> Calculate(IEnumerable<PublicHoliday> publicHolidays);
    }

}