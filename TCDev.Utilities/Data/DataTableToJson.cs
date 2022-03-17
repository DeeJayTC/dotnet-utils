// TCDev 2022/03/17
// Apache 2.0 License
// https://www.github.com/deejaytc/dotnet-utils

using System.Data;
using System.Text;

namespace Tente.Utilities.Data;

public static class JsonEncoderHelper
{
   public static string ToJson(this DataTable table)
   {
      var sbuilder = new StringBuilder();

      sbuilder.Append("{\"");
      sbuilder.Append(table.TableName);
      sbuilder.Append("\":[");

      var first = true;
      foreach (DataRow drow in table.Rows)
      {
         if (first)
         {
            sbuilder.Append("{");
            first = false;
         }
         else
         {
            sbuilder.Append(",{");
         }

         var firstColumn = true;
         foreach (DataColumn column in table.Columns)
         {
            if (firstColumn)
            {
               sbuilder.Append(string.Format("\"{0}\":\"{1}\"", column.ColumnName, drow[column]));
               firstColumn = false;
            }
            else
            {
               sbuilder.Append(string.Format(",\"{0}\":\"{1}\"", column.ColumnName, drow[column]));
            }
         }

         sbuilder.Append("}");
      }

      sbuilder.Append("]}");

      return sbuilder.ToString();
   }
}