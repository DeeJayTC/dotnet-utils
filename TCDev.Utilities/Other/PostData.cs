// TCDev.de 2022/03/17
// TCDev.Utilities.PostData.cs
// https://www.github.com/deejaytc/dotnet-utils

using System.Collections.Generic;
using System.Text;

namespace TCDev.Utilities.Helper;

public class PostData
{
   // Change this if you need to, not necessary
   public static string boundary = "AaB03x";

   public List<PostDataParam> Params { get; set; }

   public PostData()
   {
      this.Params = new List<PostDataParam>();
   }

   /// <summary>
   ///    Returns the parameters array formatted for multi-part/form data
   /// </summary>
   /// <returns></returns>
   public string GetPostData()
   {
      var sb = new StringBuilder();
      foreach (var p in this.Params)
      {
         sb.AppendLine("--" + boundary);

         if (p.Type == PostDataParamType.File)
         {
            sb.AppendLine(string.Format("Content-Disposition: file; name=\"{0}\"; filename=\"{1}\"", p.Name, p.FileName));
            sb.AppendLine("Content-Type: application/octet-stream");
            sb.AppendLine();
            sb.AppendLine(p.Value);
         }
         else
         {
            sb.AppendLine(string.Format("Content-Disposition: form-data; name=\"{0}\"", p.Name));
            sb.AppendLine();
            sb.AppendLine(p.Value);
         }
      }

      sb.AppendLine("--" + boundary + "--");

      return sb.ToString();
   }
}

public enum PostDataParamType
{
   Field, File
}

public class PostDataParam
{
   public string FileName;

   public string Name;
   public PostDataParamType Type;
   public string Value;

   public PostDataParam(string name, string value, PostDataParamType type)
   {
      this.Name = name;
      this.Value = value;
      this.Type = type;
   }

   public PostDataParam(string name, string filename, string value, PostDataParamType type)
   {
      this.Name = name;
      this.Value = value;
      this.FileName = filename;
      this.Type = type;
   }
}
