// TCDev.de 2022/03/17
// TCDev.Utilities.DecimalExtensions.cs
// https://www.github.com/deejaytc/dotnet-utils

namespace TCDev.Utilities.Numeric;

/// <summary>
///    Decimal Extensions
/// </summary>
public static class DecimalExtensions
{
   #region PercentageOf calculations

   /// <summary>
   ///    The numbers percentage
   /// </summary>
   /// <param name="number">The number.</param>
   /// <param name="percent">The percent.</param>
   /// <returns>The result</returns>
   public static decimal PercentageOf(this decimal number, int percent)
   {
      return number * percent / 100;
   }

   /// <summary>
   ///    Percentage of the number.
   /// </summary>
   /// <param name="percent">The percent</param>
   /// <param name="number">The Number</param>
   /// <returns>The result</returns>
   public static decimal PercentOf(this decimal position, int total)
   {
      decimal result = 0;
      if (position > 0 && total > 0)
         result = position / total * 100;
      return result;
   }

   /// <summary>
   ///    The numbers percentage
   /// </summary>
   /// <param name="number">The number.</param>
   /// <param name="percent">The percent.</param>
   /// <returns>The result</returns>
   public static decimal PercentageOf(this decimal number, decimal percent)
   {
      return number * percent / 100;
   }

   /// <summary>
   ///    Percentage of the number.
   /// </summary>
   /// <param name="percent">The percent</param>
   /// <param name="number">The Number</param>
   /// <returns>The result</returns>
   public static decimal PercentOf(this decimal position, decimal total)
   {
      decimal result = 0;
      if (position > 0 && total > 0)
         result = position / total * 100;
      return result;
   }

   /// <summary>
   ///    The numbers percentage
   /// </summary>
   /// <param name="number">The number.</param>
   /// <param name="percent">The percent.</param>
   /// <returns>The result</returns>
   public static decimal PercentageOf(this decimal number, long percent)
   {
      return number * percent / 100;
   }

   /// <summary>
   ///    Percentage of the number.
   /// </summary>
   /// <param name="percent">The percent</param>
   /// <param name="number">The Number</param>
   /// <returns>The result</returns>
   public static decimal PercentOf(this decimal position, long total)
   {
      decimal result = 0;
      if (position > 0 && total > 0)
         result = position / total * 100;
      return result;
   }

   #endregion
}
