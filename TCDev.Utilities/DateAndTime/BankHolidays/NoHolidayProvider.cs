// TCDev.de 2021/08/30
// TCDev.Utilities.NoHolidayProvider.cs
// https://www.github.com/deejaytc/dotnet-utils

using System;
using System.Collections.Generic;
using System.Linq;
using TCDev.Utilities.DateAndTime.BankHolidays.Base;

namespace TCDev.Utilities.DateAndTime.BankHolidays;

/// <summary>
///    NoHolidaysProvider
/// </summary>
internal class NoHolidaysProvider : IPublicHolidayProvider
{
   private static readonly Lazy<IPublicHolidayProvider> _instance = new(() => new NoHolidaysProvider());

   /// <summary>
   ///    Gets the singleton instance of <see cref="NoHolidaysProvider" />.
   /// </summary>
   public static IPublicHolidayProvider Instance => _instance.Value;

   private NoHolidaysProvider() { }

   /// <inheritdoc />
   public IEnumerable<PublicHoliday> Get(int year)
   {
      return Enumerable.Empty<PublicHoliday>();
   }

   /// <inheritdoc />
   public IEnumerable<string> GetSources()
   {
      return Enumerable.Empty<string>();
   }
}
