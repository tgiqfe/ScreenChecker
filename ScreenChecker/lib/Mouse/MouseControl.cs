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
        internal static NativeMethods.Input MouseMoveData(int x, int y, Screen screen, IntPtr extraInfo)
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
        internal static NativeMethods.Input MouseDataWithoutMove(uint flags, IntPtr extraInfo)
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
        internal static NativeMethods.Input MouseDataWheel(int delta, IntPtr extraInfo)
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
