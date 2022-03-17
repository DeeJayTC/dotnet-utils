// TCDev 2022/03/17
// Apache 2.0 License
// https://www.github.com/deejaytc/dotnet-utils

using System.IO;

namespace TCDev.Utilities.Extensions;

public static class FileInfoExtensions
{
   public static bool IsLocked(this FileInfo file)
   {
      FileStream stream = null;

      try
      {
         stream = file.Open(FileMode.Open, FileAccess.Read, FileShare.None);
      }
      catch (IOException)
      {
         //the file is unavailable because it is:
         //still being written to
         //or being processed by another thread
         //or does not exist (has already been processed)
         return true;
      }
      finally
      {
         if (stream != null)
            stream.Close();
      }

      //file is not locked
      return false;
   }
}