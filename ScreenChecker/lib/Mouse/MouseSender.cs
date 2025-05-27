using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScreenChecker.Lib.Mouse
{
    internal class MouseSender
    {
        internal static void SendMouseMove(int x, int y, int screenNum = 0, bool isFast = false)
        {
            var screen = screenNum == 0 ?
                Screen.PrimaryScreen :
                Screen.AllScreens[screenNum];

            if (isFast)
            {
                var extraInfo = NativeMethods.GetMessageExtraInfo();
                var input_move = MouseControl.MouseMoveData(x, y, screen, extraInfo);

                var inputs1 = new NativeMethods.Input[]
                {
                    input_move
                };
                NativeMethods.SendInput((uint)inputs1.Length, ref inputs1[0], Marshal.SizeOf(inputs1[0]));
            }
            else
            {
                var pt = new NativeMethods.Win32Point() { x = 0, y = 0 };
                NativeMethods.GetCursorPos(ref pt);
                MouseControl.SlowMove(pt.x, pt.y, x, y);
            }
        }

        internal static void SendMouseLeftClick(int interval = 100)
        {
            var extraInfo = NativeMethods.GetMessageExtraInfo();
            var input_leftdown = MouseControl.MouseDataWithoutMove(NativeMethods.MOUSEEVENTF_LEFTDOWN, extraInfo);
            var input_leftup = MouseControl.MouseDataWithoutMove(NativeMethods.MOUSEEVENTF_LEFTUP, extraInfo);

            var inputs1 = new NativeMethods.Input[]
            {
                input_leftdown,
            };
            NativeMethods.SendInput((uint)inputs1.Length, ref inputs1[0], Marshal.SizeOf(inputs1[0]));

            Thread.Sleep(interval);

            var inputs2 = new NativeMethods.Input[]
            {
                input_leftup
            };
            NativeMethods.SendInput((uint)inputs2.Length, ref inputs2[0], Marshal.SizeOf(inputs2[0]));
        }

        internal static void SendMouseLeftDoubleClick(int interval = 100, int wClickInterval = 300)
        {
            var extraInfo = NativeMethods.GetMessageExtraInfo();
            var input_leftdown = MouseControl.MouseDataWithoutMove(NativeMethods.MOUSEEVENTF_LEFTDOWN, extraInfo);
            var input_leftup = MouseControl.MouseDataWithoutMove(NativeMethods.MOUSEEVENTF_LEFTUP, extraInfo);

            var inputs1 = new NativeMethods.Input[]
            {
                input_leftdown,
            };
            NativeMethods.SendInput((uint)inputs1.Length, ref inputs1[0], Marshal.SizeOf(inputs1[0]));

            Thread.Sleep(interval);

            var inputs2 = new NativeMethods.Input[]
            {
                input_leftup
            };
            NativeMethods.SendInput((uint)inputs2.Length, ref inputs2[0], Marshal.SizeOf(inputs2[0]));

            Thread.Sleep(wClickInterval);

            var inputs3 = new NativeMethods.Input[]
            {
                input_leftdown,
            };
            NativeMethods.SendInput((uint)inputs3.Length, ref inputs3[0], Marshal.SizeOf(inputs3[0]));

            Thread.Sleep(interval);

            var inputs4 = new NativeMethods.Input[] {
                input_leftup
            };
            NativeMethods.SendInput((uint)inputs4.Length, ref inputs4[0], Marshal.SizeOf(inputs4[0]));
        }

        internal static void SendMouseMiddleClick(int interval = 100)
        {
            var extraInfo = NativeMethods.GetMessageExtraInfo();
            var input_middledown = MouseControl.MouseDataWithoutMove(NativeMethods.MOUSEEVENTF_MIDDLEDOWN, extraInfo);
            var input_middleup = MouseControl.MouseDataWithoutMove(NativeMethods.MOUSEEVENTF_MIDDLEUP, extraInfo);

            var inputs1 = new NativeMethods.Input[]
            {
                input_middledown,
            };
            NativeMethods.SendInput((uint)inputs1.Length, ref inputs1[0], Marshal.SizeOf(inputs1[0]));

            Thread.Sleep(interval);

            var inputs2 = new NativeMethods.Input[]
            {
                input_middleup
            };
            NativeMethods.SendInput((uint)inputs2.Length, ref inputs2[0], Marshal.SizeOf(inputs2[0]));
        }

        internal static void SendMouseRightClick(int interval = 100)
        {
            var extraInfo = NativeMethods.GetMessageExtraInfo();
            var input_rightdown = MouseControl.MouseDataWithoutMove(NativeMethods.MOUSEEVENTF_RIGHTDOWN, extraInfo);
            var input_rightup = MouseControl.MouseDataWithoutMove(NativeMethods.MOUSEEVENTF_RIGHTUP, extraInfo);

            var inputs1 = new NativeMethods.Input[]
            {
                input_rightdown,
            };
            NativeMethods.SendInput((uint)inputs1.Length, ref inputs1[0], Marshal.SizeOf(inputs1[0]));

            Thread.Sleep(interval);

            var inputs2 = new NativeMethods.Input[]
            {
                input_rightup
            };
            NativeMethods.SendInput((uint)inputs2.Length, ref inputs2[0], Marshal.SizeOf(inputs2[0]));
        }

        internal static void SendMouseLeftDrag(int x2, int y2, int screenNum = 0, bool isFast = false, int interval = 500)
        {
            var pt = new NativeMethods.Win32Point() { x = 0, y = 0 };
            NativeMethods.GetCursorPos(ref pt);
            SendMouseLeftDrag(pt.x, pt.y, x2, y2, screenNum, isFast, interval);
        }

        internal static void SendMouseLeftDrag(int x1, int y1, int x2, int y2, int screenNum = 0, bool isFast = false, int interval = 500)
        {
            var screen = screenNum == 0 ?
                Screen.PrimaryScreen :
                Screen.AllScreens[screenNum];

            var extraInfo = NativeMethods.GetMessageExtraInfo();
            var input_move_start = MouseControl.MouseMoveData(x1, y1, screen, extraInfo);
            var input_leftdown = MouseControl.MouseDataWithoutMove(NativeMethods.MOUSEEVENTF_LEFTDOWN, extraInfo);
            var input_move_end = MouseControl.MouseMoveData(x2, y2, screen, extraInfo);
            var input_leftup = MouseControl.MouseDataWithoutMove(NativeMethods.MOUSEEVENTF_LEFTUP, extraInfo);

            if (isFast)
            {
                var inputs1 = new NativeMethods.Input[]
                {
                    input_move_start,
                };
                NativeMethods.SendInput((uint)inputs1.Length, ref inputs1[0], Marshal.SizeOf(inputs1[0]));
            }
            else
            {
                var pt = new NativeMethods.Win32Point() { x = 0, y = 0 };
                NativeMethods.GetCursorPos(ref pt);
                MouseControl.SlowMove(pt.x, pt.y, x1, y1);
            }

            var inputs2 = new NativeMethods.Input[]
            {
                input_leftdown,
            };
            NativeMethods.SendInput((uint)inputs2.Length, ref inputs2[0], Marshal.SizeOf(inputs2[0]));

            if (isFast)
            {
                Thread.Sleep(interval);
            }
            else
            {
                MouseControl.SlowMove(x1, y1, x2, y2);
            }

            var inputs3 = new NativeMethods.Input[]
            {
                input_move_end,
                input_leftup
            };
            NativeMethods.SendInput((uint)inputs3.Length, ref inputs3[0], Marshal.SizeOf(inputs3[0]));
        }

        internal static void SendMouseWheel(int delta)
        {
            var extraInfo = NativeMethods.GetMessageExtraInfo();
            var input_wheel = MouseControl.MouseDataWheel(delta, extraInfo);

            var inputs = new NativeMethods.Input[]
            {
                input_wheel
            };
            NativeMethods.SendInput((uint)inputs.Length, ref inputs[0], Marshal.SizeOf(inputs[0]));
        }
    }
}
