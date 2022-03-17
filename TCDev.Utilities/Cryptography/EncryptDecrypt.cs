// TCDev 2022/03/15
// Apache 2.0 License
// https://www.github.com/deejaytc/dotnet-utils

using System;
using System.Security.Cryptography;
using System.Text;

namespace TCDev.Utilities.Cryptography
{
   public static class StringExtensions
   {
      /// <summary>
      ///    Encryptes a string using the supplied key. Encoding is done using RSA encryption.
      /// </summary>
      /// <param name="stringToEncrypt">String that must be encrypted.</param>
      /// <param name="key">Encryptionkey.</param>
      /// <returns>A string representing a byte array separated by a minus sign.</returns>
      /// <exception cref="ArgumentException">Occurs when stringToEncrypt or key is null or empty.</exception>
      public static string Encrypt(this string stringToEncrypt, string key)
      {
         // Get the bytes of the string
         var bytesToBeEncrypted = Encoding.UTF8.GetBytes(stringToEncrypt);
         var passwordBytes = Encoding.UTF8.GetBytes(key);

         // Hash the password with SHA256
         passwordBytes = SHA512.Create().ComputeHash(passwordBytes);

         var bytesEncrypted = Encryption.AES_Encrypt(bytesToBeEncrypted, passwordBytes);

         var result = Convert.ToBase64String(bytesEncrypted);

         return result;
      }

      /// <summary>
      ///    Decryptes a string using the supplied key. Decoding is done using RSA encryption.
      /// </summary>
      /// <param name="stringToDecrypt">String that must be decrypted.</param>
      /// <param name="key">Decryptionkey.</param>
      /// <returns>The decrypted string or null if decryption failed.</returns>
      /// <exception cref="ArgumentException">Occurs when stringToDecrypt or key is null or empty.</exception>
      public static string Decrypt(this string stringToDecrypt, string key)
      {
         // Get the bytes of the string
         var bytesToBeDecrypted = Convert.FromBase64String(stringToDecrypt);
         var passwordBytes = Encoding.UTF8.GetBytes(key);
         passwordBytes = SHA512.Create().ComputeHash(passwordBytes);

         var bytesDecrypted = Encryption.AES_Decrypt(bytesToBeDecrypted, passwordBytes);

         var result = Encoding.UTF8.GetString(bytesDecrypted);

         return result;
      }
   }
}