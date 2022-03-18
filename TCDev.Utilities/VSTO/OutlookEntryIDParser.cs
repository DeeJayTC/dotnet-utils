// TCDev 2022/03/18
// Apache 2.0 License
// https://www.github.com/deejaytc/dotnet-utils

using System;
using System.Text;

namespace TCDev.Office.Core;

/// <summary>
///    Helps find specific objects and user infos in an VSTO APP
/// </summary>
public class EntryID
{
   public string ServerShortName => getServerShortName();

   public string ServerShortNameWithoutDomain => getServerShortNameWithoutDomain();

   public string UserAddress => getUserAddress();
   private readonly string entryId = "";
   private string serverShortName = "";
   private string userAddress = "";

   public EntryID(string anEntryId)
   {
      this.entryId = anEntryId;
   }

   private string getServerShortName()
   {
      if (this.serverShortName != "") return this.serverShortName;
      var temp = HexToByteArray(this.entryId.Substring(120, this.entryId.IndexOf("00", 120) - 120));
      this.serverShortName = Encoding.ASCII.GetString(temp);
      return this.serverShortName;
   }


   private string getServerShortNameWithoutDomain()
   {
      if (this.serverShortName != "") return this.serverShortName;
      var temp = HexToByteArray(this.entryId.Substring(120, this.entryId.IndexOf("00", 120) - 120));
      this.serverShortName = Encoding.ASCII.GetString(temp);
      return this.serverShortName.Substring(0, this.serverShortName.IndexOf("@"));
   }

   private string getUserAddress()
   {
      if (this.userAddress != "") return this.userAddress;
      this.serverShortName = getServerShortName();
      var temp = HexToByteArray(this.entryId.Substring(120 + this.serverShortName.Length * 2 + 2));
      this.userAddress = Encoding.ASCII.GetString(temp);
      return this.userAddress;
   }

   private byte[] HexToByteArray(string hex)
   {
      var bytes = new byte[hex.Length / 2];

      for (var i = 0; i < hex.Length; i += 2) bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);

      return bytes;
   }
}