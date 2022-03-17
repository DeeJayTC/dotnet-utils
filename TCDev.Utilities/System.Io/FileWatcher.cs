// TCDev 2022/03/17
// Apache 2.0 License
// https://www.github.com/deejaytc/dotnet-utils

using System.IO;

namespace DocumentEditorPrototype.Utility;

public class FileWatcher
{
   public bool IsChanged;
   public FileSystemWatcher watcher;

   public FileWatcher(string path, string filename)
   {
      // Create a new FileSystemWatcher and set its properties.
      this.watcher = new FileSystemWatcher
      {
         Path = path, NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite
                                                              | NotifyFilters.FileName | NotifyFilters.DirectoryName
         , Filter = filename
      };
      /* Watch for changes in LastAccess and LastWrite times, and the renaming of files or directories. */

      // Add event handlers.
      this.watcher.Changed += OnChanged;
      this.watcher.Created += OnChanged;
      this.watcher.Deleted += OnChanged;
      this.watcher.Renamed += OnRenamed;

      // Begin watching.
      this.watcher.EnableRaisingEvents = true;
   }

   // Define the event handlers.
   private void OnChanged(object source, FileSystemEventArgs e)
   {
      this.IsChanged = true;
   }

   private void OnRenamed(object source, RenamedEventArgs e)
   {
   }
}