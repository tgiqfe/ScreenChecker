using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ScreenChecker.Lib;

namespace ScreenChecker.Cmdlet
{
    [Cmdlet(VerbsLifecycle.Invoke, "MouseClick")]
    public class InvokeMouseClick : PSCmdlet
    {
        [Parameter(Position = 0)]
        public MouseAction Action { get; set; } = MouseAction.Click;

        [Parameter]
        public int? X { get; set; }

        [Parameter]
        public int? Y { get; set; }

        [Parameter]
        public int ScreenNumber { get; set; } = 0;

        [Parameter]
        public int WheelDelta { get; set; }

        [Parameter]
        public SwitchParameter Fast { get; set; }

        [Parameter]
        public int Delay { get; set; } = 0;

        protected override void ProcessRecord()
        {
            if (this.Delay > 0) Thread.Sleep(this.Delay);

            if (this.X != null && this.Y != null)
            {
                MouseControl.SendMouseMove(this.X.Value, this.Y.Value, this.ScreenNumber, this.Fast);
            }

            switch (this.Action)
            {
                case MouseAction.Click:
                    MouseControl.SendMouseLeftClick();
                    break;
                case MouseAction.DoubleClick:
                    MouseControl.SendMouseLeftDoubleClick();
                    break;
                case MouseAction.RightClick:
                    MouseControl.SendMouseRightClick();
                    break;
                case MouseAction.MiddleClick:
                    MouseControl.SendMouseMiddleClick();
                    break;
                case MouseAction.Wheel:
                    MouseControl.SendMouseWheel(this.WheelDelta);
                    break;
            }
        }
    }
}
