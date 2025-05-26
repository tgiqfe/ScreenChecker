using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace ScreenChecker.Lib
{
    internal class MouseControl
    {
        #region Create Input methods

        /// <summary>
        /// Mouse move
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="screen"></param>
        /// <param name="extraInfo"></param>
        /// <returns></returns>
        static NativeMethods.Input MouseMoveData(int x, int y, Screen screen, IntPtr extraInfo)
        {
            x = x * 65536 / screen.Bounds.Width;
            y = y * 65536 / screen.Bounds.Height;

            var input = new NativeMethods.Input();
            input.Type = NativeMethods.INPUT_MOUSE;
            input.ui.Mouse.Flags = NativeMethods.MOUSEEVENTF_MOVE | NativeMethods.MOUSEEVENTF_ABSOLUTE;
            input.ui.Mouse.Data = 0;
            input.ui.Mouse.X = x;
            input.ui.Mouse.Y = y;
            input.ui.Mouse.Time = 0;
            input.ui.Mouse.ExtraInfo = extraInfo;
            return input;
        }

        /// <summary>
        /// Mouse click(left,right,middle)
        /// </summary>
        /// <param name="flags"></param>
        /// <param name="extraInfo"></param>
        /// <returns></returns>
        static NativeMethods.Input MouseDataWithoutMove(uint flags, IntPtr extraInfo)
        {
            var input = new NativeMethods.Input();
            input.Type = NativeMethods.INPUT_MOUSE;
            input.ui.Mouse.Flags = flags;
            input.ui.Mouse.Data = 0;
            input.ui.Mouse.X = 0;
            input.ui.Mouse.Y = 0;
            input.ui.Mouse.Time = 0;
            input.ui.Mouse.ExtraInfo = extraInfo;
            return input;
        }

        /// <summary>
        /// Mouse wheel
        /// </summary>
        /// <param name="delta"></param>
        /// <param name="extraInfo"></param>
        /// <returns></returns>
        static NativeMethods.Input MouseDataWheel(int delta, IntPtr extraInfo)
        {
            var input = new NativeMethods.Input();
            input.Type = NativeMethods.INPUT_MOUSE;
            input.ui.Mouse.Flags = NativeMethods.MOUSEEVENTF_WHEEL;
            input.ui.Mouse.Data = delta;
            input.ui.Mouse.X = 0;
            input.ui.Mouse.Y = 0;
            input.ui.Mouse.Time = 0;
            input.ui.Mouse.ExtraInfo = extraInfo;
            return input;
        }

        #endregion

        internal static void SendMouseMove(int x, int y, int screenNum = 0, bool isFast = false)
        {
            var screen = screenNum == 0 ?
                Screen.PrimaryScreen :
                Screen.AllScreens[screenNum];

            if (isFast)
            {
                var extraInfo = NativeMethods.GetMessageExtraInfo();
                var input_move = MouseMoveData(x, y, screen, extraInfo);

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
                SlowMove(pt.x, pt.y, x, y);
            }
        }

        internal static void SendMouseLeftClick(int interval = 100)
        {
            var extraInfo = NativeMethods.GetMessageExtraInfo();
            var input_leftdown = MouseDataWithoutMove(NativeMethods.MOUSEEVENTF_LEFTDOWN, extraInfo);
            var input_leftup = MouseDataWithoutMove(NativeMethods.MOUSEEVENTF_LEFTUP, extraInfo);

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
            var input_leftdown = MouseDataWithoutMove(NativeMethods.MOUSEEVENTF_LEFTDOWN, extraInfo);
            var input_leftup = MouseDataWithoutMove(NativeMethods.MOUSEEVENTF_LEFTUP, extraInfo);

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
            var input_middledown = MouseDataWithoutMove(NativeMethods.MOUSEEVENTF_MIDDLEDOWN, extraInfo);
            var input_middleup = MouseDataWithoutMove(NativeMethods.MOUSEEVENTF_MIDDLEUP, extraInfo);

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
            var input_rightdown = MouseDataWithoutMove(NativeMethods.MOUSEEVENTF_RIGHTDOWN, extraInfo);
            var input_rightup = MouseDataWithoutMove(NativeMethods.MOUSEEVENTF_RIGHTUP, extraInfo);

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
            var input_move_start = MouseMoveData(x1, y1, screen, extraInfo);
            var input_leftdown = MouseDataWithoutMove(NativeMethods.MOUSEEVENTF_LEFTDOWN, extraInfo);
            var input_move_end = MouseMoveData(x2, y2, screen, extraInfo);
            var input_leftup = MouseDataWithoutMove(NativeMethods.MOUSEEVENTF_LEFTUP, extraInfo);

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
                SlowMove(pt.x, pt.y, x1, y1);
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
                SlowMove(x1, y1, x2, y2);
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
            var input_wheel = MouseDataWheel(delta, extraInfo);

            var inputs = new NativeMethods.Input[]
            {
                input_wheel
            };
            NativeMethods.SendInput((uint)inputs.Length, ref inputs[0], Marshal.SizeOf(inputs[0]));
        }

        internal static void SlowMove(int x1, int y1, int x2, int y2)
        {
            int step = 15;
            int interval = 10;

            int length = Math.Abs(x2 - x1) >= Math.Abs(y2 - y1) ?
                Math.Abs(x2 - x1) / step :
                Math.Abs(y2 - y1) / step;
            if (length == 0) length = 1;
            for (int i = 0; i <= length; i++)
            {
                int x = x1 + (x2 - x1) * i / length;
                int y = y1 + (y2 - y1) * i / length;
                NativeMethods.SetCursorPos(x, y);
                Thread.Sleep(interval);
            }
        }
    }
}
