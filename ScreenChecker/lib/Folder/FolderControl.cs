using System;
using System.IO;
using System.Runtime.InteropServices;

namespace ScreenChecker.Lib.Folder
{
    internal class FolderControl
    {
        public static void Open(string folderPath, string itemName = null)
        {
            if (string.IsNullOrEmpty(itemName))
            {
                NativeMethods.ShellExecute(IntPtr.Zero, "open", folderPath, null, null, 1);
            }
            else
            {
                string item = itemName.Contains("\\") ? Path.GetFileName(itemName) : itemName;
                string itemPath = Path.Combine(folderPath, item);

                uint psfgaoOut;
                IntPtr pidFolder, pidFile;
                var hr_d = NativeMethods.SHParseDisplayName(folderPath, IntPtr.Zero, out pidFolder, 0, out psfgaoOut);
                var hr_f = NativeMethods.SHParseDisplayName(itemPath, IntPtr.Zero, out pidFile, 0, out psfgaoOut);

                IntPtr[] array = { pidFile };
                var ret = NativeMethods.SHOpenFolderAndSelectItems(pidFolder, (uint)array.Length, array, 0);
                NativeMethods.CoTaskMemFree(pidFolder);
                NativeMethods.CoTaskMemFree(pidFile);
            }
        }

        public static void Close(string folderPath)
        {
            string uri = new Uri(folderPath).ToString().TrimEnd('/');

            var t = Type.GetTypeFromProgID("Shell.Application");
            dynamic o = Activator.CreateInstance(t);
            try
            {
                var ws = o.Windows();
                for (int i = 0; i < ws.Count; i++)
                {
                    var ie = ws.Item(i);
                    if (ie == null) continue;

                    string processName = Path.GetFileName(ie.FullName as string);
                    if (processName.Equals("explorer.exe", StringComparison.OrdinalIgnoreCase))
                    {
                        string locationPath = new Uri(ie.LocationURL as string).ToString().TrimEnd('/');
                        if (locationPath.TrimEnd('/').Equals(uri, StringComparison.OrdinalIgnoreCase))
                        {
                            NativeMethods.PostMessage((IntPtr)ie.HWND, NativeMethods.WM_CLOSE, IntPtr.Zero, IntPtr.Zero);
                        }
                    }
                }
            }
            finally
            {
                Marshal.FinalReleaseComObject(o);
            }
        }
    }
}
