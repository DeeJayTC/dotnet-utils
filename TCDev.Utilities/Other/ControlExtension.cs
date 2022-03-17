// TCDev 2022/03/17
// Apache 2.0 License
// https://www.github.com/deejaytc/dotnet-utils

using System;

namespace TCDev.Office.Core;

public static class ControlExtensions
{
   /// <summary>
   ///    Executes the Action asynchronously on the UI thread, does not block execution on the calling thread.
   /// </summary>
   /// <param name="control"></param>
   /// <param name="code"></param>
   public static void UIThread(this Control @this, Action code)
   {
      if (@this.InvokeRequired)
         @this.BeginInvoke(code);
      else
         code.Invoke();
   }
}