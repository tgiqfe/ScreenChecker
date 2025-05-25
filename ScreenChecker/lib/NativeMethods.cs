using System;
using System.Runtime.InteropServices;

namespace ScreenChecker.Lib
{
    internal class NativeMethods
    {
        #region for input (mouse, keyboard)

        [DllImport("user32.dll", SetLastError = true)]
        internal static extern uint SendInput(uint nInputs, ref Input pInputs, int cbSize);

        [DllImport("user32.dll", SetLastError = true)]
        internal static extern IntPtr GetMessageExtraInfo();

        [DllImport("user32.dll", SetLastError = true)]
        internal static extern bool GetCursorPos(ref Win32Point pt);

        [DllImport("user32.dll")]
        internal static extern bool SetCursorPos(int x, int y);

        [DllImport("user32.dll", SetLastError = true, EntryPoint = "MapVirtualKeyA")]
        internal static extern uint MapVirtualKey(uint uCode, uint uMapType);

        internal const int INPUT_MOUSE = 0;
        internal const int INPUT_KEYBOARD = 1;
        internal const int INPUT_HARDWARE = 2;

        internal const uint MOUSEEVENTF_MOVE = 0x0001;
        internal const uint MOUSEEVENTF_LEFTDOWN = 0x0002;
        internal const uint MOUSEEVENTF_LEFTUP = 0x0004;
        internal const uint MOUSEEVENTF_RIGHTDOWN = 0x0008;
        internal const uint MOUSEEVENTF_RIGHTUP = 0x0010;
        internal const uint MOUSEEVENTF_MIDDLEDOWN = 0x0020;
        internal const uint MOUSEEVENTF_MIDDLEUP = 0x0040;
        internal const uint MOUSEEVENTF_XDOWN = 0x0080;
        internal const uint MOUSEEVENTF_XUP = 0x0100;
        internal const uint MOUSEEVENTF_WHEEL = 0x0800;
        internal const uint MOUSEEVENTF_HWHEEL = 0x01000;
        internal const uint MOUSEEVENTF_VIRTUALDESK = 0x4000;
        internal const uint MOUSEEVENTF_ABSOLUTE = 0x8000;
        internal const uint MOUSEEVENTF_WHEEL_DELTA = 0x078;

        internal const uint KEYEVENTF_KEYDOWN = 0x0000;
        internal const uint KEYEVENTF_KEYUP = 0x0002;
        internal const int KEYEVENTF_EXTENDEDKEY = 0x0001;

        [StructLayout(LayoutKind.Sequential)]
        internal struct Win32Point
        {
            public int x;
            public int y;
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct MouseInput
        {
            public int X;
            public int Y;
            public int Data;
            public uint Flags;
            public int Time;
            public IntPtr ExtraInfo;
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct KeyboardInput
        {
            public ushort VirtualKey;
            public ushort ScanCode;
            public uint Flags;
            public int Time;
            public IntPtr ExtraInfo;
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct HardwareInput
        {
            public int Msg;
            public ushort wParamL;
            public ushort wParamH;
        }

        [StructLayout(LayoutKind.Explicit)]
        internal struct InputUnion
        {
            [FieldOffset(0)]
            public MouseInput Mouse;
            [FieldOffset(0)]
            public KeyboardInput Keyboard;
            [FieldOffset(0)]
            public HardwareInput Hardware;
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct Input
        {
            public int Type;
            public InputUnion ui;
        }

        #endregion
        #region for Screen check

        internal const int SRCCOPY = 13369376;
        internal const int CAPTUREBLT = 1073741824;

        [DllImport("user32.dll")]
        internal static extern IntPtr GetDC(IntPtr hwnd);

        [DllImport("gdi32.dll")]
        internal static extern int BitBlt(IntPtr hDeskDC,
            int x,
            int y,
            int nWidth,
            int nHeight,
            IntPtr hSrcDC,
            int xSrc,
            int ySrc,
            int dwRop);

        [DllImport("user32.dll")]
        internal static extern IntPtr ReleaseDC(IntPtr hwnd, IntPtr hDC);

        [StructLayout(LayoutKind.Sequential)]
        internal struct RECT
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }

        [DllImport("user32.dll")]
        internal static extern IntPtr GetWindowDC(IntPtr hwnd);

        [DllImport("user32.dll")]
        internal static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        internal static extern int GetWindowRect(IntPtr hwnd, ref RECT lpRect);

        #endregion
        #region for Open/Close folder

        [DllImport("shell32.dll", CharSet = CharSet.Auto)]
        internal static extern int ShellExecute(IntPtr hwnd, string hWnd, string lpOperation, string lpParameters, string lpDirectory, int nShowCmd);

        [DllImport("shell32.dll", CharSet = CharSet.Unicode, ExactSpelling = true)]
        internal static extern int SHOpenFolderAndSelectItems(
            IntPtr pidlFolder,
            uint cidl,
            [MarshalAs(UnmanagedType.LPArray)] IntPtr[] apidl,
            uint dwFlags);

        [DllImport("shell32.dll", CharSet = CharSet.Unicode, ExactSpelling = true)]
        internal static extern int SHParseDisplayName(
            [MarshalAs(UnmanagedType.LPWStr)] string pszName,
            IntPtr pdc,
            out IntPtr ppidl,
            uint sfgaoIn,
            out uint psfgaoOut);

        [DllImport("ole32.dll", CharSet = CharSet.Unicode, ExactSpelling = true)]
        internal static extern void CoTaskMemFree(IntPtr pv);

        [DllImport("user32.dll", SetLastError = true)]
        internal static extern bool PostMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

        internal const uint WM_CLOSE = 0x0010;

        #endregion
    }
}
