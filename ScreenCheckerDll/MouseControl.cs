using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ScreenChecker
{
    public class MouseControl
    {
        [DllImport("user32.dll", CallingConvention = CallingConvention.StdCall)]
        private static extern void SetCursorPos(int x, int y);

        [DllImport("user32.dll", CallingConvention = CallingConvention.StdCall)]
        private static extern void mouse_event(int dwFlags, int dx, int dy, int cButtons, int dxExtraInfo);

        const int MOUSEEVENTF_ABSOLUTE = 0x8000;
        const int MOUSEEVENTF_LEFTDOWN = 0x0002;    //  左クリックDown
        const int MOUSEEVENTF_LEFTUP = 0x0004;      //  左クリックUp
        const int MOUSEEVENTF_MIDDLEDOWN = 0x0020;  //  中クリックDown
        const int MOUSEEVENTF_MIDDLEUP = 0x0040;    //  中クリックUP
        const int MOUSEEVENTF_MOVE = 0x0001;
        const int MOUSEEVENTF_RIGHTDOWN = 0x0008;   //  右クリックDown
        const int MOUSEEVENTF_RIGHTUP = 0x0010;     //  右クリックUp
        const int MOUSEEVENTF_WHEEL = 0x0800;
        const int MOUSEEVENTF_XDOWN = 0x0080;
        const int MOUSEEVENTF_XUP = 0x0100;
        const int MOUSEEVENTF_HWHEEL = 0x01000;

        public static void Click(int x, int y)
        {
            SetCursorPos(x, y);
            mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);
            mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
        }

        public static void Click(Point point)
        {
            Click(point.X, point.Y);
        }

        public static void DoubleClick(int x, int y)
        {
            SetCursorPos(x, y);
            mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);
            mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
            Thread.Sleep(50);
            mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);
            mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
        }

        public static void DoubleClick(Point point)
        {
            DoubleClick(point.X, point.Y);
        }
    }
}
