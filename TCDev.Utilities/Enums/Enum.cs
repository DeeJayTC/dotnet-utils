// TCDev.de 2022/03/17
// TCDev.Utilities.Enum.cs
// https://www.github.com/deejaytc/dotnet-utils

using System.ComponentModel;

namespace TCDev.Utilities.Reflection.Enum;

public static class Enums
{
   /// <summary>
   ///    Get the description attribute value of an enum type
   /// </summary>
   /// <typeparam name="TEnum"></typeparam>
   /// <param name="value"></param>
   /// <returns></returns>
   public static string GetEnumDescription<TEnum>(int value)
   {
      return GetEnumDescription((System.Enum)(object)(TEnum)(object)value); // ugly, but works
   }

   public static string GetEnumDescription(System.Enum value)
   {
      var fi = value.GetType()
         .GetField(value.ToString());

      var attributes =
         (DescriptionAttribute[])fi.GetCustomAttributes(
            typeof(DescriptionAttribute),
            false);

      if (attributes.Length > 0)
         return attributes[0]
            .Description;
      return value.ToString();
   }
}
