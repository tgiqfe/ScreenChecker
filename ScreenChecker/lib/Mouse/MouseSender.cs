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

        /// <summary>
        /// Mouse mode.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="screenNum"></param>
        /// <param name="isFast"></param>
        public void MouseMove(int x, int y, int screenNum = 0, bool isFast = false)
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

        /// <summary>
        /// Mouse click(left).
        /// </summary>
        /// <param name="interval"></param>
        public void MouseLeftClick(int interval = 100)
        {
            var extraInfo = NativeMethods.GetMessageExtraInfo();
            SendInputProcess(MouseControl.MouseDataWithoutMove(NativeMethods.MOUSEEVENTF_LEFTDOWN, extraInfo));
            Thread.Sleep(interval);
            SendInputProcess(MouseControl.MouseDataWithoutMove(NativeMethods.MOUSEEVENTF_LEFTUP, extraInfo));
        }

        /// <summary>
        /// Mouse double click(left).
        /// </summary>
        /// <param name="interval"></param>
        /// <param name="wClickInterval"></param>
        public void MouseLeftDoubleClick(int interval = 100, int wClickInterval = 300)
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

        /// <summary>
        /// Mouse click(middle).
        /// </summary>
        /// <param name="interval"></param>
        public void MouseMiddleClick(int interval = 100)
        {
            var extraInfo = NativeMethods.GetMessageExtraInfo();
            SendInputProcess(MouseControl.MouseDataWithoutMove(NativeMethods.MOUSEEVENTF_MIDDLEDOWN, extraInfo));
            Thread.Sleep(interval);
            SendInputProcess(MouseControl.MouseDataWithoutMove(NativeMethods.MOUSEEVENTF_MIDDLEUP, extraInfo));
        }

        /// <summary>
        /// Mouse click(right).
        /// </summary>
        /// <param name="interval"></param>
        public void MouseRightClick(int interval = 100)
        {
            var extraInfo = NativeMethods.GetMessageExtraInfo();
            SendInputProcess(MouseControl.MouseDataWithoutMove(NativeMethods.MOUSEEVENTF_RIGHTDOWN, extraInfo));
            Thread.Sleep(interval);
            SendInputProcess(MouseControl.MouseDataWithoutMove(NativeMethods.MOUSEEVENTF_RIGHTUP, extraInfo));
        }

        /// <summary>
        /// Mouse drag and drop(left).
        /// </summary>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <param name="screenNum"></param>
        /// <param name="isFast"></param>
        /// <param name="interval"></param>
        public void MouseLeftDrag(int x1, int y1, int x2, int y2, int screenNum = 0, bool isFast = false, int interval = 500)
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

        /// <summary>
        /// Mouse drag and drop(left) from current position to specified position.
        /// </summary>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <param name="screenNum"></param>
        /// <param name="isFast"></param>
        /// <param name="interval"></param>
        public void MouseLeftDrag(int x2, int y2, int screenNum = 0, bool isFast = false, int interval = 500)
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

        /// <summary>
        /// Mouse wheel scroll.
        /// </summary>
        /// <param name="delta"></param>
        public void MouseWheel(int delta)
        {
            var extraInfo = NativeMethods.GetMessageExtraInfo();
            SendInputProcess(MouseControl.MouseDataWheel(delta, extraInfo));
        }
    }
}
