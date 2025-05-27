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
        private void SendInputProcess(params NativeMethods.Input[] inputs)
        {
            if (inputs?.Length > 0)
            {
                NativeMethods.SendInput((uint)inputs.Length, ref inputs[0], Marshal.SizeOf(inputs[0]));
            }
        }

        public void MouseMove2(int x, int y, int screenNum = 0, bool isFast = false)
        {
            var screen = screenNum == 0 ?
                Screen.PrimaryScreen :
                Screen.AllScreens[screenNum];
            var extraInfo = NativeMethods.GetMessageExtraInfo();
            if (isFast)
            {
                SendInputProcess(MouseControl.MouseMoveData(x, y, screen, extraInfo));
            }
            else
            {
                var pt = new NativeMethods.Win32Point() { x = 0, y = 0 };
                NativeMethods.GetCursorPos(ref pt);
                MouseControl.SlowMove(pt.x, pt.y, x, y);
            }
        }

        public void MouseLeftClick2(int interval = 100)
        {
            var extraInfo = NativeMethods.GetMessageExtraInfo();
            SendInputProcess(MouseControl.MouseDataWithoutMove(NativeMethods.MOUSEEVENTF_LEFTDOWN, extraInfo));
            Thread.Sleep(interval);
            SendInputProcess(MouseControl.MouseDataWithoutMove(NativeMethods.MOUSEEVENTF_LEFTUP, extraInfo));
        }

        public void MouseLeftDoubleClick2(int interval = 100, int wClickInterval = 300)
        {
            var extraInfo = NativeMethods.GetMessageExtraInfo();
            SendInputProcess(MouseControl.MouseDataWithoutMove(NativeMethods.MOUSEEVENTF_LEFTDOWN, extraInfo));
            Thread.Sleep(interval);
            SendInputProcess(MouseControl.MouseDataWithoutMove(NativeMethods.MOUSEEVENTF_LEFTUP, extraInfo));
            Thread.Sleep(wClickInterval);
            SendInputProcess(MouseControl.MouseDataWithoutMove(NativeMethods.MOUSEEVENTF_LEFTDOWN, extraInfo));
            Thread.Sleep(interval);
            SendInputProcess(MouseControl.MouseDataWithoutMove(NativeMethods.MOUSEEVENTF_LEFTUP, extraInfo));
        }

        public void MouseMiddleClick2(int interval = 100)
        {
            var extraInfo = NativeMethods.GetMessageExtraInfo();
            SendInputProcess(MouseControl.MouseDataWithoutMove(NativeMethods.MOUSEEVENTF_MIDDLEDOWN, extraInfo));
            Thread.Sleep(interval);
            SendInputProcess(MouseControl.MouseDataWithoutMove(NativeMethods.MOUSEEVENTF_MIDDLEUP, extraInfo));
        }

        public void MouseRightClick2(int interval = 100)
        {
            var extraInfo = NativeMethods.GetMessageExtraInfo();
            SendInputProcess(MouseControl.MouseDataWithoutMove(NativeMethods.MOUSEEVENTF_RIGHTDOWN, extraInfo));
            Thread.Sleep(interval);
            SendInputProcess(MouseControl.MouseDataWithoutMove(NativeMethods.MOUSEEVENTF_RIGHTUP, extraInfo));
        }

        public void MouseLeftDrag2(int x1, int y1, int x2, int y2, int screenNum = 0, bool isFast = false, int interval = 500)
        {
            var screen = screenNum == 0 ?
                Screen.PrimaryScreen :
                Screen.AllScreens[screenNum];
            var extraInfo = NativeMethods.GetMessageExtraInfo();

            //  move to drag start position.
            if (isFast)
            {
                SendInputProcess(MouseControl.MouseMoveData(x1, y1, screen, extraInfo));
            }
            else
            {
                var pt = new NativeMethods.Win32Point() { x = 0, y = 0 };
                NativeMethods.GetCursorPos(ref pt);
                MouseControl.SlowMove(pt.x, pt.y, x1, y1);
            }

            //  left down
            SendInputProcess(MouseControl.MouseDataWithoutMove(NativeMethods.MOUSEEVENTF_LEFTDOWN, extraInfo));

            //  move to drag end position.
            if (isFast)
            {
                Thread.Sleep(interval);
            }
            else
            {
                MouseControl.SlowMove(x1, y1, x2, y2);
            }
            SendInputProcess(
                MouseControl.MouseMoveData(x2, y2, screen, extraInfo),
                MouseControl.MouseDataWithoutMove(NativeMethods.MOUSEEVENTF_LEFTUP, extraInfo));
        }

        public void MouseLeftDrag2(int x2, int y2, int screenNum = 0, bool isFast = false, int interval = 500)
        {
            var screen = screenNum == 0 ?
                Screen.PrimaryScreen :
                Screen.AllScreens[screenNum];
            var extraInfo = NativeMethods.GetMessageExtraInfo();

            var pt = new NativeMethods.Win32Point() { x = 0, y = 0 };
            NativeMethods.GetCursorPos(ref pt);

            //  left down
            SendInputProcess(MouseControl.MouseDataWithoutMove(NativeMethods.MOUSEEVENTF_LEFTDOWN, extraInfo));

            //  move to drag end position.
            if (isFast)
            {
                Thread.Sleep(interval);
            }
            else
            {
                MouseControl.SlowMove(pt.x, pt.y, x2, y2);
            }
            SendInputProcess(
                MouseControl.MouseMoveData(x2, y2, screen, extraInfo),
                MouseControl.MouseDataWithoutMove(NativeMethods.MOUSEEVENTF_LEFTUP, extraInfo));
        }

        public void MouseWheel2(int delta)
        {
            var extraInfo = NativeMethods.GetMessageExtraInfo();
            SendInputProcess(MouseControl.MouseDataWheel(delta, extraInfo));
        }

        /*
        internal static void MouseMove(int x, int y, int screenNum = 0, bool isFast = false)
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

        internal static void MouseLeftClick(int interval = 100)
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

        internal static void MouseLeftDoubleClick(int interval = 100, int wClickInterval = 300)
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

        internal static void MouseMiddleClick(int interval = 100)
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

        internal static void MouseRightClick(int interval = 100)
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

        internal static void MouseLeftDrag(int x2, int y2, int screenNum = 0, bool isFast = false, int interval = 500)
        {
            var pt = new NativeMethods.Win32Point() { x = 0, y = 0 };
            NativeMethods.GetCursorPos(ref pt);
            MouseLeftDrag(pt.x, pt.y, x2, y2, screenNum, isFast, interval);
        }

        internal static void MouseLeftDrag(int x1, int y1, int x2, int y2, int screenNum = 0, bool isFast = false, int interval = 500)
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

        internal static void MouseWheel(int delta)
        {
            var extraInfo = NativeMethods.GetMessageExtraInfo();
            var input_wheel = MouseControl.MouseDataWheel(delta, extraInfo);

            var inputs = new NativeMethods.Input[]
            {
                input_wheel
            };
            NativeMethods.SendInput((uint)inputs.Length, ref inputs[0], Marshal.SizeOf(inputs[0]));
        }
        */
    }
}
