using System;
using System.Collections.Generic;

namespace ScreenChecker.Lib.Folder
{
    internal class WindowInfoCollection
    {
        public List<WindowInfo> List = null;

        public WindowInfoCollection()
        {
            this.List = new List<WindowInfo>();
            IntPtr foregroundHandle = NativeMethods.GetForegroundWindow();

            FindWindowWithoutExplorer(foregroundHandle);
            FindWindowExplorerOnly(foregroundHandle);
        }

        private void FindWindowWithoutExplorer(IntPtr foregroundHandle)
        {
            NativeMethods.EnumWindows((hWnd, lParam) =>
            {
                var info = new WindowInfo(hWnd, foregroundHandle);
                if (info.Enabled)
                {
                    this.List.Add(info);
                }
                return true;
            }, IntPtr.Zero);
        }

        private void FindWindowExplorerOnly(IntPtr foregroundHandle)
        {
            var type = Type.GetTypeFromProgID("Shell.Application");
            dynamic o = Activator.CreateInstance(type);
            dynamic ws = o.Windows();
            for (int i = 0; i < ws.Count; i++)
            {
                dynamic ie = ws.Item(i);
                if (ie.hWnd != 0)
                {
                    IntPtr hWnd = (IntPtr)ie.hWnd;
                    var info = new WindowInfo(hWnd, foregroundHandle, isExplorer: true);
                    info.FolderPath = Uri.UnescapeDataString(new Uri(ie.LocationURL).AbsolutePath);
                    if (info.Enabled)
                    {
                        List.Add(info);
                    }
                }
            }
        }
    }
}
