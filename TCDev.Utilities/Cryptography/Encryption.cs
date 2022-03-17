// TCDev 2022/03/17
// Apache 2.0 License
// https://www.github.com/deejaytc/dotnet-utils

using System.Security.Cryptography;
using System.Text;

namespace TCDev.Utilities.Cryptography
{
   public static class KeyCreator
   {
      public static string CreateKey(int numBytes)
      {
         var rng = new RNGCryptoServiceProvider();
         var buff = new byte[numBytes];

         rng.GetBytes(buff);
         return BytesToHexString(buff);
      }

      private static string BytesToHexString(byte[] bytes)
      {
         var hexString = new StringBuilder(64);

         foreach (var t in bytes)
            hexString.Append($"{t:X2}");

         return hexString.ToString();
      }
   }

   public static class Encryption
   {
      public static byte[] AES_Encrypt(byte[] bytesToBeEncrypted, byte[] passwordBytes)
      {
         byte[] encryptedBytes = null;

         // Set your salt here, change it to meet your flavor:
         // The salt bytes must be at least 8 bytes.
         byte[] saltBytes = {1, 2, 3, 4, 5, 6, 7, 8};

         using (var ms = new MemoryStream())
         {
            using (var AES = new RijndaelManaged())
            {
               AES.KeySize = 256;
               AES.BlockSize = 128;

               var key = new Rfc2898DeriveBytes(passwordBytes, saltBytes, 1000);
               AES.Key = key.GetBytes(AES.KeySize / 8);
               AES.IV = key.GetBytes(AES.BlockSize / 8);

               AES.Mode = CipherMode.CBC;

               using (var cs = new CryptoStream(ms, AES.CreateEncryptor(), CryptoStreamMode.Write))
               {
                  cs.Write(bytesToBeEncrypted, 0, bytesToBeEncrypted.Length);
                  cs.Close();
               }

               encryptedBytes = ms.ToArray();
            }
         }

         return encryptedBytes;
      }


      public static byte[] AES_Decrypt(byte[] bytesToBeDecrypted, byte[] passwordBytes)
      {
         byte[] decryptedBytes = null;

         // Set your salt here, change it to meet your flavor:
         // The salt bytes must be at least 8 bytes.
         byte[] saltBytes = {1, 2, 3, 4, 5, 6, 7, 8};

         using var ms = new MemoryStream();
         using var AES = new RijndaelManaged();
         AES.KeySize = 256;
         AES.BlockSize = 128;

         var key = new Rfc2898DeriveBytes(passwordBytes, saltBytes, 1000);
         AES.Key = key.GetBytes(AES.KeySize / 8);
         AES.IV = key.GetBytes(AES.BlockSize / 8);

         AES.Mode = CipherMode.CBC;

         using (var cs = new CryptoStream(ms, AES.CreateDecryptor(), CryptoStreamMode.Write))
         {
            cs.Write(bytesToBeDecrypted, 0, bytesToBeDecrypted.Length);
            cs.Close();
         }

         decryptedBytes = ms.ToArray();

         return decryptedBytes;
      }
   }
}