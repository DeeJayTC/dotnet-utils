// TCDev 2022/03/17
// Apache 2.0 License
// https://www.github.com/deejaytc/dotnet-utils

using System.Net.Http;
using System.Text;

namespace TeamWorkNet.Helper.String;

public class JsonContent : StringContent
{
   private readonly string ObjectName;

   public JsonContent(string objectName, string content, Encoding encoding)
      : base(content, encoding)
   {
      this.ObjectName = objectName;
   }

   public override string ToString()
   {
      return "{" + this.ObjectName + ":" + base.ToString() + "}";
   }
}