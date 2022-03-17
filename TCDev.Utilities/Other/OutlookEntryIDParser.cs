using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCDev.Office.Core
{
  public class EntryID
  {
    private string entryId = "";
    private string serverShortName = "";
    private string userAddress = "";
    public EntryID(string anEntryId)
    {
      entryId = anEntryId;
    }

    public string ServerShortName => getServerShortName();

    public string ServerShortNameWithoutDomain => getServerShortNameWithoutDomain();

    public string UserAddress => getUserAddress();

    private string getServerShortName()
    {
      if (serverShortName != "") return serverShortName;
      byte[] temp = HexToByteArray(entryId.Substring(120, entryId.IndexOf("00", 120) - 120));
      serverShortName = System.Text.Encoding.ASCII.GetString(temp);
      return serverShortName;
    }


    private string getServerShortNameWithoutDomain()
    {
      if (serverShortName != "") return serverShortName;
      byte[] temp = HexToByteArray(entryId.Substring(120, entryId.IndexOf("00", 120) - 120));
      serverShortName = System.Text.Encoding.ASCII.GetString(temp);
      return serverShortName.Substring(0,serverShortName.IndexOf("@"));
    }

    private string getUserAddress()
    {
      if (userAddress != "") return userAddress;
      serverShortName = getServerShortName();
      byte[] temp = HexToByteArray(entryId.Substring(120 + serverShortName.Length * 2 + 2));
      userAddress = System.Text.Encoding.ASCII.GetString(temp);
      return userAddress;
    }

    private byte[] HexToByteArray(string hex)
    {
      byte[] bytes = new byte[hex.Length / 2];

      for (int i = 0; i < hex.Length; i += 2)
      {
        bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
      }

      return bytes;
    }
  } 
}
