using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ScreenChecker.Lib;
using ScreenChecker.Lib.Mouse;

namespace ScreenChecker.Cmdlet
{
    [Cmdlet(VerbsLifecycle.Invoke, "MouseClick")]
    public class InvokeMouseClick : PSCmdlet
    {
        [Parameter(ValueFromPipeline = true)]
        public ScreenCheckResult[] InputObject { get; set; }

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
        public int DoubleClickInterval { get; set; }

        [Parameter]
        public int Delay { get; set; } = 0;

        protected override void ProcessRecord()
        {
            if (this.Delay > 0) Thread.Sleep(this.Delay);

            var sender = new MouseSender();

            if (InputObject?.Length > 0)
            {
                var result = InputObject.Where(x => x.IsFound).OrderBy(x => x.MatchValue).FirstOrDefault();
                if(result != null)
                {

                }
            }
            else if (this.X != null && this.Y != null)
            {
                sender.MouseMove2(this.X.Value, this.Y.Value, this.ScreenNumber, this.Fast);
            }

            switch (this.Action)
            {
                case MouseAction.Click:
                    sender.MouseLeftClick2();
                    break;
                case MouseAction.DoubleClick:
                    sender.MouseLeftDoubleClick2(this.DoubleClickInterval);
                    break;
                case MouseAction.RightClick:
                    sender.MouseRightClick2();
                    break;
                case MouseAction.MiddleClick:
                    sender.MouseMiddleClick2();
                    break;
                case MouseAction.Wheel:
                    sender.MouseWheel2(this.WheelDelta);
                    break;
            }
        }
    }
}
